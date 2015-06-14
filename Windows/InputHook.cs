using System;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Input
{
    public static class InputSystem
    {
        /*####################################################################*/
        /*                               Events                               */
        /*####################################################################*/

        public static event EventHandler<KeyboardCharacterEventArgs> CharEntered;

        public static event EventHandler<KeyboardKeyEventArgs> KeyDown;
        public static event EventHandler<KeyboardKeyEventArgs> KeyUp;

        public static event EventHandler<MouseEventArgs> MouseDown;
        public static event EventHandler<MouseEventArgs> MouseUp;
        public static event EventHandler<MouseEventArgs> MouseMove;
        public static event EventHandler<MouseEventArgs> MouseWheel;
        public static event EventHandler<MouseEventArgs> MouseDoubleClick;

        /*####################################################################*/
        /*                       Properties and Constants                     */
        /*####################################################################*/

        delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        private static bool Initialized { get; set; }

        private static IntPtr _hImc; //Used to keep the callback from getting GCed
        private static WndProc _hookProcDelegate;
        private static Win32.WndProcDelegate _prevWndProc;        

        private static int _totalDelta;

        /*####################################################################*/
        /*                         Message Translation                        */
        /*####################################################################*/

        /// Initialize the TextInput with the given GameWindow.      
        /// The XNA window to which text input should be linked.
        internal static void Initialize(GameWindow window)
        {
            if (Initialized)
            {
                throw new Exception("Only 1 instance of Windows input can be created at a time.");
            }

            _hookProcDelegate = HookProc;
            _prevWndProc = Win32.SetWindowLong(
                window.Handle,
                Win32.GWL_WNDPROC,
                (int)Marshal.GetFunctionPointerForDelegate(_hookProcDelegate));
            _hImc = Win32.ImmGetContext(window.Handle);

            Initialized = true;
        }

        private static IntPtr HookProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            var returnCode = Win32.CallWindowProc(_prevWndProc, hWnd, msg, wParam, lParam);

            var wparam = wParam.ToInt32();
            var lparam = lParam.ToInt32();

            short low, high;
            SplitIntIntoWords(wparam, out low, out high);

            var virtualKey = (Keys)Win32.MapVirtualKey((uint)((lparam & 0x00ff0000) >> 16), Win32.MAPVK_VSC_TO_VK_EX);

            switch ((WindowsMessages)msg)
            {
                    //Keyboard messages
                case WindowsMessages.GETDLGCODE:
                    returnCode = (IntPtr)(returnCode.ToInt32() | Win32.DLGC_WANTALLKEYS);
                    break;
                case WindowsMessages.KEYDOWN:
                    if (KeyDown != null)
                        KeyDown(null, new WKeyEventArgs(virtualKey));
                    break;
                case WindowsMessages.KEYUP:
                    if (KeyUp != null)
                        KeyUp(null, new WKeyEventArgs(virtualKey));
                    break;
                case WindowsMessages.CHAR:
                    if (CharEntered != null)
                        CharEntered(null, new WCharacterEventArgs((char)wParam, lparam));
                    break;

                    // Mouse messages                       
                case WindowsMessages.MOUSEMOVE:
                    if (MouseMove != null)
                    {
                        short x, y;
                        MouseLocationFromLParam(lparam, out x, out y);
                        MouseMove(null, new WMouseEventArgs(
                            x, y, MouseButton.None, 0, null, null));
                    }
                    break;
                case WindowsMessages.MOUSEWHEEL:
                    if (MouseWheel != null)
                    {
                        var delta = (wparam >> 16) / 120;
                        _totalDelta += delta;
                        short x, y;
                        MouseLocationFromLParam(lparam, out x, out y);
                        MouseWheel(null, new WMouseEventArgs(
                            x, y, MouseButton.None, 0, _totalDelta, delta));
                    }
                    break;
                case WindowsMessages.LBUTTONDOWN:
                    RaiseMouseDownEvent(MouseButton.Left, lparam);
                    break;
                case WindowsMessages.LBUTTONUP:
                    RaiseMouseUpEvent(MouseButton.Left, lparam);
                    break;
                case WindowsMessages.LBUTTONDBLCLK:
                    RaiseMouseDblClickEvent(MouseButton.Left, lparam);
                    break;
                case WindowsMessages.RBUTTONDOWN:
                    RaiseMouseDownEvent(MouseButton.Right, lparam);
                    break;
                case WindowsMessages.RBUTTONUP:
                    RaiseMouseUpEvent(MouseButton.Right, lparam);
                    break;
                case WindowsMessages.RBUTTONDBLCLK:
                    RaiseMouseDblClickEvent(MouseButton.Right, lparam);
                    break;
                case WindowsMessages.MBUTTONDOWN:
                    RaiseMouseDownEvent(MouseButton.Middle, lparam);
                    break;
                case WindowsMessages.MBUTTONUP:
                    RaiseMouseUpEvent(MouseButton.Middle, lparam);
                    break;
                case WindowsMessages.MBUTTONDBLCLK:
                    RaiseMouseDblClickEvent(MouseButton.Middle, lparam);
                    break;
                case WindowsMessages.XBUTTONDOWN:
                    switch (high)
                    {
                        case 0x0001:
                            RaiseMouseDownEvent(MouseButton.XButton1, lparam);
                            break;
                        case 0x0002:
                            RaiseMouseDownEvent(MouseButton.XButton2, lparam);
                            break;
                    }
                    break;
                case WindowsMessages.XBUTTONUP:
                    switch (high)
                    {
                        case 0x0001:
                            RaiseMouseUpEvent(MouseButton.XButton1, lparam);
                            break;
                        case 0x0002:
                            RaiseMouseUpEvent(MouseButton.XButton2, lparam);
                            break;
                    }
                    break;
                case WindowsMessages.XBUTTONDBLCLK:
                    switch (high) {
                        case 0x0001:
                            RaiseMouseDblClickEvent(MouseButton.XButton1, lparam);
                            break;
                        case 0x0002:
                            RaiseMouseDblClickEvent(MouseButton.XButton2, lparam);
                            break;
                    }
                    break;
            }

            return returnCode;
        }

        /*####################################################################*/
        /*                            Raise Events                            */
        /*####################################################################*/

        private static void RaiseMouseDownEvent(MouseButton button, int lParam)
        {
            if (MouseDown == null) { return; }

            short x, y;
            MouseLocationFromLParam(lParam, out x, out y);
            MouseDown(null, new WMouseEventArgs(x, y, button, 1, null, null));
        }

        private static void RaiseMouseUpEvent(MouseButton button, int lParam)
        {
            if (MouseUp == null) { return; }

            short x, y;
            MouseLocationFromLParam(lParam, out x, out y);
            MouseUp(null, new WMouseEventArgs(x, y, button, 1, null, null));
        }

        private static void RaiseMouseDblClickEvent(MouseButton button, int lParam)
        {
            if (MouseDoubleClick == null) { return; }

            short x, y;
            MouseLocationFromLParam(lParam, out x, out y);
            MouseDoubleClick(null, new WMouseEventArgs(x, y, button, 1, null, null));
        }

        private static void MouseLocationFromLParam(int lParam, out short x, out short y)
        {
            x = (short)(lParam & 0xFFFF);
            y = (short)(lParam >> 16);
        }

        private static void SplitIntIntoWords(int lParam, out short lowOrder, out short highOrder)
        {
            lowOrder = (short)(lParam & 0xFFFF);
            highOrder = (short)(lParam >> 16);
        }
    }
}