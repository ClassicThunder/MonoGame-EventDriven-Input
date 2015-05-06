using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CTInput
{
    public class GenericInput : Input
    {
        private readonly MouseEvents _mouseEvents;
        private readonly KeyboardEvents _keyboardEvents;

        public GenericInput() 
        {
            _mouseEvents = new MouseEvents();
            _keyboardEvents = new KeyboardEvents();
        }

        public void Update(GameTime gameTime) 
        {
            _mouseEvents.Update(gameTime);
            _keyboardEvents.Update(gameTime);
        }


        /*####################################################################*/
        /*                          Keyboard Events                           */
        /*####################################################################*/

        public override event EventHandler<KeyboardEventArgs> KeyTyped
        {
            add { _keyboardEvents.KeyTyped += value; }
            remove { _keyboardEvents.KeyTyped -= value; }
        }

        public override event EventHandler<KeyboardEventArgs> KeyDown
        {
            add { _keyboardEvents.KeyPressed += value; }
            remove { _keyboardEvents.KeyPressed -= value; }
        }

        public override event EventHandler<KeyboardEventArgs> KeyUp
        {
            add { _keyboardEvents.KeyReleased += value; }
            remove { _keyboardEvents.KeyReleased -= value; }
        }


        /*####################################################################*/
        /*                            Mouse Events                            */
        /*####################################################################*/ 

        //Movement
        public override event EventHandler<MouseEventArgs> MouseMoved
        {
            add { _mouseEvents.MouseMoved += value; }
            remove { _mouseEvents.MouseMoved -= value; }
        }

        public override event EventHandler<MouseEventArgs> MouseDragged
        {
            add { _mouseEvents.MouseDragged += value; }
            remove { _mouseEvents.MouseDragged -= value; }
        }

        //Buttons
        public override event EventHandler<MouseButtonEventArgs> MouseClick
        {
            add { _mouseEvents.ButtonClicked += value; }
            remove { _mouseEvents.ButtonClicked -= value; }
        }

        public override event EventHandler<MouseButtonEventArgs> MouseDoubleClick
        {
            add { _mouseEvents.ButtonDoubleClicked += value; }
            remove { _mouseEvents.ButtonDoubleClicked -= value; }
        }

        public override event EventHandler<MouseButtonEventArgs> MouseDown
        {
            add { _mouseEvents.ButtonPressed += value; }
            remove { _mouseEvents.ButtonPressed -= value; }
        }

        public override event EventHandler<MouseButtonEventArgs> MouseUp
        {
            add { _mouseEvents.ButtonReleased += value; }
            remove { _mouseEvents.ButtonReleased -= value; }
        }                

        //Wheel
        public override event EventHandler<MouseWheelEventArgs> MouseWheel
        {
            add { _mouseEvents.MouseWheelMoved += value; }
            remove { _mouseEvents.MouseWheelMoved -= value; }
        }
    }
}
