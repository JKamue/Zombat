using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Windows.Forms;

namespace Zombat.Game
{
    class Map
    {
        private int[,] grid;
        public Point Spawn;
        public int BlockSize { get; private set; } = 16;

        public readonly int TotalWidth;
        public readonly int TotalHeight;
        
        public Map(int[,] grid, Point spawn)
        {
            this.grid = grid;
            Spawn = spawn;
            TotalWidth = grid.GetLength(1) * BlockSize;
            TotalHeight = grid.GetLength(0) * BlockSize;
        }

        public void Redraw(System.Drawing.Graphics g)
        {
            g.Clear(Color.White);
            var brush = new SolidBrush(Color.Black);
            for (var y = 0; y < grid.GetLength(0); y++)
            {
                for (var x = 0; x < grid.GetLength(1); x++)
                {
                    if (IsWall(x, y))
                        g.FillRectangle(brush, x * BlockSize, y * BlockSize, BlockSize, BlockSize);
                }
            }
        }

        public bool IsWall(int x, int y)
        {
            return grid[y, x] == 0;
        }
        
        public Size GetSize()
        {
            var width = grid.GetLength(1) * BlockSize;
            var height = grid.GetLength(0) * BlockSize;
            return new Size(width, height);
        }

        public bool HasWall(float x, float y)
        {
            if (x < 0 || x > TotalWidth || y < 0 || y > TotalHeight)
                return true;


            var mapGridIndexX = (int) Math.Round(Math.Floor(x / BlockSize));
            var mapGridIndexY = (int) Math.Round(Math.Floor(y / BlockSize));
            return IsWall(mapGridIndexX, mapGridIndexY);
        }
    }
}
