using Leap;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Speech.Synthesis;
using LeapVox.Audio;
using System.Windows.Media.Animation;

namespace LeapVox
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        PortamentoSineWaveOscillator osc;
        WaveOut WaveOutput;

        Controller Leap=null;
        DispatcherTimer dt = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1 / 30) };

        public MainWindow()
        {
            InitializeComponent();

            Leap = new Controller();

            dt.Tick += dt_Tick;
            dt.Start();

            WaveOutput = new WaveOut();
            osc = new PortamentoSineWaveOscillator(44100,120);
            osc.Pitch = 120;
            osc.Amplitude = 0;

            WaveOutput.Init(osc);
            WaveOutput.Play();

            FirePS.Start();
        }

        void dt_Tick(object sender, EventArgs e)
        {
            using (var Frame = Leap.Frame())
            {
                var f = Frame.Fingers.FirstOrDefault();
                if (f != null)
                {
                    var p = Math.Abs(f.TipPosition.y / 2);
                    var a = 255 - Math.Abs(f.TipPosition.x);
                    if (p >= 0 && p <= 150) osc.Pitch = p;
                    if (a >= 0 && a <= 255) osc.Amplitude = (short)a;
                    emit.X = f.TipPosition.x * 2 + 512;
                    emit.Y = -f.TipPosition.y * 2 + 600;
                }
            }
        }


    }

}
