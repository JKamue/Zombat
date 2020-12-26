using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Zombat.Graphics;

namespace Zombat.Game
{
    class Game
    {
        private readonly Map _map;
        private readonly Player _player;
        private readonly DirectBitmap _bitmap;
        private readonly Panel _miniMap;
        private readonly BufferedScreenController _bufferedScreen;

        private readonly double _fovAngle = 100 * (Math.PI / 180);
        private readonly double _stripWidth = 1;
        private readonly double _rayNum;
        private readonly List<Ray> _rays = new List<Ray>();
        
        public Game(Map map, DirectBitmap bitmap, Panel miniMap)
        {
            _map = map;
            _player = new Player(map);
            _bitmap = bitmap;
            _miniMap = miniMap;
            _bufferedScreen = new BufferedScreenController(miniMap, Color.White);
            
            miniMap.Size = map.GetSize();
            _rayNum = _bitmap.Bitmap.Width / _stripWidth;
        }

        public void CastAllRays(System.Drawing.Graphics g)
        {
            _rays.Clear();
            var rayAngle = _player.Rotation - _fovAngle / 2;
            
            for (var i = 0; i < _rayNum; i++)
            {
                _rays.Add(new Ray(_player.X, _player.Y, rayAngle));
                rayAngle += _fovAngle / _rayNum;
            }
            
            _rays.ForEach(r => r.Cast(_map, g));
        }
        
        public void Redraw()
        {
            var minimap = _bufferedScreen.StartDrawing();
            _map.Redraw(minimap);
            CastAllRays(minimap);
            _player.Redraw(minimap);
            _bufferedScreen.FinishDrawing();

            for (var i = 0; i < _rays.Count; i++)
                DrawRay(_rays[i], i);
        }
        
        public void DrawRay(Ray ray, int i)
        {
            var distanceProjectionPlane = ((float) _bitmap.Width / 2) / Math.Tan(_fovAngle / 2);
            var wallStripHeight = (_map.BlockSize / ray.Distance)*distanceProjectionPlane;
            var color = DirectBitmap.MakeArgb(255, 128, 128, 128);

            for (var n = 0; n < _stripWidth; n++)
            {
                var x = (int) Math.Round(i * _stripWidth - n);
                var start = (int) Math.Round((float) _bitmap.Height / 2 - wallStripHeight / 2);
                var length = (int) Math.Round(wallStripHeight);

                if (length > _bitmap.Height)
                {
                    length = _bitmap.Height;
                    start = 0;
                }

                _bitmap.SetVLine(x, start, length, color);
            }
        }

        public static double NormalizeAngle(double angle)
        {
            angle = angle % (2 * Math.PI);
            if (angle < 0)
                angle += 2 * Math.PI;

            return angle;
        }
    }
}
