using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zombat.Graphics;

namespace Zombat.Game
{
    class Ray
    {
        private readonly float _x;
        private readonly float _y;
        public double WallHitX;
        public double WallHitY;
        public double Distance;
        private bool wasHitVertical;
        public double Angle;
        private readonly bool _rayFacingDown;
        private readonly bool _rayFacingRight;
        private Map _map;

        public Ray(float x, float y, double anglen)
        {
            _x = x;
            _y = y;
            Angle = Game.NormalizeAngle(anglen);

            _rayFacingDown = Angle > 0 && Angle < Math.PI;
            _rayFacingRight = Angle < Math.PI / 2 || Angle > Math.PI * 3 / 2;
        }

        public void Cast(Map map, System.Drawing.Graphics g)
        {
            var foundHorizontalWallHit = false;
            var wallHitXH = 0d;
            var wallHitYH = 0d;
            
            var yIntersect = Math.Floor(_y / map.BlockSize) * map.BlockSize;
            yIntersect += _rayFacingDown ? map.BlockSize : 0;
            
            var xIntersect = _x + (yIntersect - _y) / Math.Tan(Angle);

            var yStep = (double) map.BlockSize;
            yStep *= _rayFacingDown ? 1 : -1;
            var xStep = map.BlockSize / Math.Tan(Angle);
            xStep *= (!_rayFacingRight && xStep > 0) ? -1 : 1;
            xStep *= (_rayFacingRight && xStep < 0) ? -1 : 1;

            var nextHTouchX = xIntersect;
            var nextHTouchY = yIntersect;

            while (nextHTouchX >= 0 && nextHTouchX <= map.TotalWidth && nextHTouchY >= 0 && nextHTouchY <= map.TotalHeight)
            {
                if (map.HasWall((float) nextHTouchX, (float) nextHTouchY - (_rayFacingDown ? 0 : 1)))
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

            xIntersect = Math.Floor(_x / map.BlockSize) * map.BlockSize;
            xIntersect += _rayFacingRight ? map.BlockSize : 0;

            yIntersect = _y + (xIntersect - _x) * Math.Tan(Angle);

            xStep = map.BlockSize;
            xStep *= _rayFacingRight ? 1 : -1;
            
            yStep = map.BlockSize * Math.Tan(Angle);
            yStep *= (!_rayFacingDown && yStep > 0) ? -1 : 1;
            yStep *= (_rayFacingDown && yStep < 0) ? -1 : 1;

            var nextVTouchX = xIntersect;
            var nextVTouchY = yIntersect;

            while (nextVTouchX >= 0 && nextVTouchX <= map.TotalWidth && nextVTouchY >= 0 && nextVTouchY <= map.TotalHeight)
            {
                if (map.HasWall((float)nextVTouchX - (_rayFacingRight ? 0 : 1), (float)nextVTouchY))
                {
                    foundVerticalWallHit = true;
                    wallHitXV = nextVTouchX;
                    wallHitYV = nextVTouchY;
                    break;
                }

                nextVTouchY += yStep;
                nextVTouchX += xStep;
            }
            
            var hDist = (foundHorizontalWallHit) ? CalcDistance(_x,_y, wallHitXH, wallHitYH) : int.MaxValue;
            var vDist = (foundVerticalWallHit) ? CalcDistance(_x,_y, wallHitXV, wallHitYV) : int.MaxValue;

            if (hDist < vDist)
            {
                Distance = hDist;
                WallHitX = wallHitXH;
                WallHitY = wallHitYH;
                wasHitVertical = false;
            }
            else
            {
                Distance = vDist;
                WallHitX = wallHitXV;
                WallHitY = wallHitYV;
                wasHitVertical = true;
            }
         
            g.DrawLine(new Pen(Color.Blue), _x, _y, (float) WallHitX, (float)WallHitY);
        }
        
        private double CalcDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }
    }
}
