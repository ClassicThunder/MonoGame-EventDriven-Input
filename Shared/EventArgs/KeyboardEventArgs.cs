using System;

namespace Microsoft.Xna.Framework.Input
{
    public abstract class KeyboardEventArgs : EventArgs
    {
        abstract public Keys Key { get; set; }

        abstract public char? Character { get; set; }
    }
}
