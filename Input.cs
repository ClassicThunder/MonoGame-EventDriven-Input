using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CTInput
{
    public abstract class Input 
    {
        /*####################################################################*/
        /*                           Input Events                             */
        /*####################################################################*/

        public abstract void Update(GameTime gameTime);

        /*####################################################################*/
        /*                          Keyboard Events                           */
        /*####################################################################*/

        public abstract event EventHandler<KeyboardEventArgs> CharacterTyped;

        public abstract event EventHandler<KeyboardEventArgs> KeyDown;
        public abstract event EventHandler<KeyboardEventArgs> KeyUp;

        /*####################################################################*/
        /*                            Mouse Events                            */
        /*####################################################################*/

        // ##### Movement ##### //
        public abstract event EventHandler<MouseEventArgs> MouseMoved;

        // ##### Buttons ##### //

        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/aa931259.aspx
        /// 
        /// ## About
        /// The system generates a double-click message when the user clicks a mouse 
        /// button twice in quick succession. When the user clicks a button, the system 
        /// establishes a rectangle centered around the cursor hot spot. It also marks 
        /// the time at which the click occurred. When the user clicks the same button 
        /// a second time, the system determines whether the hot spot is still within 
        /// the rectangle and calculates the time elapsed since the first click. If the 
        /// hot spot is still within the rectangle and the elapsed time does not exceed 
        /// the double-click time-out value, the system generates a double-click message.
        /// 
        /// ## Message order
        /// WM_LBUTTONDOWN 
        /// WM_LBUTTONUP 
        /// WM_LBUTTONDBLCLK 
        /// WM_LBUTTONUP 
        /// 
        /// ## Settings
        /// 
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms724385%28v=vs.85%29.aspx
        /// 
        /// The height of the rectangle around the location of a first click in a double-click 
        /// sequence, in pixels. The second click must occur within the rectangle defined by 
        /// SM_CXDOUBLECLK and SM_CYDOUBLECLK for the system to consider the two clicks a 
        /// double-click. The two clicks must also occur within a specified time.
        /// 
        /// GetSystemMetrics(SM_CXDOUBLECLK); Default is 4
        /// GetSystemMetrics(SM_CYDOUBLECLK); Default is 4
        /// 
        /// https://msdn.microsoft.com/en-us/library/aa922956.aspx
        /// 
        /// The return value is the maximum number of milliseconds that can elapse between the first 
        /// click and the second click for the OS to consider the mouse action a double-click.
        /// 
        /// UINT GetDoubleClickTime(void);
        /// 
        /// </summary>
        public abstract event EventHandler<MouseEventArgs> MouseDoubleClick;

        //TODO: Add hover

        public abstract event EventHandler<MouseEventArgs> MouseDown;
        public abstract event EventHandler<MouseEventArgs> MouseUp;

        // ##### Wheel ##### //
        public abstract event EventHandler<MouseEventArgs> MouseWheel;
    }
}
