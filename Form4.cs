using NAudio.Wave;
using System;
using System.Windows.Forms;

namespace Spots {
    
    public partial class Form4 : Form {
        private AudioFileReader audioFileReader;
        private IWavePlayer iWavePlayer;

        public Form4() {
            InitializeComponent();
            iWavePlayer = new WaveOut();
            audioFileReader = new AudioFileReader("C:\\Documents\\study\\c#\\Spots\\bin\\Debug\\touch.mp3");
            iWavePlayer.Init(audioFileReader);
        }

        private void pictureBox1_Click(object sender, EventArgs e) {
            audioFileReader.Position = 0;
            iWavePlayer.Play();
            Close();
        }
    }
}