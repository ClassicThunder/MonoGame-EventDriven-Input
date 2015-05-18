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
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Gets or sets the previous mouse state for the given event. This is what the mouse looked like
        /// in the previous Update.
        /// </summary>
        public MouseState Previous { get; protected set; }

        /// <summary>
        /// Gets or sets the current mouse state for the given event. This is what the mouse looked like
        /// at the time the event occurred.
        /// </summary>
        public MouseState Current { get; protected set; }

        /// <summary>
        /// Creates a new MouseEventArgs object, based on a time for the event, and the previous and
        /// current mouse states.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="previous"></param>
        /// <param name="current"></param>
        public SiMouseEventArgs(TimeSpan time, MouseState previous, MouseState current)
        {
            Previous = previous;
            Current = current;
        }
    }
}
