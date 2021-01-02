using System;
using System.Drawing;

namespace Zombat.Game
{
    class Player
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public double Rotation { get; private set; } = Math.PI / 2;

        private readonly float _moveSpeed = 0.5f;
        private readonly double _rotationSpeed = 5 * (Math.PI / 180);
        
        private readonly Map _map;
        
        public Player(Map map)
        {
            X = map.Spawn.X;
            Y = map.Spawn.Y;
            _map = map;
        }

        public void Redraw(System.Drawing.Graphics g)
        {
            Update();
            g.FillEllipse(new SolidBrush(Color.Red), X-3, Y-3, 6,6);
            g.DrawLine(new Pen(Color.Black), X, Y, X + (float) Math.Cos(Rotation) * 16, Y + (float)Math.Sin(Rotation) * 16);
        }

        public void Update()
        {
            var direction = 0;
            direction += KeyStatus.IsPressed(83) ? -1 : 0;
            direction += KeyStatus.IsPressed(87) ? 1 : 0;


            var rotation = 0;
            rotation += KeyStatus.IsPressed(68) ? 1 : 0;
            rotation += KeyStatus.IsPressed(65) ? -1 : 0;

            Rotation += rotation * _rotationSpeed;

            var step = direction * _moveSpeed;
            var newX = X + (float)Math.Cos(Rotation) * step;
            var newY = Y + (float)Math.Sin(Rotation) * step;

            if (_map.HasWall(newX, newY) == 0)
            {
                X += (float) Math.Cos(Rotation) * step;
                Y += (float) Math.Sin(Rotation) * step;
            }
        }
    }
}
