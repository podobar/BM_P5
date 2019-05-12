using System.Windows.Forms;
using System.Media;
using AForge.Math;
using NAudio.Wave;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace BM_P5
{
    public partial class SpeechRecognitionForm : Form
    {
        private System.Media.SoundPlayer _player1;
        private System.Media.SoundPlayer _player2;
        private readonly int SAMPLES_PER_FRAME = 256;
        private readonly int frameOverlappingPercentage = 10;

        private Dictionary<string, List<double>> database_data; //string - speaker, List<double> - comparison result

        private float[] _data_Track1;
        private float[] _data_Track2;
        private float[] _data_File;

        private float[,] _matrix_OfLocalCost;
        private float[,] _matrix_OfGlobalCost;

        private Complex[] _data_complex_Track1;
        private Complex[] _data_complex_Track2;
        private Complex[] _data_complex_FileData;
        
        private List<Complex[]> _fft_frames_Track1;
        private List<Complex[]> _fft_frames_Track2;
        private List<Complex[]> _fft_frames_FileData;

        #region Can't touch this
        public SpeechRecognitionForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Computes Manhattan distance between vectors of complex numbers <paramref name="v1"/> and <paramref name="v2"/>
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private float ManhattanDistance(Complex[] v1, Complex[] v2)
        {
            double distance = 0;
            for (int i = 0; i < SAMPLES_PER_FRAME; ++i)
                distance += Math.Abs(v1[i].Magnitude - v2[i].Magnitude);

            return (float)Math.Sqrt(distance);
        }
        /// <summary>
        /// Reading .wav file chosen by user and prepare data array for further processing
        /// </summary>
        /// <param name="ts">Parameter deciding which track should be loaded (either first or second one)</param>
        private void LoadFile(TrackSelector ts)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Choose wisely";
                dialog.Filter = "Sound tracks (*.wav) | *.wav";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    PreprocessRawData(ts, dialog.FileName);
                    switch (ts)
                    {
                        case TrackSelector.First:

                            Label_OfTrack1.Text = dialog.FileName;
                            _player1?.Dispose();
                            _player1 = new SoundPlayer(dialog.FileName);
                            break;

                        case TrackSelector.Second:

                            Label_OfTrack2.Text = dialog.FileName;
                            _player2?.Dispose();
                            _player2 = new SoundPlayer(dialog.FileName);
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// Which sound should be played
        /// </summary>
        /// <param name="ts">Parameter deciding which sound from which track should be played</param>
        private void Play(TrackSelector ts)
        {
            switch (ts)
            {
                case TrackSelector.First:
                    _player1?.Play();
                    break;
                case TrackSelector.Second:
                    _player2?.Play();
                    break;
            }
        }
        /// <summary>
        /// Reading data from .wav and memory allocation for further processing
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="fileName"></param>
        private void PreprocessRawData(TrackSelector ts, string fileName)
        {
            var reader = new WaveFileReader(fileName);
            switch (ts)
            {
                case TrackSelector.First:
                    
                    _data_Track1 = new float[reader.Length/2];
                    reader.ToSampleProvider().Read(_data_Track1, 0, _data_Track1.Length);
                    _data_complex_Track1 = new Complex[reader.Length/2];
                    for (int i = 0; i < _data_Track1.Length; ++i)
                        _data_complex_Track1[i] = new Complex(_data_Track1[i], 0); //TODO: leave 0 or...?

                    _fft_frames_Track1 = new List<Complex[]>();
                    PrepareFFTValues(ts);
                    break;

                case TrackSelector.Second:
                    
                    _data_Track2 = new float[reader.Length/2];
                    reader.ToSampleProvider().Read(_data_Track2, 0, _data_Track2.Length);

                    _data_complex_Track2 = new Complex[reader.Length/2];
                    for (int i = 0; i < _data_Track2.Length; ++i)
                        _data_complex_Track2[i] = new Complex(_data_Track2[i], 0);

                    _fft_frames_Track2 = new List<Complex[]>();
                    PrepareFFTValues(ts);
                    break;

                case TrackSelector.File:
                    _data_File = new float[reader.Length / 2];
                    reader.ToSampleProvider().Read(_data_File, 0, _data_File.Length);

                    _data_complex_FileData = new Complex[reader.Length / 2];
                    for (int i = 0; i < _data_File.Length; ++i)
                        _data_complex_FileData[i] = new Complex(_data_File[i], 0);

                    _fft_frames_FileData = new List<Complex[]>();
                    PrepareFFTValues(ts);
                    break;
            }
        }
        
        /// <summary>
        /// Cost is the distance between vectors, distance function is given in third parameter
        /// </summary>
        /// <param name="length1"></param>
        /// <param name="length2"></param>
        /// <param name="cost_function"></param>
        private void CreateMatrixOfLocalCost(int x, int y, List<Complex[]> frames_1, List<Complex[]> frames_2, Func<Complex[], Complex[], float> cost_function)
        {
            _matrix_OfLocalCost = new float[x, y];
            for (int i = 0; i < x; ++i)
                for (int j = 0; j < y; ++j)
                    _matrix_OfLocalCost[i, j] = cost_function(frames_1[i], frames_2[j]);
        }
        /// <summary>
        /// Creating matrix with global costs, requires computing matrix of local cost before. Computation complexity : O(x*y)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void CreateMatrixOfGlobalCost(int x, int y)
        {
            _matrix_OfGlobalCost = new float[x, y];
            _matrix_OfGlobalCost[0, 0] = _matrix_OfLocalCost[0, 0];

            for(int i=1;i<x;++i)
                _matrix_OfGlobalCost[i, 0] = _matrix_OfGlobalCost[i - 1, 0] + _matrix_OfLocalCost[i, 0];
            for(int j=1;j<y;++j)
                _matrix_OfGlobalCost[0, j] = _matrix_OfGlobalCost[0, j-1] + _matrix_OfLocalCost[0, j];
            for (int i = 1; i < x; ++i)
                for (int j = 1; j < y; ++j)
                    _matrix_OfGlobalCost[i, j] = FindMinOf(
                            _matrix_OfGlobalCost[i - 1, j],
                            _matrix_OfGlobalCost[i, j - 1], 
                            _matrix_OfGlobalCost[i - 1, j - 1]) 
                        + _matrix_OfLocalCost[i, j];
        }
        /// <summary>
        /// Simplyfying condition: first element is (0,0), and last one is (x-1,y-1) so that tracks match each other
        /// </summary>
        /// <returns>Cheapest path from (0,0) to (x-1,y-1)</returns>
        private List<(int x, int y)> FindOptimalPath(int x, int y)
        {
            var list = new List<(int, int)>() {(--x,--y)};
            do//index out of range if
            {
                if (x == 0)
                    list.Add((x, --y));
                else if (y == 0)
                    list.Add((--x, y));
                else if (FindMinOf(_matrix_OfGlobalCost[x - 1, y - 1], _matrix_OfGlobalCost[x, y - 1], _matrix_OfGlobalCost[x - 1, y]) == _matrix_OfGlobalCost[x - 1, y - 1])
                    list.Add((--x, --y));
                else if (FindMinOf(_matrix_OfGlobalCost[x - 1, y - 1], _matrix_OfGlobalCost[x, y - 1], _matrix_OfGlobalCost[x - 1, y]) == _matrix_OfGlobalCost[x - 1, y])
                    list.Add((--x, y));
                else
                    list.Add((x, --y));
            } while (x != 0 || y!=0);
            list.Reverse();
            return list;
        }
        /// <summary>
        /// Finds minimum of given values
        /// </summary>
        /// <param name="values"></param>
        /// <returns>Minimal value</returns>
        private float FindMinOf(params float[] values)
        {
            return values.Min();
        }
        private float GetPathCost(List<(int x,int y)> path)
        {
            return _matrix_OfGlobalCost[path[path.Count - 1].x - 1, path[path.Count - 1].y - 1];
        }
        /// <summary>
        /// Cutting tracks into frames with 10% of data from previous frame (excluding first one)
        /// </summary>
        private void PrepareFFTValues(TrackSelector ts)
        {
            int frameStep = SAMPLES_PER_FRAME - (int)(SAMPLES_PER_FRAME * frameOverlappingPercentage / 100.0);
            switch (ts)
            {
                case TrackSelector.First:
                    {
                        _fft_frames_Track1.Clear();
                        for (int i = 0; i < _data_Track1.Length - SAMPLES_PER_FRAME; i += frameStep)
                        {
                            var frame = new Complex[SAMPLES_PER_FRAME];
                            System.Array.Copy(_data_complex_Track1, i, frame, 0, SAMPLES_PER_FRAME);
                            FourierTransform.FFT(frame, FourierTransform.Direction.Forward);
                            _fft_frames_Track1.Add(frame);
                        }
                        break;
                    }
                case TrackSelector.Second:
                    {
                        _fft_frames_Track2.Clear();
                        for (int i = 0; i < _data_Track2.Length - SAMPLES_PER_FRAME; i += frameStep)
                        {
                            var frame = new Complex[SAMPLES_PER_FRAME];
                            System.Array.Copy(_data_complex_Track2, i, frame, 0, SAMPLES_PER_FRAME);
                            FourierTransform.FFT(frame, FourierTransform.Direction.Forward);
                            _fft_frames_Track2.Add(frame);
                        }
                        break;
                    }
                case TrackSelector.File:
                    {
                        _fft_frames_FileData.Clear();
                        for (int i = 0; i < _data_File.Length - SAMPLES_PER_FRAME; i += frameStep)
                        {
                            var frame = new Complex[SAMPLES_PER_FRAME];
                            System.Array.Copy(_data_complex_FileData, i, frame, 0, SAMPLES_PER_FRAME);
                            FourierTransform.FFT(frame, FourierTransform.Direction.Forward);
                            _fft_frames_FileData.Add(frame);
                        }
                        
                        break;
                    }
            }
        }
        #endregion

        
        #region Event handlers
        private void LoadButton_Track1_Click(object sender, System.EventArgs e)
        {
            LoadFile(TrackSelector.First);
        }
        private void PlayButton_Track1_Click(object sender, System.EventArgs e)
        {
            Play(TrackSelector.First);
        }
        private void LoadButton_Track2_Click(object sender, System.EventArgs e)
        {
            LoadFile(TrackSelector.Second);
        }
        private void PlayButton_Track2_Click(object sender, System.EventArgs e)
        {
            Play(TrackSelector.Second);
        }
        /// <summary>
        /// Only compares files loaded manually (track 1 and track 2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompareTracks_1_2_Click(object sender, EventArgs e)
        {
            if (_data_Track1 == null || _data_Track2 == null)
                return;
            int x = _fft_frames_Track1.Count, y = _fft_frames_Track2.Count;
            CreateMatrixOfLocalCost(x, y, _fft_frames_Track1, _fft_frames_Track2, ManhattanDistance);
            CreateMatrixOfGlobalCost(x, y);
            DrawMatricesAndCalculateCost(x, y);
        }
        /// <summary>
        /// Compares file loaded to track 1 with whole database selected by user. 
        /// Proper nomenclature of files within database directories must be preserved.
        /// For further information see how "Samples" directory is kept.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompareWithDatabaseData_Click(object sender, EventArgs e)
        {
            if (_data_Track1 == null)
                return;
            database_data?.Clear();
            database_data = new Dictionary<string, List<double>>();
            using (var fbd = new FolderBrowserDialog())
            {
                if(fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] dirs = Directory.GetDirectories(fbd.SelectedPath);
                    foreach(var dir in dirs)
                    {
                        var filepaths = Directory.GetFiles(dir);
                        foreach(var filepath in filepaths)
                        {
                            var file_string_tokens = filepath.Split('\\');
                            string title = file_string_tokens.Last();
                            string track1_TitleHeader = Label_OfTrack1.Text.Split('\\').Last().Split('_').First();
                            if (title.Contains(track1_TitleHeader))
                            {
                                string speaker = file_string_tokens[file_string_tokens.Length - 2];
                                PreprocessRawData(TrackSelector.File, filepath);
                                int x = _fft_frames_Track1.Count, y = _fft_frames_FileData.Count;
                                CreateMatrixOfLocalCost(x, y, _fft_frames_Track1, _fft_frames_FileData, ManhattanDistance);
                                CreateMatrixOfGlobalCost(x, y);
                                if (database_data.ContainsKey(speaker))
                                    database_data[speaker].Add(GetPathCost(FindOptimalPath(x, y)));
                                else
                                    database_data.Add(speaker, new List<double>() { GetPathCost(FindOptimalPath(x, y)) });
                            }
                        }
                    }
                    VerifyUser();
                }
            }
            
        }
        private void CopyToClipboard_LocalCost(object sender, EventArgs e)
        {
            if(LocalMatrixPBox.Image!=null)
                Clipboard.SetImage(LocalMatrixPBox.Image);
        }
        private void CopyToClipboard_GlobalCost(object sender, EventArgs e)
        {
            if (GlobalMatrixPBox.Image != null)
                Clipboard.SetImage(GlobalMatrixPBox.Image);
        }
        #endregion
        /// <summary>
        /// Draws both local cost matrix and global cost matrix.
        /// Computes cost of minimal path and inserts it
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void DrawMatricesAndCalculateCost(int x, int y)
        {
            float max_local = 0, max_global = 0;
            for (int i = 0; i < x; ++i)
                for (int j = 0; j < y; ++j)
                {
                    if (_matrix_OfLocalCost[i, j] > max_local)
                        max_local = _matrix_OfLocalCost[i, j];
                    if (_matrix_OfGlobalCost[i, j] > max_global)
                        max_global = _matrix_OfGlobalCost[i, j];
                }
            var bmp_local = new Bitmap(x, y);
            var bmp_global = new Bitmap(x, y);
            LocalMatrixPBox.Image?.Dispose();
            GlobalMatrixPBox.Image?.Dispose();
            var path = FindOptimalPath(x, y);
            for (int i = 0; i < x; ++i)
                for (int j = 0; j < y; ++j)
                {
                    var local_output_color = 255 - 255 * _matrix_OfLocalCost[i, j] / max_local;
                    var c = (int)local_output_color;
                    bmp_local.SetPixel(i, j, Color.FromArgb(c, c, c));

                    if (path.Contains((i, j)))
                        bmp_global.SetPixel(i, j, Color.Red);
                    else
                    {
                        var global_output_color = 255 - 255 * _matrix_OfGlobalCost[i, j] / max_global;
                        c = (int)global_output_color;
                        bmp_global.SetPixel(i, j, Color.FromArgb(c, c, c));
                    }
                }
            bmp_local.RotateFlip(RotateFlipType.Rotate180FlipX);
            bmp_global.RotateFlip(RotateFlipType.Rotate180FlipX);
            LocalMatrixPBox.Image = bmp_local;
            GlobalMatrixPBox.Image = bmp_global;
            CostTextBox.Text = GetPathCost(path).ToString();
        }
        /// <summary>
        /// Shows which user is recognized by algorithm, optionally saves results in destination selected by user
        /// </summary>
        private void VerifyUser()
        {
            var sorted_ByNonZeroAverage = database_data.OrderBy(speaker =>
            {
                int non_zero_components = 0;
                foreach (var record in speaker.Value)
                    if (record != 0)
                        ++non_zero_components;
                return speaker.Value.Sum() / non_zero_components;
            });
            
            MessageBox.Show("Track 1 belongs to: "+sorted_ByNonZeroAverage?.First().Key);
            if(SaveToFileCheckBox.Checked)
                using(var saveDialog = new System.Windows.Forms.SaveFileDialog() { Title= "Choose where you want to keep results of comparison with database", Filter= "Text files | *.txt" })
                {
                    if(saveDialog.ShowDialog() == DialogResult.OK && !String.IsNullOrEmpty(saveDialog.FileName))
                    {
                        using (var sw = new StreamWriter(saveDialog.FileName))
                        {
                            foreach(var speaker in sorted_ByNonZeroAverage)
                            {
                                var sb = new StringBuilder(speaker.Key + " ");
                                foreach (var record in speaker.Value)
                                    sb.Append(record + "; ");
                                sw.WriteLine(sb.ToString());
                            }
                        }
                    }
                }
        }
        
        private enum TrackSelector
        {
            First,
            Second,
            File
        }
    }
}
