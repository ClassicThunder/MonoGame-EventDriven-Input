# XNA_MonoGame_FNA_Input

Unified event driven input library for XNA, MonoGame, and FNA.

The goal of this project is to create a cross platform event driven input library that works for XNA and all 
of its derivatives. Where possible it leverages OS and library specific functionality to make the input feel 
as natural as possible for the user. Ass a result the library should have no input with text entry faster than 
the polling rate for all other implementations than CIInput.

#### The OS/Framework Specific Implementations 

For the final project there will be 4 subclasses that extend the abstract Input class. 

* CIInput (100% Done)

A modification of the [Starbound Input](https://bitbucket.org/rbwhitaker/starbound-input/) library. This 
library simply keeps track of state changes each tick and fires an event when a change occurs. Uses hard 
coded settings to control the double click time and other settings. 

ToDo: Make the settings a separate class that is passed into the constructor.

* WindowsInput (100% Done)

Converts the messages popped off of the windows event queue into events using XNA's event args. The main advantage 
of this one is that it uses the native windows settings (double click time, character repeating time, etc...).

* MonoGameInput (0% Done CIInput can be used in the mean time)
* FNA Input (0% Done CIInput can be used in the mean tim)

Both MonoGame and FNA have keyboard events for entering characters. These two versions will use CIInput for the mouse 
and the framework specific functionality for the keyboard input.

#### The Input Root Class 

```C#
  public abstract event EventHandler<KeyboardCharacterEventArgs> CharacterTyped;
  
  public abstract event EventHandler<KeyboardKeyEventArgs> KeyDown;
  public abstract event EventHandler<KeyboardKeyEventArgs> KeyUp;
  
  public abstract event EventHandler<MouseEventArgs> MouseMoved;
  
  public abstract event EventHandler<MouseEventArgs> MouseDoubleClick;
  
  public abstract event EventHandler<MouseEventArgs> MouseDown;
  public abstract event EventHandler<MouseEventArgs> MouseUp;
  
  public abstract event EventHandler<MouseEventArgs> MouseWheel;
```
