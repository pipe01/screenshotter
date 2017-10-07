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
    public partial class frmCropShotViewer : Form
    {
        public frmCropShotViewer(Image image, Image originalImage, Point startPos)
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.ResizeRedraw, true);

            _Image = image;
            _OriginalImage = originalImage;
            _FormStartPos = startPos;
        }

        private Image _Image, _OriginalImage;
        private bool _Moving;
        private Point _MouseStartPos, _FormStartPos;
        
        #region Borderless resizing
        private const int
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17;

        const int Border = 10; // you can rename this variable if you like

        Rectangle RectTop { get { return new Rectangle(0, 0, this.ClientSize.Width, Border); } }
        Rectangle RectLeft { get { return new Rectangle(0, 0, Border, this.ClientSize.Height); } }
        Rectangle RectBottom { get { return new Rectangle(0, this.ClientSize.Height - Border, this.ClientSize.Width, Border); } }
        Rectangle RectRight { get { return new Rectangle(this.ClientSize.Width - Border, 0, Border, this.ClientSize.Height); } }

        Rectangle TopLeft { get { return new Rectangle(0, 0, Border, Border); } }
        Rectangle TopRight { get { return new Rectangle(this.ClientSize.Width - Border, 0, Border, Border); } }
        Rectangle BottomLeft { get { return new Rectangle(0, this.ClientSize.Height - Border, Border, Border); } }
        Rectangle BottomRight { get { return new Rectangle(this.ClientSize.Width - Border, this.ClientSize.Height - Border, Border, Border); } }
        
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == 0x84) // WM_NCHITTEST
            {
                var cursor = this.PointToClient(Cursor.Position);

                if (TopLeft.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
                else if (TopRight.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
                else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
                else if (BottomRight.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

                else if (RectTop.Contains(cursor)) message.Result = (IntPtr)HTTOP;
                else if (RectLeft.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
                else if (RectRight.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
                else if (RectBottom.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
            }
        }
        #endregion

        private void frmCropShotViewer_MouseDown(object sender, MouseEventArgs e)
        {
            _Moving = true;
            _MouseStartPos = e.Location;
        }

        void LoadImage()
        {
            this.BackgroundImage = _Image;
            this.Size = _Image.Size;
            this.MaximumSize = _Image.Size;
            this.Location = _FormStartPos;
        }

        private void cropImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmImageEditor(_OriginalImage);
            this.Hide();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                _Image = frm.Image;
                LoadImage();
            }

            this.Show();
        }

        private void frmCropShotViewer_MouseMove(object sender, MouseEventArgs e)
        {
            if (_Moving)
            {
                this.Location = new Point(
                    (this.Location.X - _MouseStartPos.X) + e.X,
                    (this.Location.Y - _MouseStartPos.Y) + e.Y);
            }
        }

        private void frmCropShotViewer_MouseUp(object sender, MouseEventArgs e)
        {
            _Moving = false;
        }

        private void frmCropShotViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void frmCropShotViewer_Load(object sender, EventArgs e)
        {
            LoadImage();
        }
        
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
