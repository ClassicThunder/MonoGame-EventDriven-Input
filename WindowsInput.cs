using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CTInput
{
    public class WindowsInput : Input 
    {
        //private WindowsHook _windowsHook;

        public WindowsInput(Game game) : base(game) 
        {
            //_windowsHook = new WindowsHook(game.Window.Handle);
            InputSystem.Initialize(game.Window);
        }

        public override void Update(GameTime gameTime) { }

        /*####################################################################*/
        /*                          Keyboard Events                           */
        /*####################################################################*/

        public override event EventHandler<KeyboardCharacterEventArgs> CharacterTyped
        {
            add { InputSystem.CharEntered += value; }
            remove { InputSystem.CharEntered -= value; }
        }

        public override event EventHandler<KeyboardKeyEventArgs> KeyDown
        {
            add { InputSystem.KeyDown += value; }
            remove { InputSystem.KeyDown -= value; }
        }

        public override event EventHandler<KeyboardKeyEventArgs> KeyUp
        {
            add { InputSystem.KeyUp += value; }
            remove { InputSystem.KeyUp -= value; }
        }


        /*####################################################################*/
        /*                            Mouse Events                            */
        /*####################################################################*/ 

        //Movement
        public override event EventHandler<MouseEventArgs> MouseMoved
        {
            add { InputSystem.MouseMove += value; }
            remove { InputSystem.MouseMove -= value; }
        }

        //Buttons
        public override event EventHandler<MouseEventArgs> MouseDoubleClick
        {
            add { InputSystem.MouseDoubleClick += value; }
            remove { InputSystem.MouseDoubleClick -= value; }
        }

        public override event EventHandler<MouseEventArgs> MouseDown
        {
            add { InputSystem.MouseDown += value; }
            remove { InputSystem.MouseDown -= value; }
        }

        public override event EventHandler<MouseEventArgs> MouseUp
        {
            add { InputSystem.MouseUp += value; }
            remove { InputSystem.MouseUp -= value; }
        }                

        //Wheel
        public override event EventHandler<MouseEventArgs> MouseWheel
        {
            add { InputSystem.MouseWheel += value; }
            remove { InputSystem.MouseWheel -= value; }
        }
    }
}
