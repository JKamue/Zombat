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

        public Game(Map map, DirectBitmap bitmap, Panel miniMap)
        {
            _map = map;
            _player = new Player(_map.Spawn);
            _bitmap = bitmap;
            _miniMap = miniMap;
            _bufferedScreen = new BufferedScreenController(miniMap, Color.White);
        }

        public void Redraw()
        {
            _map.Redraw(_bufferedScreen.StartDrawing(), _miniMap.Width, _miniMap.Height);
            _bufferedScreen.FinishDrawing();
        }
    }
}
