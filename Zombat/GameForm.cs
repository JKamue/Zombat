﻿using System;
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
            var mapObjects = new int[,]
            {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0 },
                { 0, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0 },
                { 0, 0, 0, 0, 1, 1, 1, 0, 1, 0, 1, 0 },
                { 0, 1, 1, 1, 1, 1, 0, 0, 1, 0, 1, 0 },
                { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0 },
                { 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0 },
                { 0, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0 },
                { 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0 },
                { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };
            var spawn = new Point(24, 24);
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

            KeyPreview = true;
            KeyDown += KeyStatus.KeyDownHandler;
            KeyUp += KeyStatus.KeyUpHander;
        }

        private void Redraw(object sender, EventArgs e)
        {
            _bitmap.Clear();
            _game.Redraw();
            
            var g = _screenController.StartDrawing();
            g.DrawImage(_bitmap.Bitmap, new Point(0, 0));
            _screenController.FinishDrawing();
            
            _frameCounter.FrameDrawn();
            Text = $@"Zombat - {_frameCounter.Framerate} fps";
        }
    }
}
