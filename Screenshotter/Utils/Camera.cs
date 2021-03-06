﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screenshotter.Utils
{
    public static class Camera
    {
        public static readonly string[] BannedFormats = { "MemoryBmp", "Icon" };

        public static Screen CaptureScreen { get; set; } = Screen.PrimaryScreen;

        public static Image TakeScreenshot(float scale = 1.0f)
        {
            Stopwatch sw = Stopwatch.StartNew();

            var size = GetRealScreenshotSize();
            var bmpScreenshot = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            
            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            
            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(
                CaptureScreen.Bounds.X,
                CaptureScreen.Bounds.Y,
                0,
                0,
                size,
                CopyPixelOperation.SourceCopy);

            Bitmap ret = null;

            if (Math.Abs(scale - 1.0f) < float.Epsilon) //scale ~= 1.0f
            {
                ret = bmpScreenshot;
            }
            else
            {
                int w = (int)(size.Width * scale);
                int h = (int)(size.Height * scale);
                ret = ResizeImage(bmpScreenshot, w, h);
            }

            sw.Stop();
            Debug.WriteLine("Screenshot time: " + sw.Elapsed);

            return ret;
        }

        private static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static Size GetRealScreenshotSize()
        {
            return CaptureScreen.Bounds.Size;
        }

        public static void PlaySound()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Screenshotter.shutter.wav"))
                using (var player = new SoundPlayer(stream))
                    player.Play();
        }

        public static string GenerateFileName(string format, DateTime? date = null, bool extension = true)
        {
            //Screenshot220917-2302
            string ext = "." + format.ToLower();
            if (date == null)
                date = DateTime.Now;

            return "Screenshot" + date.Value.ToString(@"yyyyMMdd\-HHmmss") + (extension ? ext : "");
        }

        public static IEnumerable<string> GetAvailableFormats()
        {
            return typeof(ImageFormat).GetProperties()
                .Where(o => o.GetMethod.IsStatic)
                .Select(o => o.Name)
                .Where(o => !BannedFormats.Contains(o))
                .OrderBy(o => o);
        }
    }
}
