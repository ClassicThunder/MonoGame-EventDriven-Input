using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CTInput
{
    public sealed class SiInput : Input
    {
        private readonly SiMouseEvents _mouseEvents;
        private readonly SiKeyboardEvents _siKeyboardEvents;

        public SiInput() 
        {
            _mouseEvents = new SiMouseEvents();
            _siKeyboardEvents = new SiKeyboardEvents();
        }

        override public void Update(GameTime gameTime) 
        {
            _mouseEvents.Update(gameTime);
            _siKeyboardEvents.Update(gameTime);
        }


        /*####################################################################*/
        /*                          Keyboard Events                           */
        /*####################################################################*/

        public override event EventHandler<KeyboardEventArgs> CharacterTyped
        {
            add { _siKeyboardEvents.KeyTyped += value; }
            remove { _siKeyboardEvents.KeyTyped -= value; }
        }

        public override event EventHandler<KeyboardEventArgs> KeyDown
        {
            add { _siKeyboardEvents.KeyPressed += value; }
            remove { _siKeyboardEvents.KeyPressed -= value; }
        }

        public override event EventHandler<KeyboardEventArgs> KeyUp
        {
            add { _siKeyboardEvents.KeyReleased += value; }
            remove { _siKeyboardEvents.KeyReleased -= value; }
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

        //Buttons
        public override event EventHandler<MouseEventArgs> MouseDoubleClick
        {
            add { _mouseEvents.ButtonDoubleClicked += value; }
            remove { _mouseEvents.ButtonDoubleClicked -= value; }
        }

        public override event EventHandler<MouseEventArgs> MouseDown
        {
            add { _mouseEvents.ButtonPressed += value; }
            remove { _mouseEvents.ButtonPressed -= value; }
        }

        public override event EventHandler<MouseEventArgs> MouseUp
        {
            add { _mouseEvents.ButtonReleased += value; }
            remove { _mouseEvents.ButtonReleased -= value; }
        }                

        //Wheel
        public override event EventHandler<MouseEventArgs> MouseWheel
        {
            add { _mouseEvents.MouseWheelMoved += value; }
            remove { _mouseEvents.MouseWheelMoved -= value; }
        }
    }
}
