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

        public void Redraw(Bitmap bitmap)
        {
            _graphicsBuffer.Graphics.Clear(_color);
            _graphicsBuffer.Graphics.DrawImage(bitmap, new Point(0, 0));
            _graphicsBuffer.Render(_panelGraphics);
        }
    }
}
