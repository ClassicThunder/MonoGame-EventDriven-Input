﻿using System;

namespace Microsoft.Xna.Framework.Input
{
    /// <summary>
    /// An abstraction around mouse input that turns XNA's underlying polling model into an event-based
    /// model for mouse input.
    /// </summary>
    public class SiMouseEvents
    {
        /// <summary>
        /// Stores the previous mouse state for comparision later.
        /// </summary>
        private MouseState _previous;

        /// <summary>
        /// Stores information about when the last click was for the purposes of handling double clicks.
        /// </summary>
        private SiMouseEventArgs _lastClick;

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
        public SiMouseEvents()
        {
            DoubleClickTime = 300;
            DoubleClickMaxMove = 2;
            MoveRaisedOnDrag = true;

            _lastClick = new SiMouseEventArgs(
                -1, 
                -1,
                new TimeSpan(),
                Mouse.GetState(),
                Mouse.GetState());
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
            if (current.LeftButton == ButtonState.Pressed && _previous.LeftButton == ButtonState.Released) 
            {
                OnButtonPressed(this, new SiMouseEventArgs(
                    current.X,
                    current.Y, 
                    gameTime.TotalGameTime,
                    _previous, 
                    current,
                    MouseButton.Left));
            }

            if (current.MiddleButton == ButtonState.Pressed && _previous.MiddleButton == ButtonState.Released) 
            {
                OnButtonPressed(this, new SiMouseEventArgs(
                    current.X,
                    current.Y, 
                    gameTime.TotalGameTime,
                    _previous,
                    current,
                    MouseButton.Middle));
            }

            if (current.RightButton == ButtonState.Pressed && _previous.RightButton == ButtonState.Released) 
            {
                OnButtonPressed(this, new SiMouseEventArgs(
                    current.X, 
                    current.Y, 
                    gameTime.TotalGameTime,
                    _previous,
                    current, 
                    MouseButton.Right));
            }

            if (current.XButton1 == ButtonState.Pressed && _previous.XButton1 == ButtonState.Released) 
            {
                OnButtonPressed(this, new SiMouseEventArgs(
                    current.X,
                    current.Y,
                    gameTime.TotalGameTime,
                    _previous, 
                    current,
                    MouseButton.XButton1));
            }

            if (current.XButton2 == ButtonState.Pressed && _previous.XButton2 == ButtonState.Released) 
            {
                OnButtonPressed(this, new SiMouseEventArgs(
                    current.X,
                    current.Y, 
                    gameTime.TotalGameTime,
                    _previous,
                    current,
                    MouseButton.XButton2));
            }

            // Check button releases.
            if (current.LeftButton == ButtonState.Released && _previous.LeftButton == ButtonState.Pressed) 
            {
                OnButtonReleased(this, new SiMouseEventArgs(
                    current.X,
                    current.Y, 
                    gameTime.TotalGameTime,
                    _previous,
                    current, 
                    MouseButton.Left));
            }

            if (current.MiddleButton == ButtonState.Released && _previous.MiddleButton == ButtonState.Pressed) 
            {
                OnButtonReleased(this, new SiMouseEventArgs(
                    current.X, 
                    current.Y, 
                    gameTime.TotalGameTime,
                    _previous,
                    current,
                    MouseButton.Middle));
            }

            if (current.RightButton == ButtonState.Released && _previous.RightButton == ButtonState.Pressed) 
            {
                OnButtonReleased(this, new SiMouseEventArgs(
                    current.X, 
                    current.Y, 
                    gameTime.TotalGameTime, 
                    _previous, 
                    current, 
                    MouseButton.Right));
            }

            if (current.XButton1 == ButtonState.Released && _previous.XButton1 == ButtonState.Pressed) 
            {
                OnButtonReleased(this, new SiMouseEventArgs(
                    current.X, 
                    current.Y, 
                    gameTime.TotalGameTime, 
                    _previous, 
                    current, 
                    MouseButton.XButton1));
            }

            if (current.XButton2 == ButtonState.Released && _previous.XButton2 == ButtonState.Pressed) 
            {
                OnButtonReleased(this, new SiMouseEventArgs(
                    current.X, 
                    current.Y, 
                    gameTime.TotalGameTime, 
                    _previous, 
                    current, 
                    MouseButton.XButton2));
            }

            // Check for any sort of mouse movement. If a button is down, it's a drag,
            // otherwise it's a move.
            if (_previous.X != current.X || _previous.Y != current.Y)
            {
                OnMouseMoved(this, new SiMouseEventArgs(
                    current.X, 
                    current.Y, 
                    gameTime.TotalGameTime, 
                    _previous, 
                    current));
            }

            // Handle mouse wheel events.
            if (_previous.ScrollWheelValue != current.ScrollWheelValue)
            {
                var value = current.ScrollWheelValue / 120;
                var delta = (current.ScrollWheelValue - _previous.ScrollWheelValue) / 120;
                OnMouseWheelMoved(this, new SiMouseEventArgs(
                    current.X, 
                    current.Y, 
                    gameTime.TotalGameTime, 
                    _previous, 
                    current, 
                    MouseButton.None, 
                    value, 
                    delta));
            }

            _previous = current;
        }

