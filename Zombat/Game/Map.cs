using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Windows.Forms;

namespace Zombat.Game
{
    class Map
    {
        private readonly int[,] _grid;
        private readonly List<int> _colors;
        private readonly List<int> _vColors;
        
        
        public Point Spawn;
        public int BlockSize { get; private set; } = 16;

        public readonly int TotalWidth;
        public readonly int TotalHeight;
        
        public Map(int[,] grid, Point spawn, List<int> colors, List<int> vColors)
        {
            this._grid = grid;
            Spawn = spawn;
            TotalWidth = grid.GetLength(1) * BlockSize;
            TotalHeight = grid.GetLength(0) * BlockSize;
            _colors = colors;
            _vColors = vColors;
        }

        public void Redraw(System.Drawing.Graphics g)
        {
            g.Clear(Color.White);
            var brush = new SolidBrush(Color.Black);
            for (var y = 0; y < _grid.GetLength(0); y++)
            {
                for (var x = 0; x < _grid.GetLength(1); x++)
                {
                    if (IsWall(x, y))
                        g.FillRectangle(brush, x * BlockSize, y * BlockSize, BlockSize, BlockSize);
                }
            }
        }

        public bool IsWall(int x, int y)
        {
            return _grid[y, x] != 0;
        }
        
        public Size GetSize()
        {
            var width = _grid.GetLength(1) * BlockSize;
            var height = _grid.GetLength(0) * BlockSize;
            return new Size(width, height);
        }

        public int GetColor(int i, bool vert)
        {
            return vert ? _vColors[i] : _colors[i];
        }
        
        public int HasWall(float x, float y)
        {
            if (x < 0 || x > TotalWidth || y < 0 || y > TotalHeight)
                return 0;


            var mapGridIndexX = (int) Math.Round(Math.Floor(x / BlockSize));
            var mapGridIndexY = (int) Math.Round(Math.Floor(y / BlockSize));
            return _grid[mapGridIndexY, mapGridIndexX];
        }
    }
}
