using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombat.Graphics
{
    public class FrameRateCounter
    {
        public int Framerate { get; private set; } = 0;

        private long unixStamp = 0;
        private int lastFrames = 0;

        public FrameRateCounter()
        {
            unixStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        public void FrameDrawn()
        {
            if (unixStamp == DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            {
                lastFrames++;
            }
            else
            {
                unixStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                Framerate = lastFrames;
                lastFrames = 1;
            }

        }
    }
}
