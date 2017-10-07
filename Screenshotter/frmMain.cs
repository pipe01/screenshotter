using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screenshotter
{
    public partial class frmMain : Form
    {
        string[] _FileFormats = new string[0];
        KeyboardHook _Hook = new KeyboardHook();
        Stopwatch _Stopwatch = new Stopwatch();
        Configuration _Config = Configuration.Load();

        public frmMain()
        {
            InitializeComponent();
        }

        void LoadConfig()
        {
            if (_Config.StoreFolder == null)
                _Config.StoreFolder = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                        "Screenshots");

            if (!Directory.Exists(_Config.StoreFolder))
                Directory.CreateDirectory(_Config.StoreFolder);


            cbFileFormats.SelectedIndex = _FileFormats.ToList().IndexOf(_Config.FileFormat);
            chkEffect.Checked = _Config.ScreenEffect;
            tbScale.Value = (int)(_Config.ImageScale * 100);
            chkWinStart.Checked = WindowsStartup.Enabled;

            tbScale_Scroll(this, EventArgs.Empty);


            _Config.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.Icon = this.Icon;

            _Hook.KeyPressed += _Hook_KeyPressed;
            _Hook.RegisterHotKey(Screenshotter.ModifierKeys.Shift, Keys.PrintScreen);
            _Hook.RegisterHotKey(Screenshotter.ModifierKeys.Control |
                                 Screenshotter.ModifierKeys.Shift, Keys.PrintScreen);
            _Hook.RegisterHotKey(Screenshotter.ModifierKeys.Control |
                                 Screenshotter.ModifierKeys.Shift |
                                 Screenshotter.ModifierKeys.Alt, Keys.PrintScreen);
            
            _FileFormats = Camera.GetAvailableFormats().ToArray();

            foreach (var item in _FileFormats)
            {
                cbFileFormats.Items.Add("." + item.ToLower());
            }

            LoadConfig();
        }

        private void _Hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            bool shift = ModifierDown(e.Modifier, Screenshotter.ModifierKeys.Shift);
            bool ctrl = ModifierDown(e.Modifier, Screenshotter.ModifierKeys.Control);
            bool alt = ModifierDown(e.Modifier, Screenshotter.ModifierKeys.Alt);

            bool saveAs = (shift && ctrl) && !alt;
            bool cropShot = (shift && ctrl && alt) && !saveAs;

            TakeScreenshot(saveAs, cropShot);
        }

        bool ModifierDown(ModifierKeys mods, ModifierKeys target)
        {
            return (mods & target) == target;
        }

        void TakeScreenshot(bool saveAs = false, bool cropShot = false)
        {
            if (_Stopwatch.ElapsedMilliseconds < 1000 && _Stopwatch.IsRunning)
                return;

            _Stopwatch.Restart();
            
            string name = Camera.GenerateFileName(_Config.FileFormat);
            var img = Camera.TakeScreenshot(cropShot ? 1 : _Config.ImageScale);

            if (_Config.ScreenEffect && !cropShot)
                new frmOverlay().Show();

            Camera.PlaySound();

            if (cropShot)
            {
                new frmImageEditor(img, true).Show();
            }
            else if (saveAs)
            {
                new frmSaveScreenshot(img, DateTime.Now, _Config).Show();
            }
            else
            {
                img.Save(Path.Combine(_Config.StoreFolder, name));
            }

            GC.Collect();
        }

        void TimeSave()
        {
            timerSave.Stop();
            timerSave.Start();
        }

        private void cbFileFormats_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Config.FileFormat = _FileFormats[cbFileFormats.SelectedIndex];
            _Config.Save();
        }

        private void tbScale_Scroll(object sender, EventArgs e)
        {
            float scale = tbScale.Value / 100.0f;
            int w = (int)(Camera.GetRealScreenshotSize().Width * scale);
            int h = (int)(Camera.GetRealScreenshotSize().Height * scale);

            lblScale.Text = $"({w},{h})";
            _Config.ImageScale = scale;

            TimeSave();
        }

        private void timerSave_Tick(object sender, EventArgs e)
        {
            _Config.Save();
            timerSave.Stop();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                HideToTaskbar();
            }
            else
            {
                _Config.Save();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void HideToTaskbar()
        {
            notifyIcon1.Visible = true;
            this.Hide();
        }

        void RestoreFromTaskbar()
        {
            notifyIcon1.Visible = false;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            RestoreFromTaskbar();
        }

        private void chkEffect_CheckedChanged(object sender, EventArgs e)
        {
            _Config.ScreenEffect = chkEffect.Checked;
            TimeSave();
        }

        private void chkWinStart_CheckedChanged(object sender, EventArgs e)
        {
            WindowsStartup.Enabled = chkWinStart.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new frmImageEditor(new Bitmap(50, 50), true).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //new frmCropShotViewer(new Bitmap(50, 50), Cursor.Position).Show();
        }
    }
}
