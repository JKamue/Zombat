using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombat.Game
{
    class Ray
    {
        private float x;
        private float y;
        private double angle;

        public Ray(float x, float y, double angle)
        {
            this.x = x;
            this.y = y;
            this.angle = angle;
        }

        public void Redraw(System.Drawing.Graphics g)
        {
            g.DrawLine(new Pen(Color.FromArgb(10, 45, 196, 39)), x, y, x + (float)Math.Cos(angle) * 50, y + (float)Math.Sin(angle) * 50);
        }
    }
}
