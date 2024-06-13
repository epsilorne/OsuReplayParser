using OsuReplayParser.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuReplayParser.Objects
{
    public class ReplayFrame
    {
        public long MsSinceStart { get; set; }
        public long MsSincePreviousFrame { get; set; }
        public float X { get; set; }
        public float Y { get; set; }

        public List<Keypress> KeysPressed { get; set; }

        public ReplayFrame(long msSinceStart, long msSincePreviousFrame, float x, float y, int keysPressedEncoding)
        {
            MsSinceStart = msSinceStart;
            MsSincePreviousFrame = msSincePreviousFrame;
            X = x;
            Y = y;
            KeysPressed = Keys.GetKeysPressed(keysPressedEncoding);
        }

        public override string ToString()
        {
            return "[At Time: " + MsSinceStart + "ms, at (" + X + ", " + Y + "), keys (" + string.Join(", ", KeysPressed) + ") pressed.]";
        }
    }
}
