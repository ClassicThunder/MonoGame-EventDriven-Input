using System;

namespace Microsoft.Xna.Framework.Input
{
    /// <summary>
    /// An EventArgs object that represents mouse events specific to buttons, their presses, clicks, and releases.
    /// </summary>
    public class SiMouseButtonEventArgs : SiMouseEventArgs
    {
        /// <summary>
        /// Creates a new MouseButtonEventArgs object given a time, the previous and current mouse states, and
        /// the button that the event occurred with.
        /// </summary>
        public SiMouseButtonEventArgs(TimeSpan time, MouseState previous, MouseState current, MouseButton button)
            : base(time, previous, current)
        {
            Button = button;
            Delta = null;
        }
    }
}
