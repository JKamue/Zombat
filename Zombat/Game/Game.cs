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

        public Game(Map map, DirectBitmap bitmap, Panel miniMap)
        {
            _map = map;
            _player = new Player(_map.Spawn);
            _bitmap = bitmap;
            _miniMap = miniMap;
        }

        public void Redraw()
        {
            _map.Redraw(_miniMap);
        }
    }
}
