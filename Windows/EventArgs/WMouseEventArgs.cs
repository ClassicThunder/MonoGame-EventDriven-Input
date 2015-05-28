namespace Microsoft.Xna.Framework.Input
{
    public class WMouseEventArgs : MouseEventArgs
    {
        public int Clicks { get; private set; }

        public WMouseEventArgs(int x, int y, MouseButton button, int clicks, int? value, int? delta)
            : base(x, y, button, value, delta)
        {
            Clicks = clicks;
        }
    }
}
