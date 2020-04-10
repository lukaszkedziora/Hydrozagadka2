using System.Collections.Generic;
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
            SadConsole.Game.Create(200, 57);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

static void Init()
{   
   
    Menu MainMenu = new Menu();
    //MapScreen board1 = new MapScreen();
    
    Global.CurrentScreen = MainMenu.ConsoleMenu();
  
}
    }
}	