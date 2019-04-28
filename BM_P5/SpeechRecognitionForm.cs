using System.Windows.Forms;
using System.Media;
using AForge.Math;
using NAudio.Wave;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BM_P5
{
    public partial class SpeechRecognitionForm : Form
    {
        private System.Media.SoundPlayer _player1;
        private System.Media.SoundPlayer _player2;
        private readonly int SAMPLES_PER_FRAME = 256;

        private float[] _data_Track1;
        private float[] _data_Track2;

        private Complex[] _data_complex_Track1;
        private Complex[] _data_complex_Track2;
        
        private List<Complex[]> _fft_frames_Track1;
        private List<Complex[]> _fft_frames_Track2;

        #region Completed, can't touch this
        public SpeechRecognitionForm()
        {
            InitializeComponent();
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
                    PreprocessRawData(ts, dialog.FileName); //TODO: ZAZNACZ, ŻE  "ONLY WORKS FOR PCM"
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

                    waveViewer1.WaveStream = reader;
                    _data_Track1 = new float[reader.Length];
                    reader.ToSampleProvider().Read(_data_Track1, 0, _data_Track1.Length);

                    _data_complex_Track1 = new Complex[reader.Length];
                    for (int i = 0; i < _data_Track1.Length; ++i)
                        _data_complex_Track1[i] = new Complex(_data_Track1[i], i); //TODO: i or something much smaller?

                    _fft_frames_Track1 = new List<Complex[]>();
                    break;

                case TrackSelector.Second:

                    waveViewer2.WaveStream = reader;
                    _data_Track2 = new float[reader.Length];
                    reader.ToSampleProvider().Read(_data_Track2, 0, _data_Track2.Length);

                    _data_complex_Track2 = new Complex[reader.Length];
                    for (int i = 0; i < _data_Track2.Length; ++i)
                        _data_complex_Track2[i] = new Complex(_data_Track2[i], i);

                    _fft_frames_Track2 = new List<Complex[]>();
                    break;
            }
                //TODO: Delete this
            PrepareFFTValues();
            CalculateMatrixOfCost(_fft_frames_Track1.Count, _fft_frames_Track2.Count, EuclideanDistance);
        }
        /// <summary>
        /// Cost is the distance between vectors, distance function is given in third parameter
        /// </summary>
        /// <param name="length1"></param>
        /// <param name="length2"></param>
        /// <param name="cost_function"></param>
        private void CalculateMatrixOfCost(int x, int y, Func<Complex[], Complex[], float> cost_function)
        {
            var _matrix_OfCost = new float[x, y];
            //TODO: Wyjaśnij to w sprawozdaniu
            //First and last point match each other (simplyfying assumption)
            _matrix_OfCost[0, 0] = 0;
            _matrix_OfCost[x - 1, y - 1] = 0;
            
            for (int j = 1; j < y; ++j)     //First column
                _matrix_OfCost[0, j] = _matrix_OfCost[0, j - 1] + cost_function(_fft_frames_Track1[0], _fft_frames_Track2[j]);
            for (int i = 1; i < y; ++i)     //First Row
                _matrix_OfCost[i, 0] = _matrix_OfCost[i - 1, 0] + cost_function(_fft_frames_Track1[i], _fft_frames_Track2[0]);
            //TODO: Optimize calculating area -> how far do you want to count cost?
            for(int i = 1; i < x; ++i)
            {
                for (int j = 1; j < y; ++j)
                {

                }
            }
                
        }
        #endregion

        /// <summary>
        /// Main DTW algorithm implementation comparing track1 and track2 (only if both loaded). Algorithm complexity: O(N * M), where N and M depends on each loaded track
        /// </summary>
        private void PrepareFFTValues()
        {
            if (_data_Track1 == null || _data_Track2 == null)
                return;
            _fft_frames_Track1.Clear();
            _fft_frames_Track2.Clear();
            //TODO: W sprawozdaniu zaznacz, że ostatnia ramka MOŻE BYĆ niepełna, dlatego pomijamy ją. Ze względu na  taka ilość danych może być zaniedbana (30k na )
            var tmp_frame = new Complex[SAMPLES_PER_FRAME];
            for (int i = 0; i < _data_Track1.Length / SAMPLES_PER_FRAME; ++i)
            {
                System.Array.Copy(_data_complex_Track1, SAMPLES_PER_FRAME * i, tmp_frame, 0, SAMPLES_PER_FRAME);
                FourierTransform.FFT(tmp_frame, FourierTransform.Direction.Forward);
                for (int j = 0; j < SAMPLES_PER_FRAME; ++j)
                    _fft_module_Track1[i] += (float)tmp_frame[j].Magnitude;
            }
            for (int i = 0; i < _data_Track2.Length / SAMPLES_PER_FRAME; ++i)
            {
                System.Array.Copy(_data_complex_Track2, SAMPLES_PER_FRAME * i, tmp_frame, 0, SAMPLES_PER_FRAME);
                FourierTransform.FFT(tmp_frame, FourierTransform.Direction.Forward);
                for (int j = 0; j < SAMPLES_PER_FRAME; ++j)
                    _fft_module_Track2[i] += (float)tmp_frame[j].Magnitude;
            }
            ;
        }
        private float EuclideanDistance(Complex[] vector1, Complex[] vector2)
        {
            double distance = 0;
            for (int i = 0; i < SAMPLES_PER_FRAME; ++i)
                distance += Math.Pow(vector1[i].Magnitude - vector2[i].Magnitude, 2);
            return (float)Math.Sqrt(distance);
        }

        //_Clicks
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
        #endregion
        private enum TrackSelector
        {
            First,
            Second
        }
    }
}
