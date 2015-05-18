using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CTInput
{
    public abstract class Input 
    {
        /*####################################################################*/
        /*                           Input Events                             */
        /*####################################################################*/

        public abstract void Update(GameTime gameTime);

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
        public abstract event EventHandler<MouseEventArgs> MouseClick;
        public abstract event EventHandler<MouseEventArgs> MouseDoubleClick;
        public abstract event EventHandler<MouseEventArgs> MouseDown;
        public abstract event EventHandler<MouseEventArgs> MouseUp;             

        //Wheel
        public abstract event EventHandler<MouseEventArgs> MouseWheel;
    }
}
