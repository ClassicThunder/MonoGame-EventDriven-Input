using System;
using System.Linq;

namespace Microsoft.Xna.Framework.Input
{
    /// <summary>
    /// An abstraction around keyboard input that turns XNA's underlying polling model into an event-based
    /// model for keyboard input.
    /// </summary>
    public class SiKeyboardEvents
    {
        /// <summary>
        /// Represents the amount of time between a key being pressed, and the time that key typed events
        /// start repeating. This is measured in milliseconds. The initial delay is traditionally 
        /// significantly longer than other delays. The default is 800 milliseconds.
        /// </summary>
        public static int InitialDelay { get; set; }

        /// <summary>
        /// Represents the amount of time delay between key typed events after the first repeat. This 
        /// "normal" repeat delay is typically much faster than the initial. The default is 50 milliseconds
        /// (20 times per second).
        /// </summary>
        public static int RepeatDelay { get; set; }
        
        /// <summary>
        /// Stores the last keyboard state from the previous update.
        /// </summary>
        private KeyboardState _previous;

        /// <summary>
        /// Creates a new SIKeyboardEvents object.
        /// </summary>
        public SiKeyboardEvents()
        {
            InitialDelay = 800;
            RepeatDelay = 50;
        }

        /// <summary>
        /// Updates the component, turning XNA's polling model into an event-based model, raising
        /// events as they happen.
        /// </summary>
        public void Update()
        {
            var current = Keyboard.GetState();
            
            // Key pressed and initial key typed events for all keys.
            if (!current.IsKeyDown(Keys.LeftAlt)
                && !current.IsKeyDown(Keys.RightAlt)) 
            {
                foreach (var key in Enum.GetValues(typeof (Keys))
                    .Cast<Keys>()
                    .Where(key => current.IsKeyDown(key) && _previous.IsKeyUp(key)))
                {
                    var args = new KeyboardKeyEventArgs(key);

                    OnKeyPressed(this, args);

                    var character = KeyboardUtil.ToChar(key, args.Modifiers);
                    if (character.HasValue) {
                        OnKeyTyped(this, new KeyboardCharacterEventArgs(character.Value));
                    }
                }
            }

            // Key released events for all keys.
            foreach (var key in 
                Enum.GetValues(typeof(Keys))
                .Cast<Keys>()
                .Where(key => current.IsKeyUp(key) && _previous.IsKeyDown(key))) 
            {
                OnKeyReleased(this, new KeyboardKeyEventArgs(key));
            }

            _previous = current;
        }

        /// <summary>
        /// Raises the CharacterTyped event. This is done automatically by a correctly configured component,
        /// but this is exposed publicly to allow programmatic key typed events to occur.
        /// </summary>
        private void OnKeyTyped(object sender, KeyboardCharacterEventArgs args)
        {
            if (CharacterTyped != null)
            {
                CharacterTyped(sender, args);
            }
        }

        /// <summary>
        /// Raises the KeyPressed event. This is done automatically by a correctly configured component,
        /// but this is exposed publicly to allow programmatic key press events to occur.
        /// </summary>
        private void OnKeyPressed(object sender, KeyboardKeyEventArgs args)
        {
            if (KeyPressed != null) 
            {
                KeyPressed(sender, args);
            }
        }

        /// <summary>
        /// Raises the KeyReleased event. This is done automatically by a correctly configured component,
        /// but this is exposed publicly to allow programmatic key release events to occur.
        /// </summary>
        private void OnKeyReleased(object sender, KeyboardKeyEventArgs args)
        {
            if (KeyReleased != null) 
            {
                KeyReleased(sender, args);
            }
        }

        /// <summary>
        /// An event that is raised when a character key is released.
        /// </summary>
        public event EventHandler<KeyboardCharacterEventArgs> CharacterTyped;

        /// <summary>
        /// An event that is raised when a key is first pressed.
        /// </summary>
        public event EventHandler<KeyboardKeyEventArgs> KeyPressed;

        /// <summary>
        /// An event that is raised when a key is released.
        /// </summary>
        public event EventHandler<KeyboardKeyEventArgs> KeyReleased;
    }
}