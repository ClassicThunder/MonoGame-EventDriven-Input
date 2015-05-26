using System;

namespace Microsoft.Xna.Framework.Input
{
    public abstract class MouseEventArgs : EventArgs
    {
        /// <summary>
        /// The X coordinate of the mouse at the time of the event. 
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The Y coordinate of the mouse at the time of the event. 
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets the MouseButton associated with the event. 
        /// </summary>
        public MouseButton Button { get; protected set; }

        /// <summary>
        /// Gets thwe current wheel position. 
        /// </summary>
        public int? Value { get; protected set; }

        /// <summary>
        /// Gets or sets the change in mouse wheel position. While most mice will tend to consistently
        /// return the same value depending on the number of "clicks" of the mouse wheel, you should not
        /// assume that all mice use the same amount. Different mice will produce different deltas for 
        /// each notch of the mouse wheel, while others don't have notches, but rather, a continuous rotation.
        /// </summary>
        public int? Delta { get; protected set; }        
    }
}
