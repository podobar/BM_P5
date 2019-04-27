using System.Windows.Forms;
using System.Media;
using AForge.Math;
using NAudio.Wave;
using System.Collections.Generic;

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

        private Complex[] _fft_Track1;
        private Complex[] _fft_Track2;

        private float[] _fft_module_Track1;
        private float[] _fft_module_Track2;

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

                    _fft_Track1 = new Complex[_data_Track1.Length / SAMPLES_PER_FRAME];
                    break;

                case TrackSelector.Second:

                    waveViewer2.WaveStream = reader;
                    _data_Track2 = new float[reader.Length];
                    reader.ToSampleProvider().Read(_data_Track2, 0, _data_Track2.Length);

                    _data_complex_Track2 = new Complex[reader.Length];
                    for (int i = 0; i < _data_Track2.Length; ++i)
                        _data_complex_Track2[i] = new Complex(_data_Track2[i], i);

                    _fft_Track2 = new Complex[_data_Track2.Length / SAMPLES_PER_FRAME];
                    break;
            }
        }
        #endregion

        /// <summary>
        /// Main DTW algorithm implementation comparing track1 and track2 (only if both loaded). Algorithm complexity: O(N * M), where N and M depends on each loaded track
        /// </summary>
        private void DTW_Compare()
        {
            if (_data_Track1 == null || _data_Track2 == null)
                return;

            //var tmp_frame;
            //TODO: FFT here, but how to divide whole array into frames?
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
        #endregion
        private enum TrackSelector
        {
            First,
            Second
        }
    }

}
