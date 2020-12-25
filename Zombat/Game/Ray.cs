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
        private double wallHitX;
        private double wallHitY;
        private double distance;
        private double angle;
        private bool rayFacingDown;
        private bool rayFacingRight;

        public Ray(float x, float y, double anglen)
        {
            this.x = x;
            this.y = y;
            angle = Game.NormalizeAngle(anglen);

            rayFacingDown = angle > 0 && angle < Math.PI;
            rayFacingRight = angle < Math.PI / 2 || angle > Math.PI * 3 / 2;
        }

        public void Cast(Map map, System.Drawing.Graphics g)
        {
            var foundHorizontalWallHit = false;
            var wallHitX = 0d;
            var wallHitY = 0d;
            
            var yIntersect = Math.Floor(y / map.BlockSize) * map.BlockSize;
            yIntersect += rayFacingDown ? map.BlockSize : 0;
            
            var xIntersect = x + (yIntersect - y) / Math.Tan(angle);

            var yStep = map.BlockSize;
            yStep *= rayFacingDown ? 1 : -1;
            var xStep = map.BlockSize / Math.Tan(angle);
            xStep *= (!rayFacingRight && xStep > 0) ? -1 : 1;
            xStep *= (rayFacingRight && xStep < 0) ? -1 : 1;

            var nextHTouchX = xIntersect;
            var nextHTouchY = yIntersect;

            if (!rayFacingDown)
                nextHTouchY--;

            while (nextHTouchX >= 0 && nextHTouchX <= map.TotalWidth && nextHTouchY >= 0 && nextHTouchY <= map.TotalHeight)
            {
                if (map.HasWall((float) nextHTouchX, (float) nextHTouchY))
                {
                    foundHorizontalWallHit = true;
                    wallHitX = nextHTouchX;
                    wallHitY = nextHTouchY;
                    
                    g.DrawLine(new Pen(Color.Blue), x, y, (float) wallHitX, (float) wallHitY);
                    break;
                }
                else
                {
                    nextHTouchY += yStep;
                    nextHTouchX += xStep;
                }
            }
        }

        public void Redraw(System.Drawing.Graphics g)
        {
            g.DrawLine(new Pen(Color.FromArgb(10, 45, 196, 39)), x, y, x + (float)Math.Cos(angle) * 50, y + (float)Math.Sin(angle) * 50);
        }
    }
}
