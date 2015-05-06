using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Input
{
    /// <summary>
    /// An abstraction around mouse input that turns XNA's underlying polling model into an event-based
    /// model for mouse input.
    /// </summary>
    public class MouseEvents
    {
        /// <summary>
        /// Stores the previous mouse state for comparision later.
        /// </summary>
        private MouseState _previous;

        /// <summary>
        /// Stores information about when the last click was for the purposes of handling double clicks.
        /// </summary>
        private readonly Dictionary<MouseButton, MouseButtonEventArgs> _lastClicks;

        /// <summary>
        /// Stores information about when the last double click was for the purposes of handling triple
        /// clicks.
        /// </summary>
        private readonly Dictionary<MouseButton, MouseButtonEventArgs> _lastDoubleClicks;

        /// <summary>
        /// The maximum amount of time allowed between clicks for it to count as a double-click. Measured in
        /// milliseconds. Defaults to 300 milliseconds.
        /// </summary>
        public int DoubleClickTime { get; set; }

        /// <summary>
        /// The maximum amount that the cursor can move (in pixels) and still count as a double-click.
        /// Defaults to 2.
        /// </summary>
        public int DoubleClickMaxMove { get; set; }

        /// <summary>
        /// Indicates whether a MouseMoved event is raised even when being dragged. If set to <code>false</code>,
        /// moves and drags will be treated as separate events, with moves occurring when no button is pressed,
        /// and drags occurring when a button is pressed. If set to <code>true</code>, moves will be relayed
        /// any time the mouse moves, regardless if whether a button is pressed or not, and drags will still
        /// only occur when a button is pressed. In this case, a MouseMoved and MouseDragged event will both
        /// be raised.
        /// </summary>
        public bool MoveRaisedOnDrag { get; set; } 

        /// <summary>
        /// Creates a new MouseEvents object.
        /// </summary>
        public MouseEvents()
        {
            DoubleClickTime = 300;
            DoubleClickMaxMove = 2;
            MoveRaisedOnDrag = true;

            _lastClicks = new Dictionary<MouseButton, MouseButtonEventArgs>();
            _lastDoubleClicks = new Dictionary<MouseButton, MouseButtonEventArgs>();

            _lastClicks.Add(MouseButton.Left, new MouseButtonEventArgs(new TimeSpan(-1, 0, 0), new MouseState(), new MouseState(), MouseButton.Left));
            _lastClicks.Add(MouseButton.Right, new MouseButtonEventArgs(new TimeSpan(-1, 0, 0), new MouseState(), new MouseState(), MouseButton.Right));
            _lastClicks.Add(MouseButton.Middle, new MouseButtonEventArgs(new TimeSpan(-1, 0, 0), new MouseState(), new MouseState(), MouseButton.Middle));
            _lastClicks.Add(MouseButton.XButton1, new MouseButtonEventArgs(new TimeSpan(-1, 0, 0), new MouseState(), new MouseState(), MouseButton.XButton1));
            _lastClicks.Add(MouseButton.XButton2, new MouseButtonEventArgs(new TimeSpan(-1, 0, 0), new MouseState(), new MouseState(), MouseButton.XButton2));
            
            _lastDoubleClicks.Add(MouseButton.Left, new MouseButtonEventArgs(new TimeSpan(-1, 0, 0), new MouseState(), new MouseState(), MouseButton.Left));
            _lastDoubleClicks.Add(MouseButton.Right, new MouseButtonEventArgs(new TimeSpan(-1, 0, 0), new MouseState(), new MouseState(), MouseButton.Right));
            _lastDoubleClicks.Add(MouseButton.Middle, new MouseButtonEventArgs(new TimeSpan(-1, 0, 0), new MouseState(), new MouseState(), MouseButton.Middle));
            _lastDoubleClicks.Add(MouseButton.XButton1, new MouseButtonEventArgs(new TimeSpan(-1, 0, 0), new MouseState(), new MouseState(), MouseButton.XButton1));
            _lastDoubleClicks.Add(MouseButton.XButton2, new MouseButtonEventArgs(new TimeSpan(-1, 0, 0), new MouseState(), new MouseState(), MouseButton.XButton2));
        }

        /// <summary>
        /// Allows this component to handle polling and raise any and all mouse events that have occurred
        /// since the last update.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            var current = Mouse.GetState();
            
            // Check button press events.
            if (current.LeftButton == ButtonState.Pressed && _previous.LeftButton == ButtonState.Released) { OnButtonPressed(this, new MouseButtonEventArgs(gameTime.TotalGameTime, _previous, current, MouseButton.Left)); }
            if (current.MiddleButton == ButtonState.Pressed && _previous.MiddleButton == ButtonState.Released) { OnButtonPressed(this, new MouseButtonEventArgs(gameTime.TotalGameTime, _previous, current, MouseButton.Middle)); }
            if (current.RightButton == ButtonState.Pressed && _previous.RightButton == ButtonState.Released) { OnButtonPressed(this, new MouseButtonEventArgs(gameTime.TotalGameTime, _previous, current, MouseButton.Right)); }
            if (current.XButton1 == ButtonState.Pressed && _previous.XButton1 == ButtonState.Released) { OnButtonPressed(this, new MouseButtonEventArgs(gameTime.TotalGameTime, _previous, current, MouseButton.XButton1)); }
            if (current.XButton2 == ButtonState.Pressed && _previous.XButton2 == ButtonState.Released) { OnButtonPressed(this, new MouseButtonEventArgs(gameTime.TotalGameTime, _previous, current, MouseButton.XButton2)); }

            // Check button click events.
            if (current.LeftButton == ButtonState.Pressed && _previous.LeftButton == ButtonState.Released) { OnButtonClicked(this, new MouseButtonEventArgs(gameTime.TotalGameTime, _previous, current, MouseButton.Left)); }
            if (current.MiddleButton == ButtonState.Pressed && _previous.MiddleButton == ButtonState.Released) { OnButtonClicked(this, new MouseButtonEventArgs(gameTime.TotalGameTime, _previous, current, MouseButton.Middle)); }
            if (current.RightButton == ButtonState.Pressed && _previous.RightButton == ButtonState.Released) { OnButtonClicked(this, new MouseButtonEventArgs(gameTime.TotalGameTime, _previous, current, MouseButton.Right)); }
            if (current.XButton1 == ButtonState.Pressed && _previous.XButton1 == ButtonState.Released) { OnButtonClicked(this, new MouseButtonEventArgs(gameTime.TotalGameTime, _previous, current, MouseButton.XButton1)); }
            if (current.XButton2 == ButtonState.Pressed && _previous.XButton2 == ButtonState.Released) { OnButtonClicked(this, new MouseButtonEventArgs(gameTime.TotalGameTime, _previous, current, MouseButton.XButton2)); }

            // Check button releases.
            if (current.LeftButton == ButtonState.Released && _previous.LeftButton == ButtonState.Pressed) { OnButtonReleased(this, new MouseButtonEventArgs(gameTime.TotalGameTime, _previous, current, MouseButton.Left)); }
            if (current.MiddleButton == ButtonState.Released && _previous.MiddleButton == ButtonState.Pressed) { OnButtonReleased(this, new MouseButtonEventArgs(gameTime.TotalGameTime, _previous, current, MouseButton.Middle)); }
            if (current.RightButton == ButtonState.Released && _previous.RightButton == ButtonState.Pressed) { OnButtonReleased(this, new MouseButtonEventArgs(gameTime.TotalGameTime, _previous, current, MouseButton.Right)); }
            if (current.XButton1 == ButtonState.Released && _previous.XButton1 == ButtonState.Pressed) { OnButtonReleased(this, new MouseButtonEventArgs(gameTime.TotalGameTime, _previous, current, MouseButton.XButton1)); }
            if (current.XButton2 == ButtonState.Released && _previous.XButton2 == ButtonState.Pressed) { OnButtonReleased(this, new MouseButtonEventArgs(gameTime.TotalGameTime, _previous, current, MouseButton.XButton2)); }

            // Whether ANY button is pressed.
            bool buttonDown = current.LeftButton == ButtonState.Pressed ||
                              current.MiddleButton == ButtonState.Pressed ||
                              current.RightButton == ButtonState.Pressed ||
                              current.XButton1 == ButtonState.Pressed ||
                              current.XButton2 == ButtonState.Pressed;

            // Check for any sort of mouse movement. If a button is down, it's a drag,
            // otherwise it's a move.
            if (_previous.X != current.X || _previous.Y != current.Y)
            {
                if (buttonDown) { OnMouseDragged(this, new MouseEventArgs(gameTime.TotalGameTime, _previous, current)); }
                
                if(MoveRaisedOnDrag || !buttonDown) { OnMouseMoved(this, new MouseEventArgs(gameTime.TotalGameTime, _previous, current)); }
            }

            // Handle mouse wheel events.
            if (_previous.ScrollWheelValue != current.ScrollWheelValue)
            {
                int value = current.ScrollWheelValue;
                int delta = current.ScrollWheelValue - _previous.ScrollWheelValue;
                OnMouseWheelMoved(this, new MouseWheelEventArgs(gameTime.TotalGameTime, _previous, current, delta, value));
            }

            _previous = current;
        }

        /// <summary>
        /// Raises the ButtonReleased event. This is done automatically by a correctly configured component,
        /// but this is exposed publicly to allow programmatic button release events to occur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnButtonReleased(object sender, MouseButtonEventArgs args)
        {
            if (ButtonReleased != null) { ButtonReleased(sender, args); }
        }

        /// <summary>
        /// Raises the ButtonClicked event. This is done automatically by a correctly configured component,
        /// but this is exposed publicly to allow programmatic button click events to occur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnButtonClicked(object sender, MouseButtonEventArgs args)
        {
            // If this click is within the right time and position of the last click, raise a
            // double-click event as well.
            var lastClick = _lastClicks[args.Button].Time;
            if ((args.Time - lastClick).TotalMilliseconds < DoubleClickTime &&
                DistanceBetween(args.Current, _lastClicks[args.Button].Current) < DoubleClickMaxMove)
            {
                OnButtonDoubleClicked(sender, args);
                _lastDoubleClicks[args.Button] = args;
            }

            _lastClicks[args.Button] = args;
            if (ButtonClicked != null) { ButtonClicked(sender, args); }
        }

        /// <summary>
        /// Calculates the Manhattan distance between two mouse positions.
        /// </summary>
        private static int DistanceBetween(MouseState a, MouseState b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        /// <summary>
        /// Raises the ButtonPressed event. This is done automatically by a correctly configured component,
        /// but this is exposed publicly to allow programmatic button press events to occur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnButtonPressed(object sender, MouseButtonEventArgs args)
        {
            if (ButtonPressed != null) { ButtonPressed(sender, args); }
        }

        /// <summary>
        /// Raises the ButtonDoubleClicked event. This is done automatically by a correctly configured component,
        /// but this is exposed publicly to allow programmatic button double-click events to occur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnButtonDoubleClicked(object sender, MouseButtonEventArgs args)
        {
            if (ButtonDoubleClicked != null) { ButtonDoubleClicked(sender, args); }
        }

        /// <summary>
        /// Raises the MouseMoved event. This is done automatically by a correctly configured component,
        /// but this is exposed publicly to allow programmatic mouse move events to occur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnMouseMoved(object sender, MouseEventArgs args)
        {
            if (MouseMoved != null) { MouseMoved(sender, args); }
        }

        /// <summary>
        /// Raises the MouseDragged event. This is done automatically by a correctly configured component,
        /// but this is exposed publicly to allow programmatic mouse drag events to occur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnMouseDragged(object sender, MouseEventArgs args)
        {
            if (MouseDragged != null) { MouseDragged(sender, args); }
        }

        /// <summary>
        /// Raises the MouseWheelMoved event. This is done automatically by a correctly configured component,
        /// but this is exposed publicly to allow programmatic mouse wheel events to occur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnMouseWheelMoved(object sender, MouseWheelEventArgs args)
        {
            if (MouseWheelMoved != null) { MouseWheelMoved(sender, args); }
        }

        /// <summary>
        /// An event that is raised whenever any mouse button is released. The specific button, as well as
        /// other relevant information, can be looked up through the MouseButtonEventArgs parameter.
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> ButtonReleased;

        /// <summary>
        /// An event that is raised whenever any mouse button is pressed. The specific button, as well as
        /// other relevant information, can be looked up through the MouseButtonEventArgs parameter.
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> ButtonPressed;

        /// <summary>
        /// An event that is raised whenever any mouse button is clicked. The specific button, as well as
        /// other relevant information, can be looked up through the MouseButtonEventArgs parameter.
        /// In the default implementation, clicks occur at the same time as presses (when the button
        /// actually gets pressed in) but it represents a different conceptual model, and other 
        /// implementations may not define clicks in the same way.
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> ButtonClicked;

        /// <summary>
        /// An event that is raised whenever two button clicks occur in the same spot in a short period
        /// of time. The tolerance allowed for defining "same spot" can be configured through 
        /// MouseEvents.DoubleClickMaxMove, while the tolerance for the time period can be configured through
        /// MouseEvents.DoubleClickTime.
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> ButtonDoubleClicked;

        /// <summary>
        /// An event that is raised whenever the mouse moves.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseMoved;

        /// <summary>
        /// An event that is raised whenever the mouse is dragged (a mouse move with any button pressed).
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseDragged;

        /// <summary>
        /// An event that is raised whenever the mouse wheel is rotated.
        /// </summary>
        public event EventHandler<MouseWheelEventArgs> MouseWheelMoved;
    }
}
