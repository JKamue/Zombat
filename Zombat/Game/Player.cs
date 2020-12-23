using System.Drawing;

namespace Zombat.Game
{
    class Player
    {
        public Point Position { get; private set; }

        public Player(Point position)
        {
            Position = position;
        }
    }
}
