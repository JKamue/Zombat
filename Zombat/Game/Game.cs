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

        private readonly double _fovAngle = 60 * (Math.PI / 180);
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
