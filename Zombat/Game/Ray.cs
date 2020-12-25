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
        private bool wasHitVertical;
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
            var wallHitXH = 0d;
            var wallHitYH = 0d;
            
            var yIntersect = Math.Floor(y / map.BlockSize) * map.BlockSize;
            yIntersect += rayFacingDown ? map.BlockSize : 0;
            
            var xIntersect = x + (yIntersect - y) / Math.Tan(angle);

            var yStep = (double) map.BlockSize;
            yStep *= rayFacingDown ? 1 : -1;
            var xStep = map.BlockSize / Math.Tan(angle);
            xStep *= (!rayFacingRight && xStep > 0) ? -1 : 1;
            xStep *= (rayFacingRight && xStep < 0) ? -1 : 1;

            var nextHTouchX = xIntersect;
            var nextHTouchY = yIntersect;

            while (nextHTouchX >= 0 && nextHTouchX <= map.TotalWidth && nextHTouchY >= 0 && nextHTouchY <= map.TotalHeight)
            {
                if (map.HasWall((float) nextHTouchX, (float) nextHTouchY - (rayFacingDown ? 0 : 1)))
                {
                    foundHorizontalWallHit = true;
                    wallHitXH = nextHTouchX;
                    wallHitYH = nextHTouchY;
                    break;
                }
                
                nextHTouchY += yStep;
                nextHTouchX += xStep;
            }
            
            
            
            var foundVerticalWallHit = false;
            var wallHitXV = 0d;
            var wallHitYV = 0d;

            xIntersect = Math.Floor(x / map.BlockSize) * map.BlockSize;
            xIntersect += rayFacingRight ? map.BlockSize : 0;

            yIntersect = y + (xIntersect - x) * Math.Tan(angle);

            xStep = map.BlockSize;
            xStep *= rayFacingRight ? 1 : -1;
            
            yStep = map.BlockSize * Math.Tan(angle);
            yStep *= (!rayFacingDown && yStep > 0) ? -1 : 1;
            yStep *= (rayFacingDown && yStep < 0) ? -1 : 1;

            var nextVTouchX = xIntersect;
            var nextVTouchY = yIntersect;

            while (nextVTouchX >= 0 && nextVTouchX <= map.TotalWidth && nextVTouchY >= 0 && nextVTouchY <= map.TotalHeight)
            {
                if (map.HasWall((float)nextVTouchX - (rayFacingRight ? 0 : 1), (float)nextVTouchY))
                {
                    foundVerticalWallHit = true;
                    wallHitXV = nextVTouchX;
                    wallHitYV = nextVTouchY;
                    break;
                }

                nextVTouchY += yStep;
                nextVTouchX += xStep;
            }
            
            var hDist = (foundHorizontalWallHit) ? Distance(x,y, wallHitXH, wallHitYH) : int.MaxValue;
            var vDist = (foundVerticalWallHit) ? Distance(x,y, wallHitXV, wallHitYV) : int.MaxValue;

            if (hDist < vDist)
            {
                distance = hDist;
                wallHitX = wallHitXH;
                wallHitY = wallHitYH;
                wasHitVertical = false;
            }
            else
            {
                distance = vDist;
                wallHitX = wallHitXV;
                wallHitY = wallHitYV;
                wasHitVertical = true;
            }
         
            g.DrawLine(new Pen(Color.Blue), x, y, (float) wallHitX, (float)wallHitY);
            
        }

        private double Distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }
    }
}
