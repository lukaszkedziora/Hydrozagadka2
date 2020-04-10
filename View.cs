using System;
using Microsoft.Xna.Framework;
using SadConsole;
using Console = SadConsole.Console;

namespace Hydrozagadka2 {

    public class View {
        static public void DrawMapScreenBackground (Console map) {
            int glyph = 0;
            for (int column = 0; column < 28; column++) {
                for (int row = 0; row < 50; row++) {
                    map.SetGlyph (row, column, glyph);
                    glyph++;
                }
            }
        }
    }
    class TitleConsole : Console {
        public TitleConsole (string title) : base (50, 50) {
            Fill (Color.White, Color.Black, 176);
            Print (0, 0, title.Align (HorizontalAlignment.Center, Width), Color.Black, Color.Yellow);
        }
    }
}