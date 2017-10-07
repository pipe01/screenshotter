using Screenshotter.Utils;
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
            //The screenshot store folder hasn't yet been set,
            //set it to X:/Users/<user>/Pictures/Screenshots
            if (_Config.StoreFolder == null)
                _Config.StoreFolder = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                        "Screenshots");

            //Create the previously mentioned folder
            if (!Directory.Exists(_Config.StoreFolder))
                Directory.CreateDirectory(_Config.StoreFolder);

            //Set control values to match config:
            //Format combo box
            cbFileFormats.SelectedIndex = _FileFormats.ToList().IndexOf(_Config.FileFormat);

            //Screen effect check box
            chkEffect.Checked = _Config.ScreenEffect;

            //Resolution slider
            tbScale.Value = (int)(_Config.ImageScale * 100);

            //Windows startup check box
            chkWinStart.Checked = WindowsStartup.Enabled;

            //Simulate slider scroll to set labels
            tbScale_Scroll(this, EventArgs.Empty);

            //Save the config
            _Config.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.Icon = this.Icon;

            _Hook.KeyPressed += _Hook_KeyPressed;

            //Register hotkeys
            //Quick screenshot
            _Hook.RegisterHotKey(Utils.ModifierKeys.Shift, Keys.PrintScreen);

            //Custom screenshot
            _Hook.RegisterHotKey(Utils.ModifierKeys.Control |
                                 Utils.ModifierKeys.Shift, Keys.PrintScreen);

            //Crop shot
            _Hook.RegisterHotKey(Utils.ModifierKeys.Control |
                                 Utils.ModifierKeys.Shift |
                                 Utils.ModifierKeys.Alt, Keys.PrintScreen);
            

            //Get formats
            _FileFormats = Camera.GetAvailableFormats().ToArray();

            //Add them to the combo box
            foreach (var item in _FileFormats)
            {
                cbFileFormats.Items.Add("." + item.ToLower());
            }

            LoadConfig();
        }

        private void _Hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            bool shift = ModifierDown(e.Modifier, Utils.ModifierKeys.Shift);
            bool ctrl = ModifierDown(e.Modifier, Utils.ModifierKeys.Control);
            bool alt = ModifierDown(e.Modifier, Utils.ModifierKeys.Alt);

            //Save as if Shift and Control are pressed and Alt isn't
            bool saveAs = (shift && ctrl) && !alt;

            //Crop shot if Shift, Control and Alt are pressed and we are not Saving as
            bool cropShot = (shift && ctrl && alt) && !saveAs;

            //Take the screenshot
            TakeScreenshot(saveAs, cropShot);
        }

        bool ModifierDown(ModifierKeys mods, ModifierKeys target)
        {
            return (mods & target) == target;
        }

        void TakeScreenshot(bool saveAs = false, bool cropShot = false)
        {
            //Only take screenshots every 1 second
            if (_Stopwatch.ElapsedMilliseconds < 1000 && _Stopwatch.IsRunning)
                return;

            _Stopwatch.Restart();
            
            //Generate file name for screenshot
            string name = Camera.GenerateFileName(_Config.FileFormat);

            //Get the screenshot
            var img = Camera.TakeScreenshot(cropShot ? 1 : _Config.ImageScale);

            //If the user wants to see the screen effect and this isn't a crop shot, show it
            if (_Config.ScreenEffect && !cropShot)
                new frmOverlay().Show();

            //Play the sound
            Camera.PlaySound();

            if (cropShot)
            {
                //If it's a crop shot, show the Image Editor in crop shot mode
                new frmImageEditor(img, true).Show();
            }
            else if (saveAs)
            {
                //If we are Saving as, show the Save Screenshot dialog
                new frmSaveScreenshot(img, DateTime.Now, _Config).Show();
            }
            else
            {
                //If it's a regular screenshot, just save it
                img.Save(Path.Combine(_Config.StoreFolder, name));
            }

            //Collect GC
            GC.Collect();
        }

        /// <summary>
        /// Saves the functions hasn't been called for a length of time
        /// </summary>
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
    }
}
