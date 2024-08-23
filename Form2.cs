using System;
using System.Windows.Forms;

namespace Spots {
    
    public partial class Form2 : Form {

        public Form2() {
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

        public void button1_Click(object sender, EventArgs e) {
            Close();
        }
    }
}