using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screenshotter
{
    public static class Extensions
    {
        public static Image Crop(this Image src, Rectangle rect) =>
            Crop(src, rect.X, rect.Y, rect.Width, rect.Height);
        public static Image Crop(this Image src, int x, int y, int w, int h)
        {
            Image img = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(img);
            g.DrawImage(src, new Rectangle(0, 0, w, h), new Rectangle(x, y, w, h), GraphicsUnit.Pixel);

            return img;
        }

        public static Rectangle ShrinkCenter(this Rectangle rect, int all)
        {
            return new Rectangle(
                rect.X + all / 2,
                rect.Y + all / 2,
                rect.Width - all / 2,
                rect.Height - all / 2);
        }
    }
}
