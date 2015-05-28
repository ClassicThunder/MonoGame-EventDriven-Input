using System;

namespace Microsoft.Xna.Framework.Input
{
    public abstract class KeyboardEventArgs : EventArgs
    {
        public Keys Key { get; set; }

        public char? Character { get; set; }

        protected internal KeyboardEventArgs(Keys key, char? character) 
        {
            Key = key;
            Character = character;
        }
    }
}
