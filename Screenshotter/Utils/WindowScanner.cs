using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Screenshotter.Utils
{
    public static class WindowScanner
    {
        public struct Window
        {
            public Rectangle Bounds;
            public string Text;
            public IntPtr RawPtr;

            public Window(Rectangle Bounds, string Text, IntPtr RawPtr)
            {
                this.Bounds = Bounds;
                this.Text = Text;
                this.RawPtr = RawPtr;
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hWnd, out Rectangle lpRect);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("USER32.DLL")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        
        public static IEnumerable<IntPtr> FindWindows()
        {
            IntPtr found = IntPtr.Zero;
            List<IntPtr> windows = new List<IntPtr>();

            EnumWindows(delegate (IntPtr wnd, IntPtr param)
            {
                windows.Add(wnd);
                return true;
            }, IntPtr.Zero);

            return windows;
        }

        public static IEnumerable<Window> GetAllWindows()
        {
            var wins = FindWindows();

            foreach (var item in wins)
            {
                Rectangle rect = Rectangle.Empty;
                if (GetWindowRect(item, out rect))
                {
                    var str = new StringBuilder(GetWindowTextLength(item) + 1);
                    var len = GetWindowText(item, str, 255);
                    string text = str.ToString();
                    bool visible = IsWindowVisible(item);

                    if (len > 0 && visible)
                        yield return new Window(rect, text, item);
                }
            }
        }

        /// <summary>
        /// Gets the z-order for one or more windows atomically with respect to each other. In Windows, smaller z-order is higher. If the window is not top level, the z order is returned as -1. 
        /// </summary>
        public static int[] GetZOrder(params IntPtr[] hWnds)
        {
            var z = new int[hWnds.Length];
            for (var i = 0; i < hWnds.Length; i++) z[i] = -1;

            var index = 0;
            var numRemaining = hWnds.Length;
            EnumWindows((wnd, param) =>
            {
                var searchIndex = Array.IndexOf(hWnds, wnd);
                if (searchIndex != -1)
                {
                    z[searchIndex] = index;
                    numRemaining--;
                    if (numRemaining == 0) return false;
                }
                index++;
                return true;
            }, IntPtr.Zero);

            return z;
        }
    }
}
