using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CTInput
{
    public class WindowsInput : Input 
    {
        private readonly InputSystem _inputSystem;

        public WindowsInput(Game game) 
        {
            _inputSystem = new InputSystem(game.Window);
        }

        public override void Update(GameTime gameTime) { }

        /*####################################################################*/
        /*                          Keyboard Events                           */
        /*####################################################################*/        

        public override event EventHandler<KeyboardEventArgs> CharacterTyped
        {
            add { _inputSystem.CharEntered += value; }
            remove { _inputSystem.CharEntered -= value; }
        }

        public override event EventHandler<KeyboardEventArgs> KeyDown
        {
            add { _inputSystem.KeyDown += value; }
            remove { _inputSystem.KeyDown -= value; }
        }

        public override event EventHandler<KeyboardEventArgs> KeyUp
        {
            add { _inputSystem.KeyUp += value; }
            remove { _inputSystem.KeyUp -= value; }
        }


        /*####################################################################*/
        /*                            Mouse Events                            */
        /*####################################################################*/ 

        //Movement
        public override event EventHandler<MouseEventArgs> MouseMoved
        {
            add { _inputSystem.MouseMove += value; }
            remove { _inputSystem.MouseMove -= value; }
        }

        //Buttons
        public override event EventHandler<MouseEventArgs> MouseDoubleClick
        {
            add { _inputSystem.MouseDoubleClick += value; }
            remove { _inputSystem.MouseDoubleClick -= value; }
        }

        public override event EventHandler<MouseEventArgs> MouseDown
        {
            add { _inputSystem.MouseDown += value; }
            remove { _inputSystem.MouseDown -= value; }
        }

        public override event EventHandler<MouseEventArgs> MouseUp
        {
            add { _inputSystem.MouseUp += value; }
            remove { _inputSystem.MouseUp -= value; }
        }                

        //Wheel
        public override event EventHandler<MouseEventArgs> MouseWheel
        {
            add { _inputSystem.MouseWheel += value; }
            remove { _inputSystem.MouseWheel -= value; }
        }
    }
}
