namespace Microsoft.Xna.Framework.Input
{
    public class WKeyEventArgs : KeyboardEventArgs
    {
        public WKeyEventArgs(Keys keyCode, char? character)
            : base(keyCode, character)
        { }        
    }
}
