using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zombat.Graphics
{
    public class BufferedScreenController
    {
        private readonly Panel _panel;
        private readonly Color _color;

        private BufferedGraphicsContext _context;
        private BufferedGraphics _graphicsBuffer;
        private System.Drawing.Graphics _panelGraphics;

        public BufferedScreenController(Panel panel, Color color)
        {
            _panel = panel;
            _color = color;

            SetupGraphics();

            _panel.SizeChanged += PanelResizeEvent;
        }

        public void PanelResizeEvent(object sender, EventArgs e) => SetupGraphics();

        public void SetupGraphics()
        {
            _context = BufferedGraphicsManager.Current;
            _panelGraphics = _panel.CreateGraphics();
            _graphicsBuffer = _context.Allocate(_panelGraphics, _panel.DisplayRectangle);
        }

        public System.Drawing.Graphics StartDrawing()
        {
            _graphicsBuffer.Graphics.Clear(_color);
            return _graphicsBuffer.Graphics;
        }

        public void FinishDrawing()
        {
            _graphicsBuffer.Render(_panelGraphics);
        }
    }
}
