using System;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Input
{
    public sealed class InputSystem
    {
        /*####################################################################*/
        /*                               Events                               */
        /*####################################################################*/

        /// Event raised when a character has been entered.          
        public event EventHandler<KeyboardEventArgs> CharEntered;

        /// Event raised when a key has been pressed down. May fire multiple times due to keyboard repeat.           
        public event EventHandler<KeyboardEventArgs> KeyDown;

        /// Event raised when a key has been released.
        public event EventHandler<KeyboardEventArgs> KeyUp;

        /// Event raised when a mouse button is pressed.
        public event EventHandler<MouseEventArgs> MouseDown;

        /// Event raised when a mouse button is released.    
        public event EventHandler<MouseEventArgs> MouseUp;

        /// Event raised when the mouse changes location.
        public event EventHandler<MouseEventArgs> MouseMove;

        /// Event raised when the mouse wheel has been moved.        
        public event EventHandler<MouseEventArgs> MouseWheel;

        /// Event raised when a mouse button has been double clicked.        
        public event EventHandler<MouseEventArgs> MouseDoubleClick;

        /*####################################################################*/
        /*                       Properties and Constants                     */
        /*####################################################################*/

        delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        private static bool Initialized { get; set; }

        private readonly IntPtr _prevWndProc;
        private readonly IntPtr _hImc;

        private int _totalDelta;
        
        const int GwlWndproc = -4;
        const int WmKeyDown = 0x100;
        const int WmKeyup = 0x101;
        const int WmChar = 0x102;
        const int WmImeSetContext = 0x281;
        const int WmInputLangChange = 0x51;
        const int WmGetDlgCode = 0x87;
        const int WmImeComposition = 0x10F;
        const int DlgcWantAllKeys = 4;
        const int WmMouseMove = 0x200;
        const int WmLButtonDown = 0x201;
        const int WmLButtonUp = 0x202;
        const int WmLButtOnDblClk = 0x203;
        const int WmRButtonDown = 0x204;
        const int WmRButtonUp = 0x205;
        const int WmRButtOndDlClk = 0x206;
        const int WmMButtOnDown = 0x207;
        const int WmMButtOnUp = 0x208;
        const int WmMButtOnDblClk = 0x209;
        const int WmMouseWheel = 0x20A;
        const int WmXButtOnDown = 0x20B;
        const int WmXButtOnUp = 0x20C;
        const int WmXButtOnDblClk = 0x20D;

        //const int SmCxDoubleClk = 36;
        //const int SmCyDoubleClk = 37;

        /*####################################################################*/
        /*                            DLL Imports                             */
        /*####################################################################*/

        [DllImport("Imm32.dll")]
        static extern IntPtr ImmGetContext(IntPtr hWnd);

        [DllImport("Imm32.dll")]
        static extern IntPtr ImmAssociateContext(IntPtr hWnd, IntPtr hIMC);

        [DllImport("user32.dll")]
        static extern IntPtr CallWindowProc(
            IntPtr lpPrevWndFunc, IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        //[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        //public static extern int GetSystemMetrics(int nIndex);        

        /*####################################################################*/
        /*                         Message Translation                        */
        /*####################################################################*/

        /// Initialize the TextInput with the given GameWindow.      
        /// The XNA window to which text input should be linked.
        internal InputSystem(GameWindow window)
        {
            if (Initialized) 
            {                    
                throw new Exception("Only 1 instance of Windows input can be created at a time.");
            }

            WndProc hookProcDelegate = HookProc;
            _prevWndProc = (IntPtr)SetWindowLong(window.Handle, GwlWndproc,
                (int)Marshal.GetFunctionPointerForDelegate(hookProcDelegate));
            _hImc = ImmGetContext(window.Handle);

            Initialized = true;
        }

        IntPtr HookProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            var returnCode = CallWindowProc(_prevWndProc, hWnd, msg, wParam, lParam);

            switch (msg)
            {
                case WmGetDlgCode:
                    returnCode = (IntPtr)(returnCode.ToInt32() | DlgcWantAllKeys);
                    break;
                case WmKeyDown:
                    if (KeyDown != null)
                        KeyDown(null, new WKeyEventArgs((Keys)wParam, null));
                    break;
                case WmKeyup:
                    if (KeyUp != null)
                        KeyUp(null, new WKeyEventArgs((Keys)wParam, null));
                    break;
                case WmChar:
                    if (CharEntered != null)
                        CharEntered(null, new WKeyEventArgs(Keys.None, (char)wParam));
                    break;
                case WmImeSetContext:
                    if (wParam.ToInt32() == 1)
                        ImmAssociateContext(hWnd, _hImc);
                    break;
                case WmInputLangChange:
                    ImmAssociateContext(hWnd, _hImc);
                    returnCode = (IntPtr)1;
                    break;

                // Mouse messages                       
                case WmMouseMove:
                    if (MouseMove != null)
                    {
                        short x, y;
                        MouseLocationFromLParam(lParam.ToInt32(), out x, out y);
                        MouseMove(null, new WMouseEventArgs(
                            x, y, MouseButton.None, 0, null, null));
                    }
                    break;
                case WmMouseWheel:
                    if (MouseWheel != null) 
                    {
                        var delta = (wParam.ToInt32() >> 16)/120;
                        _totalDelta += delta;
                        short x, y;
                        MouseLocationFromLParam(lParam.ToInt32(), out x, out y);
                        MouseWheel(null, new WMouseEventArgs(
                            x, y, MouseButton.None, 0, _totalDelta, delta));
                    }
                    break;
                case WmLButtonDown:
                    RaiseMouseDownEvent(MouseButton.Left, wParam.ToInt32(), lParam.ToInt32());
                    break;
                case WmLButtonUp:
                    RaiseMouseUpEvent(MouseButton.Left, wParam.ToInt32(), lParam.ToInt32());
                    break;
                case WmLButtOnDblClk:
                    RaiseMouseDblClickEvent(MouseButton.Left, wParam.ToInt32(), lParam.ToInt32());
                    break;
                case WmRButtonDown:
                    RaiseMouseDownEvent(MouseButton.Right, wParam.ToInt32(), lParam.ToInt32());
                    break;
                case WmRButtonUp:
                    RaiseMouseUpEvent(MouseButton.Right, wParam.ToInt32(), lParam.ToInt32());
                    break;
                case WmRButtOndDlClk:
                    RaiseMouseDblClickEvent(MouseButton.Right, wParam.ToInt32(), lParam.ToInt32());
                    break;
                case WmMButtOnDown:
                    RaiseMouseDownEvent(MouseButton.Middle, wParam.ToInt32(), lParam.ToInt32());
                    break;
                case WmMButtOnUp:
                    RaiseMouseUpEvent(MouseButton.Middle, wParam.ToInt32(), lParam.ToInt32());
                    break;
                case WmMButtOnDblClk:
                    RaiseMouseDblClickEvent(MouseButton.Middle, wParam.ToInt32(), lParam.ToInt32());
                    break;
                case WmXButtOnDown:
                    if ((wParam.ToInt32() & 0x10000) != 0)
                    {
                        RaiseMouseDownEvent(MouseButton.XButton1, wParam.ToInt32(), lParam.ToInt32());
                    }
                    else if ((wParam.ToInt32() & 0x20000) != 0)
                    {
                        RaiseMouseDownEvent(MouseButton.XButton2, wParam.ToInt32(), lParam.ToInt32());
                    }
                    break;
                case WmXButtOnUp:
                    if ((wParam.ToInt32() & 0x10000) != 0)
                    {
                        RaiseMouseUpEvent(MouseButton.XButton1, wParam.ToInt32(), lParam.ToInt32());
                    }
                    else if ((wParam.ToInt32() & 0x20000) != 0)
                    {
                        RaiseMouseUpEvent(MouseButton.XButton2, wParam.ToInt32(), lParam.ToInt32());
                    }
                    break;
                case WmXButtOnDblClk:
                    if ((wParam.ToInt32() & 0x10000) != 0)
                    {
                        RaiseMouseDblClickEvent(MouseButton.XButton1, wParam.ToInt32(), lParam.ToInt32());
                    }
                    else if ((wParam.ToInt32() & 0x20000) != 0)
                    {
                        RaiseMouseDblClickEvent(MouseButton.XButton2, wParam.ToInt32(), lParam.ToInt32());
                    }
                    break;
            }

            return returnCode;
        }

        /*####################################################################*/
        /*                            Raise Events                            */
        /*####################################################################*/

        void RaiseMouseDownEvent(MouseButton button, int wParam, int lParam)
        {
            if (MouseDown == null) { return; }

            short x, y;
            MouseLocationFromLParam(lParam, out x, out y);
            MouseDown(null, new WMouseEventArgs(x, y, button, 1, null, null));
        }

        void RaiseMouseUpEvent(MouseButton button, int wParam, int lParam)
        {
            if (MouseUp == null) { return; }

            short x, y;
            MouseLocationFromLParam(lParam, out x, out y);
            MouseUp(null, new WMouseEventArgs(x, y, button, 1, null, null));
        }

        void RaiseMouseDblClickEvent(MouseButton button, int wParam, int lParam)
        {
            if (MouseDoubleClick == null) { return; }

            short x, y;
            MouseLocationFromLParam(lParam, out x, out y);
            MouseDoubleClick(null, new WMouseEventArgs(x, y, button, 1, null, null));
        }

        void MouseLocationFromLParam(int lParam, out short x, out short y)
        {
            x = (short)(lParam & 0xFFFF);
            y = (short)(lParam >> 16);
        }
    }
}