        /// <summary>
        /// Raises the ButtonReleased event. This is done automatically by a correctly configured component,
        /// but this is exposed publicly to allow programmatic button release events to occur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnButtonReleased(object sender, SiMouseEventArgs args)
        {
            if (ButtonReleased != null) 
            {
                ButtonReleased(sender, args);
            }
        }

        /// <summary>
        /// Raises the ButtonPressed event. This is done automatically by a correctly configured component,
        /// but this is exposed publicly to allow programmatic button press events to occur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnButtonPressed(object sender, SiMouseEventArgs args)
        {
            // If this click is within the right time and position of the last click, raise a
            // double-click event as well.           
            if (ButtonDoubleClicked != null &&
                _lastClick.Button == args.Button &&
                (args.Time - _lastClick.Time).TotalMilliseconds < DoubleClickTime &&
                DistanceBetween(args.Current, _lastClick.Current) < DoubleClickMaxMove)
            {
                ButtonDoubleClicked(sender, args);
                args.Time = new TimeSpan(0);
            }
            else if (ButtonPressed != null) 
            {
                ButtonPressed(sender, args);
            }

            _lastClick = args;
        }   
     
        /// <summary>
        /// Calculates the Manhattan distance between two mouse positions.
        /// </summary>
        private static int DistanceBetween(MouseState a, MouseState b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        /// <summary>
        /// Raises the MouseMoved event. This is done automatically by a correctly configured component,
        /// but this is exposed publicly to allow programmatic mouse move events to occur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnMouseMoved(object sender, MouseEventArgs args)
        {
            if (MouseMoved != null) 
            {
                MouseMoved(sender, args);
            }
        }

        /// <summary>
        /// Raises the MouseWheelMoved event. This is done automatically by a correctly configured component,
        /// but this is exposed publicly to allow programmatic mouse wheel events to occur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnMouseWheelMoved(object sender, MouseEventArgs args)
        {
            if (MouseWheelMoved != null) 
            {
                MouseWheelMoved(sender, args);
            }
        }

        /// <summary>
        /// An event that is raised whenever any mouse button is released. The specific button, as well as
        /// other relevant information, can be looked up through the MouseEventArgs parameter.
        /// </summary>
        public event EventHandler<MouseEventArgs> ButtonReleased;

        /// <summary>
        /// An event that is raised whenever any mouse button is pressed. The specific button, as well as
        /// other relevant information, can be looked up through the MouseEventArgs parameter.
        /// </summary>
        public event EventHandler<MouseEventArgs> ButtonPressed;

        /// <summary>
        /// An event that is raised whenever two button clicks occur in the same spot in a short period
        /// of time. The tolerance allowed for defining "same spot" can be configured through 
        /// MouseEvents.DoubleClickMaxMove, while the tolerance for the time period can be configured through
        /// MouseEvents.DoubleClickTime.
        /// </summary>
        public event EventHandler<MouseEventArgs> ButtonDoubleClicked;

        /// <summary>
        /// An event that is raised whenever the mouse moves.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseMoved;

        /// <summary>
        /// An event that is raised whenever the mouse wheel is rotated.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseWheelMoved;
    }
}
