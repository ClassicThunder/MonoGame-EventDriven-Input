# XNA_EventDriven_Input

Unified event driven input library for XNA, MonoGame, and FNA.

The goal of this project is to create a cross platform event driven input library that works for XNA and all 
of its derivatives. The intention is for the events to be handled in an identical manner to the Windows Messaging 
queue.

#### Getting Started

```C#
//Instantiate the appropriate Input type
Input _input = new WindowsInput(this); //Windows form specific using the Message Queue
Input _input = new SiInput(this); //Generic implementation based upon polling state changes

//Subscribe to the events
_inputCharacterTyped += (sender, keyTyped) => 
{
  Console.WriteLine(keyTyped.Character);
}
```

Available Events

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

#### Generic and Windows Specific Implementations

* SIInput

A modification of the [Starbound Input](https://bitbucket.org/rbwhitaker/starbound-input/) library with the goal of 
making it consistent with the behavior of the windows Message queue. This library simply keeps track of state changes 
each tick and fires an event when a change occurs. Uses hard coded settings to control the double click time and other
settings. 

* WindowsInput (Done)

Converts the messages popped off of the windows event queue into events using XNA's event args. The main advantage 
of this one is that it uses the native windows settings (double click time, character repeating time, etc...).


