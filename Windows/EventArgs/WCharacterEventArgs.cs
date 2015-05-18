using System;

namespace Microsoft.Xna.Framework.Input 
{
    public delegate void WKeyboardEnteredHandler(object sender, WKeyboardEventArgs e);

    public class WKeyboardEventArgs : EventArgs
    {
        public char Character 
        {
            get { return character; }
        }

        public int Param 
        {
            get { return lParam; }
        }

        public int RepeatCount 
        {
            get { return lParam & 0xffff; }
        }

        public bool ExtendedKey 
        {
            get { return (lParam & (1 << 24)) > 0; }
        }

        public bool AltPressed 
        {
            get { return (lParam & (1 << 29)) > 0; }
        }

        public bool PreviousState 
        {
            get { return (lParam & (1 << 30)) > 0; }
        }

        public bool TransitionState 
        {
            get { return (lParam & (1 << 31)) > 0; }
        }

        private readonly char character;
        private readonly int lParam;

        public WKeyboardEventArgs(char character, int lParam)
        {
            this.character = character;
            this.lParam = lParam;
        }        
    }
}