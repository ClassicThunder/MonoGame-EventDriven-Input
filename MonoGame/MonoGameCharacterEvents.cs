using System;

namespace Microsoft.Xna.Framework.Input
{
    public class MonoGameCharacterEvents
    {
        KeyboardState _prevKeyState;

        public MonoGameCharacterEvents(Game game)
        {
            game.Window.TextInput += OnCharacterTyped;
        }

        public void Update()
        {            
//#if OpenGL
            var keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Back) && _prevKeyState.IsKeyUp(Keys.Back))
            {
                OnCharacterTyped(this, new TextInputEventArgs('\b'));
            }
            if (keyState.IsKeyDown(Keys.Enter) && _prevKeyState.IsKeyUp(Keys.Enter))
            {
                OnCharacterTyped(this, new TextInputEventArgs('\r'));
            }

            _prevKeyState = keyState;
//#endif
        }

        private void OnCharacterTyped(object sender, TextInputEventArgs args)
        {
            if (CharacterTyped != null)
            {
                CharacterTyped(sender, new KeyboardCharacterEventArgs(args.Character));
            }
        }

        public event EventHandler<KeyboardCharacterEventArgs> CharacterTyped;
    }
}