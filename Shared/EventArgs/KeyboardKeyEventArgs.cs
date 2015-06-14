using System;

namespace Microsoft.Xna.Framework.Input
{
    public abstract class KeyboardKeyEventArgs : EventArgs
    {
        public Keys Key { get; set; }

        protected internal KeyboardKeyEventArgs(Keys key) 
        {
            Key = key;
        }
    }
}
