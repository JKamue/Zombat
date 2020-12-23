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
    public partial class Game : Form
    {
        private BufferedScreenController screenController;
        private DirectBitmap bitmap;
        private Timer timer;
        
        public Game()
        {
            InitializeComponent();
            screenController = new BufferedScreenController(pnlGame, Color.White);
            bitmap = new DirectBitmap(pnlGame.Width, pnlGame.Height);
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += Redraw;
            timer.Start();
        }

        private void Redraw(object sender, EventArgs e)
        {
            bitmap.Clear();
            bitmap.SetVLine(50, 20, 100, MakeArgb(255, 120, 250, 100));
            screenController.Redraw(bitmap.Bitmap);
        }

        private static int MakeArgb(byte alpha, byte red, byte green, byte blue) => (int)((red << 16 | green << 8 | blue | alpha << 24) & uint.MaxValue);
    }
}
