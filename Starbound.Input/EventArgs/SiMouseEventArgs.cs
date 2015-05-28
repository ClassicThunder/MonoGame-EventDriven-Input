using System;

namespace Microsoft.Xna.Framework.Input
{
    /// <summary>
    /// An EventArgs object that represents mouse events specific to buttons, their presses, clicks, and releases.
    /// </summary>
    public class SiMouseEventArgs : MouseEventArgs
    {
        /// <summary>
        /// Stores the time of the event as a TimeSpan since the game began.
        /// </summary>
        public TimeSpan Time { get; private set; }

        /// <summary>
        /// Gets or sets the previous mouse state for the given event. This is what the mouse looked like
        /// in the previous Update.
        /// </summary>
        public MouseState Previous { get; private set; }

        /// <summary>
        /// Gets or sets the current mouse state for the given event. This is what the mouse looked like
        /// at the time the event occurred.
        /// </summary>
        public MouseState Current { get; private set; }

        /// <summary>
        /// Creates a new MouseEventArgs object, based on a time for the event, and the previous and
        /// current mouse states.
        /// </summary>
        public SiMouseEventArgs(int x, int y, TimeSpan time, MouseState previous, MouseState current, 
            MouseButton button = MouseButton.None, int? value = null, int? delta = null)
            : base(x, y, button, value, delta)
        {
            X = x;
            Y = y;
            Time = time;
            Previous = previous;
            Current = current;
        }
    }
}
