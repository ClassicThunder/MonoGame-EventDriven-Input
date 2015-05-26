using System;

namespace Microsoft.Xna.Framework.Input
{
    /// <summary>
    /// An EventArgs object that represents mouse events specific to buttons, their presses, clicks, and releases.
    /// </summary>
    public class SiMouseWheelEventArgs : SiMouseEventArgs
    {      
        /// <summary>
        /// Creates a new MouseWheelEventArgs object given a time, the previous and current mouse states, and
        /// the delta and value of the mouse wheel.
        /// </summary>
        public SiMouseWheelEventArgs(int x, int y, TimeSpan time, MouseState previous, MouseState current, int delta, int value)
            : base(x, y, time, previous, current) 
        {
            Button = MouseButton.None;

            Delta = delta;
            Value = value;
        }
    }
}
