using System;
using SadConsole;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;

namespace Hydrozagadka2
{
    public static class Program
    {
        static void Main()
        {
            // Setup the engine and create the main window.
            SadConsole.Game.Create(200, 56);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();

            
        }

static void Init()
{   
    MapScreen board1 = new MapScreen();
    board1.IsFocused = true;
    Global.CurrentScreen = board1.Board1();
  
}
    }
}	