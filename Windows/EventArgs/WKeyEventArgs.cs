using System;

namespace Microsoft.Xna.Framework.Input
{
    public delegate void WKeyEventHandler(object sender, WKeyEventArgs e);

    public class WKeyEventArgs : EventArgs
    {
        private Keys keyCode;

        public Keys KeyCode 
        {
            get { return keyCode; }
        }

        public WKeyEventArgs(Keys keyCode)
        {
            this.keyCode = keyCode;
        }        
    }
}
