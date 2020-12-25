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
        
        public Player(PointF position)
        {
            X = position.X;
            Y = position.Y;
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
            X += (float) Math.Cos(Rotation) * step;
            Y += (float) Math.Sin(Rotation) * step;
        }
    }
}
