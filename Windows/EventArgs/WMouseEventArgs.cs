using System;

namespace Microsoft.Xna.Framework.Input
{
    public delegate void WMouseEventHandler(object sender, WMouseEventArgs e);

    public class WMouseEventArgs : EventArgs
    {
        private MouseButton button;
        private int clicks;
        private int x;
        private int y;
        private int delta;

        public MouseButton Button 
        {
            get { return button; }
        }

        public int Clicks 
        {
            get { return clicks; }
        }

        public int X 
        {
            get { return x; }
        }

        public int Y 
        {
            get { return y; }
        }

        public Point Location 
        {
            get { return new Point(x, y); }
        }

        public int Delta 
        {
            get { return delta; }
        }

        public WMouseEventArgs(MouseButton button, int clicks, int x, int y, int delta)
        {
            this.button = button;
            this.clicks = clicks;
            this.x = x;
            this.y = y;
            this.delta = delta;
        }
    }
}
