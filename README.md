# MonoGame-EventDriven-Input

This project provides event driven input for desktop MonoGame projects. The input has been designed to mimic the behavior of the Windows Message Queue as closely as possible creating as familiar as possible behavior.

## Getting Started

Using the library is very simple. Simple create an instance of the MonoGameInput object, call Update in the Game.Update function, and then subscribe to the events. You can even create multiple MonoGameInput instances as an easy way or organize different input schemes (for example one for your menu and one for gameplay input). To disable a MonoGameInput instance simply stop updating it.

```C#
Input _eventDrivenInput;
_eventDrivenInput = new MonoGameInput(this);

_eventDrivenInput.Update(gameTime);

_eventDrivenInput.KeyDown += (sender, keyDown) =>
{
  //Subscribe to the events 
};
```

Below are the currently available events. In the future I intend to add touch event to support the mobile platforms. 

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

## Roadmap

 - Add support for touch events.
 - Moved the project into [MonoGame.Extended](https://github.com/craftworkgames/MonoGame.Extended).

