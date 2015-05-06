using System;
using Microsoft.Xna.Framework.Input;

namespace CTInput
{
    public abstract class Input 
    {
        /*####################################################################*/
        /*                          Keyboard Events                           */
        /*####################################################################*/

        public abstract event EventHandler<KeyboardEventArgs> KeyTyped;
        public abstract event EventHandler<KeyboardEventArgs> KeyDown;
        public abstract event EventHandler<KeyboardEventArgs> KeyUp;

        /*####################################################################*/
        /*                            Mouse Events                            */
        /*####################################################################*/ 

        //Movement
        public abstract event EventHandler<MouseEventArgs> MouseMoved;
        public abstract event EventHandler<MouseEventArgs> MouseDragged;

        //Buttons
        public abstract event EventHandler<MouseButtonEventArgs> MouseClick;
        public abstract event EventHandler<MouseButtonEventArgs> MouseDoubleClick;
        public abstract event EventHandler<MouseButtonEventArgs> MouseDown;
        public abstract event EventHandler<MouseButtonEventArgs> MouseUp;             

        //Wheel
        public abstract event EventHandler<MouseWheelEventArgs> MouseWheel;
    }
}
