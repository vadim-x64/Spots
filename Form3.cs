using System;
using System.Windows.Forms;

namespace Spots {
    
    public partial class Form3 : Form {
        
        public Form3() {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            this.ActiveControl = null;
        }

        protected override void OnClick(EventArgs e) {
            base.OnClick(e);
            this.ActiveControl = null;
        }

        private void button1_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e) {
            Close();
        }
    }
}