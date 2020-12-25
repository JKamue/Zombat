using System;
using System.Drawing;
using System.Windows.Forms;
using Zombat.Game;
using Zombat.Graphics;

namespace Zombat
{
    public partial class GameForm : Form
    {
        private readonly BufferedScreenController _screenController;
        private readonly DirectBitmap _bitmap;
        private readonly FrameRateCounter _frameCounter;
        private readonly Timer _timer;
        private readonly Game.Game _game;
        
        public GameForm()
        {
            var mapObjects = new bool[,]
            {
                { true , true , true , true , true , true , true , true , true , true , true , true  },
                { true , false, false, false, false, false, false, false, false, true , false, true  },
                { true , false, false, false, false, true , false, false, false, true , false, true  },
                { true , true , true , true , false, false, false, true , false, true , false, true  },
                { true , false, false, false, false, false, true , true , false, true , false, true  },
                { true , false, false, false, false, false, false, false, false, true , false, true  },
                { true , true , true , false, true , false, true , true , false, true , false, true  },
                { true , false, false, false, true , false, true , true , false, false, false, true  },
                { true , false, true , true , true , false, true , true , true , false, false, true  },
                { true , false, false, false, false, false, false, false, false, false, false, true  },
                { true , true , true , true , true , true , true , true , true , true , true , true  }
            };
            var spawn = new Point(1, 1);
            var map = new Map(mapObjects, spawn);

            InitializeComponent();
            _screenController = new BufferedScreenController(pnlGame, Color.White);
            _bitmap = new DirectBitmap(pnlGame.Width, pnlGame.Height);
            _frameCounter = new FrameRateCounter();
            _game = new Game.Game(map, _bitmap, pnlMap);
            
            _timer = new Timer();
            _timer.Interval = 10;
            _timer.Tick += Redraw;
            _timer.Start();
        }

        private void Redraw(object sender, EventArgs e)
        {
            _bitmap.Clear();
            _game.Redraw();
            //_bitmap.SetVLine(50, 20, 100, MakeArgb(255, 120, 250, 100));
            _screenController.Redraw(_bitmap.Bitmap);
            
            _frameCounter.FrameDrawn();
            Text = $@"Zombat - {_frameCounter.Framerate} fps";
        }
    }
}
