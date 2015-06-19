using System;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Input
{
    internal static class Win32
    {
        internal const int GWL_WNDPROC = -4;
        internal const int DLGC_WANTALLKEYS = 4;

        internal const uint MAPVK_VK_TO_VSC = 0x00;
        internal const uint MAPVK_VSC_TO_VK = 0x01;
        internal const uint MAPVK_VK_TO_CHAR = 0x02;
        internal const uint MAPVK_VSC_TO_VK_EX = 0x03;
        internal const uint MAPVK_VK_TO_VSC_EX = 0x04;

        [DllImport("user32.dll")]
        internal static extern uint MapVirtualKey(
            uint uCode,
            uint uMapType);

        [DllImport("Imm32.dll", SetLastError = true)]
        internal static extern IntPtr ImmGetContext(
            IntPtr hWnd);

        internal delegate IntPtr WndProcDelegate(
            IntPtr hWnd,
            WindowsMessages msg, 
            IntPtr wParam,
            IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern IntPtr CallWindowProc(
            WndProcDelegate lpPrevWndFunc,
            IntPtr hWnd,
            WindowsMessages msg, 
            IntPtr wParam, 
            IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern IntPtr DefWindowProc(
            IntPtr hWnd,
            WindowsMessages msg, 
            IntPtr wParam,
            IntPtr lParam);

        /// <summary>
        /// Changes an attribute of the specified window. The function also sets the 32-bit (long) value at the specified offset into the extra window memory.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">The zero-based offset to the value to be set. Valid values are in the range zero through the number of bytes of extra window memory, minus the size of an integer. To set any other value, specify one of the following values: GWL_EXSTYLE, GWL_HINSTANCE, GWL_ID, GWL_STYLE, GWL_USERDATA, GWL_WNDPROC </param>
        /// <param name="dwNewLong">The replacement value.</param>
        /// <returns>If the function succeeds, the return value is the previous value of the specified 32-bit integer. 
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError. </returns>
        [DllImport("user32.dll")]
        internal static extern WndProcDelegate SetWindowLong(
            IntPtr hWnd, 
            int nIndex, 
            int dwNewLong);
    }
}
