namespace Microsoft.Xna.Framework.Input 
{
    public class WCharacterEventArgs : KeyboardCharacterEventArgs
    {
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

        private readonly int lParam;

        public WCharacterEventArgs(char character, int lParam) : base(character)
        {
            this.lParam = lParam;
        }        
    }
}