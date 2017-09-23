using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screenshotter
{
    public partial class frmOverlay : Form
    {
        public frmOverlay()
        {
            InitializeComponent();
        }

        bool back = false;
        public int Duration { get; set; } = 200;

        private void frmOverlay_Load(object sender, EventArgs e)
        {
            this.Bounds = Screen.PrimaryScreen.Bounds;
            timer1.Interval = Duration / 20;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1.0f && !back)
            {
                this.Opacity += .1f;
            }
            else if (this.Opacity >= 1.0f)
            {
                back = true;
            }

            if (back)
            {
                this.Opacity -= .1f;
            }

            if (this.Opacity <= 0 && back)
            {
                this.Close();
            }
        }
    }
}
