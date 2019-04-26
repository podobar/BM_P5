
using System.Windows.Forms;
using AForge.Math;
namespace BM_P5
{
    public partial class SpeechRecognitionForm : Form
    {
        public SpeechRecognitionForm()
        {
            InitializeComponent();
        }
        public void test_method()
        {
            //MathNet.Numerics.IntegralTransforms.Fourier.Forward; działa niezbyt XD
            //AForge
            Complex[] data = new Complex[1];
            FourierTransform.Direction direction = FourierTransform.Direction.Forward;
            FourierTransform.FFT(data, direction);
        }
    }

}
