﻿namespace Microsoft.Xna.Framework.Input
{
    /// <summary>
    /// Represents an EventArgs object that is for all keyboard events in Starbound.UI.
    /// </summary>
    public class SiKeyboardCharacterEventArgs : KeyboardCharacterEventArgs
    {
        /// <summary>
        /// The current state of they keyboard.
        /// </summary>
        public KeyboardState State { get; protected set; }

        /// <summary>
        /// The current set of modifiers that are in use.
        /// </summary>
        public Modifiers Modifiers { get; protected set; }

        /// <summary>
        /// Creates a new KeyboardEventArgs, given a time for the event, the key that was pressed, and
        /// the modifiers that were applied at the time of the press, as well as the keyboard state at 
        /// the time the event occurred.
        /// </summary>
        public SiKeyboardCharacterEventArgs(char character, Modifiers modifiers, KeyboardState state)
            : base(character)
        {
            State = state;
            Modifiers = modifiers;
        }
    }
}