using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zombat.Graphics;

namespace Zombat
{
    public partial class GameForm : Form
    {
        private readonly BufferedScreenController _screenController;
        private readonly DirectBitmap _bitmap;
        private readonly FrameRateCounter _frameCounter;
        private readonly Timer _timer;
        
        public GameForm()
        {
            InitializeComponent();
            _screenController = new BufferedScreenController(pnlGame, Color.White);
            _bitmap = new DirectBitmap(pnlGame.Width, pnlGame.Height);
            _frameCounter = new FrameRateCounter();

            _timer = new Timer();
            _timer.Interval = 10;
            _timer.Tick += Redraw;
            _timer.Start();
        }

        private void Redraw(object sender, EventArgs e)
        {
            _bitmap.Clear();
            _bitmap.SetVLine(50, 20, 100, MakeArgb(255, 120, 250, 100));
            _screenController.Redraw(_bitmap.Bitmap);
            
            _frameCounter.FrameDrawn();
            Text = $@"Zombat - {_frameCounter.Framerate} fps";
        }

        private static int MakeArgb(byte alpha, byte red, byte green, byte blue) => (int)((red << 16 | green << 8 | blue | alpha << 24) & uint.MaxValue);
    }
}
