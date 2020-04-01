using System;
using Microsoft.Xna.Framework;
using SadConsole;
using Console = SadConsole.Console;

namespace Hydrozagadka2 {

    public class View {

        public void ShowMenu(){
            throw new NotImplementedException();

        }

    }
    class TitleConsole : Console
    {
        public TitleConsole(string title)
            : base(50, 50)
        {
            Fill(Color.White, Color.Black, 176);
            Print(0, 0, title.Align(HorizontalAlignment.Center, Width), Color.Black, Color.Yellow);
        }
    }
}