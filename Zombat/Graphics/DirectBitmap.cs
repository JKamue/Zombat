using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Zombat.Graphics
{
    /**
     * Code by A.Konzel - https://github.com/saxxonpike
     * https://stackoverflow.com/a/34801225
     */

    public class DirectBitmap : IDisposable
    {
        public Bitmap Bitmap { get; private set; }
        public Int32[] Bits { get; private set; }
        public bool Disposed { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        protected GCHandle BitsHandle { get; private set; }

        public DirectBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new Int32[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }

        public void SetPixel(int x, int y, Color color)
        {
            int col = color.ToArgb();
            SetPixel(x, y, col);
        }
        public void SetPixel(int x, int y, int color)
        {
            int index = x + (y * Width);
            SetPixel(index, color);
        }

        public void SetVLine(int x, int start, int length, int color)
        {
            for (var y = start; y < length; y++)
            {
                SetPixel(x,y,color);
            }
        }
        
        public void SetPixel(int index, int color)
        {
            Bits[index] = color;
        }

        public Color GetPixel(int x, int y)
        {
            int index = x + (y * Width);
            int col = Bits[index];
            Color result = Color.FromArgb(col);

            return result;
        }

        public void Clear()
        {
            Bits = new Int32[Width * Height];
            BitsHandle.Free();
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(Width, Height, Width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }

        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }
        
        private static int MakeArgb(byte alpha, byte red, byte green, byte blue) => (int)((red << 16 | green << 8 | blue | alpha << 24) & uint.MaxValue);
    }
}
