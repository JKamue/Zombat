﻿using System.Diagnostics.Contracts;
using System.Drawing;
using System.Windows.Forms;

namespace Zombat.Game
{
    class Map
    {
        private bool[,] grid;
        public Point Spawn;
        public int BlockSize { get; private set; } = 16;

        public Map(bool[,] grid, Point spawn)
        {
            this.grid = grid;
            Spawn = spawn;
        }

        public void Redraw(System.Drawing.Graphics g)
        {
            g.Clear(Color.White);
            var brush = new SolidBrush(Color.Black);
            for (var y = 0; y < grid.GetLength(0); y++)
            {
                for (var x = 0; x < grid.GetLength(1); x++)
                {
                    if (grid[y, x])
                    {
                        g.FillRectangle(brush, x * BlockSize, y * BlockSize, BlockSize, BlockSize);
                        var tes3t = x * BlockSize;
                    }
                }
            }
        }
        
        public Size GetSize()
        {
            var width = grid.GetLength(1) * BlockSize + BlockSize;
            var height = grid.GetLength(0) * BlockSize + BlockSize;
            return new Size(width, height);
        }
    }
}
