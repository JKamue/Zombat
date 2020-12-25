using System.Diagnostics.Contracts;
using System.Drawing;
using System.Windows.Forms;

namespace Zombat.Game
{
    class Map
    {
        private bool[,] grid;
        public Point Spawn;

        public Map(bool[,] grid, Point spawn)
        {
            this.grid = grid;
            Spawn = spawn;
        }

        public void Redraw(System.Drawing.Graphics g, int width, int height)
        {
            g.Clear(Color.White);
            var cWidth = width / grid.GetLength(1);
            var cHeight = height / grid.GetLength(0);
            var brush = new SolidBrush(Color.Black);
            
            for (var y = 0; y < grid.GetLength(0); y++)
            {
                for (var x = 0; x < grid.GetLength(1); x++)
                {
                    if (grid[y,x])
                        g.FillRectangle(brush, x * cWidth, y * cHeight, cWidth, cHeight);
                }
            }
        }
    }
}
