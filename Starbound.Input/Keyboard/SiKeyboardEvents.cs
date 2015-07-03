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
        private static int InitialDelay { get; set; }
        private static int RepeatDelay { get; set; }

        private Keys _lastKey;
        private TimeSpan _lastPress;
        private bool _isInitial;
        
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
        public void Update(GameTime gameTime)
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

                    // Maintain the state of last key pressed.
                    _lastKey = key;
                    _lastPress = gameTime.TotalGameTime;
                    _isInitial = true;
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


            // Handle keys being held down and getting multiple KeyTyped events in sequence.
            var elapsedTime = (gameTime.TotalGameTime - _lastPress).TotalMilliseconds;

            if (current.IsKeyDown(_lastKey) && 
                ((_isInitial && elapsedTime > InitialDelay) ||
                (!_isInitial && elapsedTime > RepeatDelay)))
            {
                OnKeyPressed(this, new KeyboardKeyEventArgs(_lastKey));
                
                _lastPress = gameTime.TotalGameTime;
                _isInitial = false;
            }

            _previous = current;
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

            if (CharacterTyped != null) 
            {
                var character = KeyboardUtil.ToChar(args.Key, args.Modifiers);
                if (character.HasValue) {
                    CharacterTyped(this, new KeyboardCharacterEventArgs(character.Value));
                }
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