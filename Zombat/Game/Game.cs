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
            _player = new Player(map);
            _bitmap = bitmap;
            _miniMap = miniMap;
            _bufferedScreen = new BufferedScreenController(miniMap, Color.White);
            miniMap.Size = map.GetSize();
        }

        public void Redraw()
        {
            var minimap = _bufferedScreen.StartDrawing();
            _map.Redraw(minimap);
            _player.Redraw(minimap);
            _bufferedScreen.FinishDrawing();
        }
    }
}
