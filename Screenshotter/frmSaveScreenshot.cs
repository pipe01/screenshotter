using Ookii.Dialogs;
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
    public partial class frmSaveScreenshot : Form
    {
        Image _Image;
        DateTime _Time;
        Configuration _Config;
        List<string> _Formats;

        public frmSaveScreenshot(Image screenshot, DateTime time, Configuration config)
        {
            InitializeComponent();

            _Image = screenshot;
            _Time = time;
            _Config = config;
        }

        private void frmSaveScreenshot_Load(object sender, EventArgs e)
        {
            _Formats = Camera.GetAvailableFormats().ToList();
            foreach (var item in _Formats)
            {
                cbFormat.Items.Add("." + item.ToLower());
            }
            cbFormat.SelectedIndex = _Formats.IndexOf(_Config.FileFormat);

            picPreview.BackgroundImage = _Image;

            txtFileName.Text = Camera.GenerateFileName(_Config.FileFormat, _Time, false);
            txtSavePath.Text = _Config.StoreFolder;

            this.Activate();
            this.BringToFront();
            this.Focus();
        }

        private void btnCopyClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(_Image);

            btnCopyClipboard.Text = "Copied";
            btnCopyClipboard.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string ext = "." + _Formats[cbFormat.SelectedIndex].ToLower();

            _Image.Save(System.IO.Path.Combine(txtSavePath.Text, txtFileName.Text + ext));
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtSavePath.Text = dialog.SelectedPath;
            }
        }

        private void picPreview_Click(object sender, EventArgs e)
        {
            var frm = new frmImageEditor(_Image);
            frm.ShowDialog();
            _Image = frm.Image;

            picPreview.BackgroundImage = _Image;
        }
        
        private void frmSaveScreenshot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
