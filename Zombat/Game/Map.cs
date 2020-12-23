using System.Drawing;

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
    }
}
