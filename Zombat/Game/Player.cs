using System;
using System.Drawing;

namespace Zombat.Game
{
    class Player
    {
        public int x { get; private set; }
        public int y { get; private set; }
        public double Rotation { get; private set; } = Math.PI / 2;

        private int moveSpeed = 3;
        private double rotationSpeed = 3 * (Math.PI / 180);
        
        public Player(Point position)
        {
            x = position.X;
            y = position.Y;
            Rotation = 0;
        }

        public void Redraw(System.Drawing.Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Red), x-6, y-6, 13,13);
        }
    }
}
