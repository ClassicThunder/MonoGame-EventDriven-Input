using System;

namespace Microsoft.Xna.Framework.Input
{
    public abstract class KeyboardCharacterEventArgs : EventArgs
    {
        public char? Character { get; set; }

        protected internal KeyboardCharacterEventArgs(char? character) 
        {
            Character = character;
        }
    }
}
