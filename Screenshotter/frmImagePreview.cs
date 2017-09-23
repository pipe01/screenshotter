using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screenshotter
{
    public partial class frmImagePreview : Form
    {
        public Image Image;

        Image _OriginalImage;

        bool _Moving, _Selecting, _Selected, _Changes;

        Point _MovePrev = Point.Empty,
              _ImageLocation = Point.Empty,
              _SelPoint = Point.Empty;

        float _Scale = 1.0f;

        Rectangle _SelectionRect;

        Rectangle TempSelectionRect
        {
            get
            {
                var p1 = ImageToViewport(_MovePrev);
                var p2 = ImageToViewport(_SelPoint);

                return GetRectangle(p1, p2);
            }
        }

        public frmImagePreview(Image img)
        {
            InitializeComponent();
            this.MouseWheel += FrmImagePreview_MouseWheel;
            
            Image = img;
            _OriginalImage = img;
        }

        private void FrmImagePreview_MouseWheel(object sender, MouseEventArgs e)
        {
            int steps = e.Delta / 120;
            float scaleStep = 0.2f * steps;
            scaleStep *= _Scale;

            var mouse = ViewportToImage(e.Location);

            _Scale += scaleStep;

            if (_Scale < 0.2f)
            {
                _Scale = 0.2f;
                scaleStep = 0;
            }
            
            _ImageLocation = new Point(
                _ImageLocation.X - (int)(mouse.X * -scaleStep),
                _ImageLocation.Y - (int)(mouse.Y * -scaleStep));

            this.Refresh();
        }
        
        private void frmImagePreview_Paint(object sender, PaintEventArgs e)
        {
            var size = new Size((int)(Image.Width * _Scale), (int)(Image.Height * _Scale));
            var sourceRect = new Rectangle(
                (int)(_ImageLocation.X / _Scale),
                (int)(_ImageLocation.Y / _Scale),
                (int)(this.Width / _Scale),
                (int)(this.Height / _Scale));

            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.DrawImage(
                Image,
                new Rectangle(Point.Empty, this.Size),
                sourceRect,
                GraphicsUnit.Pixel);

            var pen = new Pen(Color.White, 1.5f)
            {
                DashStyle = DashStyle.DashDotDot
            };

            if (_Selecting)
            {
                e.Graphics.DrawRectangle(pen, TempSelectionRect);
            }
            else if (_Selected)
            {
                e.Graphics.DrawRectangle(pen, ImageToViewport(_SelectionRect));
            }
        }

        private void frmImagePreview_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                _Moving = true;
                _MovePrev = e.Location;
                this.Cursor = Cursors.SizeAll;
            }
            else if (e.Button == MouseButtons.Left)
            {
                _Selecting = true;
                _Selected = false;

                _MovePrev = ViewportToImage(e.Location);
                _SelPoint = _MovePrev;
            }
        }

        private void frmImagePreview_MouseMove(object sender, MouseEventArgs e)
        {
            if (_Moving && e.Button == MouseButtons.Middle)
            {
                int newLeft = e.X - _MovePrev.X;
                int newTop = e.Y - _MovePrev.Y;
                _MovePrev = e.Location;

                _ImageLocation = new Point(_ImageLocation.X - newLeft, _ImageLocation.Y - newTop);

                this.Refresh();
            }
            else if (_Selecting && e.Button == MouseButtons.Left)
            {
                _SelPoint = ViewportToImage(e.Location);
                this.Refresh();
            }
        }

        private void frmImagePreview_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                toolStripSeparator1.Visible = _Selected;
                imageToolStripMenuItem.Visible = _Selected;

                cmsRightClick.Show(this, e.Location);
                return;
            }

            _Moving = false;
            
            if (_Selecting)
            {
                _Selecting = false;
                if (_MovePrev.Equals(_SelPoint))
                {
                    _Selected = false;
                }
                else
                {
                    _Selected = true;
                    _SelectionRect = GetRectangle(_MovePrev, _SelPoint);
                }
            }
            
            _MovePrev = Point.Empty;
            _SelPoint = Point.Empty;
            
            this.Cursor = Cursors.Cross;

            this.Refresh();
        }

        Rectangle GetRectangle(Point p1, Point p2)
        {
            bool invertX = _MovePrev.X > _SelPoint.X;
            bool invertY = _MovePrev.Y > _SelPoint.Y;

            int x = invertX ? p2.X : p1.X;
            int y = invertY ? p2.Y : p1.Y;
            int w = invertX ? p1.X - p2.X : p2.X - p1.X;
            int h = invertY ? p1.Y - p2.Y : p2.Y - p1.Y;

            return new Rectangle(x, y, w, h);
        }
        Point ViewportToImage(Point p)
        {
            return new Point(
                (int)((p.X + _ImageLocation.X) / _Scale),
                (int)((p.Y + _ImageLocation.Y) / _Scale));
        }
        Point ImageToViewport(Point p)
        {
            return new Point(
                (int)((p.X * _Scale) - _ImageLocation.X),
                (int)((p.Y * _Scale) - _ImageLocation.Y));
        }
        Rectangle ImageToViewport(Rectangle rect)
        {
            return new Rectangle(
                ImageToViewport(rect.Location),
                new Size((int)(rect.Width * _Scale),
                         (int)(rect.Height * _Scale)));
        }

        void ResetZoom()
        {
            _Scale = 1.0f;

            int w = (int)(Image.Width * _Scale);
            int h = (int)(Image.Height * _Scale);
            
            _ImageLocation = new Point(
                -(this.Width / 2 - w / 2),
                -(this.Height / 2 - h / 2));

            this.Refresh();
        }

        private void frmImagePreview_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_Changes && e.CloseReason == CloseReason.UserClosing)
            {
                var result = taskDialog.ShowDialog();
                int btnIndex = taskDialog.Buttons.IndexOf(result);

                switch (btnIndex)
                {
                    case 0: //Save changes
                        break;
                    case 1: //Discard changes
                        this.Image = _OriginalImage;
                        break;
                    case 2: //Keep editing
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(Image);
        }

        private void cropToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _Changes = true;
            _Selected = false;

            Image cropped = Image.Crop(_SelectionRect);
            
            this.Image = cropped;

            ResetZoom();
        }
        
        private void resetZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetZoom();
        }
        
        private void frmImagePreview_MouseClick(object sender, MouseEventArgs e)
        {
            this.Refresh();
        }

        private void frmImagePreview_Load(object sender, EventArgs e)
        {
            ResetZoom();
        }
    }
}
