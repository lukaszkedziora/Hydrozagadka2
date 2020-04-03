using Microsoft.Xna.Framework;
using SadConsole;
// using SadConsole.Components;
using SadConsole.Input;
using Console = SadConsole.Console;


namespace Hydrozagadka2
{

    public class Engine
    {
    }


    // MapScreen - player controls, displaying console
    public class MapScreen : ContainerConsole
    {
        public Console MapConsole { get; }
        public Console  ConsoleFront { get; }
        public Cell PlayerGlyph { get; set; }
        private Point _playerPosition;
        private Cell _playerPositionMapGlyph;
   

        public Point PlayerPosition
        {
            get => _playerPosition;
            private set
            {
                // Test new position
                if (value.X < 0 || value.X >= ConsoleFront.Width ||
                    value.Y < 2 || value.Y >= ConsoleFront.Height)
                    return;

                // Restore map cell
                _playerPositionMapGlyph.CopyAppearanceTo(ConsoleFront[_playerPosition.X, _playerPosition.Y]);
                // Move player
                _playerPosition = value;
                // Save map cell
                _playerPositionMapGlyph.CopyAppearanceFrom(ConsoleFront[_playerPosition.X, _playerPosition.Y]);
                // Draw player
                PlayerGlyph.CopyAppearanceTo(ConsoleFront[_playerPosition.X, _playerPosition.Y]);
                // Redraw the map
                ConsoleFront.IsDirty = true;
            }
        }

        public MapScreen()
        {
            SadConsole.Global.LoadFont("colored.font");
            SadConsole.Global.LoadFont("colored1.font");
            var charactersSizedFont = SadConsole.Global.Fonts["colored1"].GetFont(SadConsole.Font.FontSizes.One);
            var normalSizedFont = SadConsole.Global.Fonts["colored"].GetFont(SadConsole.Font.FontSizes.One);
            var mapConsoleWidth = (int)((Global.RenderWidth / 32));
            var mapConsoleHeight = (int)((Global.RenderHeight / 32));
            var consoleStats = new Console(100, 5);
            var consoleHeader = new Console(200, 2);
            ConsoleFront = new Console(Global.RenderWidth / 64, Global.RenderHeight / 64);


            // Setup map
            MapConsole = new Console(mapConsoleWidth, mapConsoleHeight);
            MapConsole.Font = normalSizedFont;
            DrawMapScreenBackground(MapConsole);
            MapConsole.Parent = this;

            //MapConsole.Position = new Point(0, 5);
            MapConsole.Fill(null, null, null);

            // Console for displaying stats
            consoleStats.Position = new Point(95, 2);
            consoleStats.Fill(null, Color.LightCoral, null);
            consoleStats.Print(1, 1, "Player stats");
            consoleStats.Parent = MapConsole;

            // Console for displaying the header and menu
            consoleHeader.Position = new Point(0, 54);
            consoleHeader.Fill(null, Color.LightCoral, null);
            consoleHeader.Print(65, 1, "HYDROZAGADKA");
            consoleHeader.Parent = MapConsole;

            // Console for displaying front and characters
            ConsoleFront.Font = charactersSizedFont;
            ConsoleFront.Position = new Point(0, 0);
            ConsoleFront.Fill(null, null, null);
            ConsoleFront.Parent = MapConsole;
            ConsoleFront.SetGlyph(2, 4, 2);
            ConsoleFront.SetGlyph(3, 5, 5);
            ConsoleFront.SetGlyph(19, 8, 6);
            ConsoleFront.SetGlyph(16, 2, 3);
            ConsoleFront.SetGlyph(9, 2, 7);
            ConsoleFront.SetGlyph(17, 3, 4);


            // Setup player
            PlayerGlyph = new Cell(Color.White, Color.Black, 1);
            _playerPosition = new Point(23, 12);
            _playerPositionMapGlyph = new Cell();
            _playerPositionMapGlyph.CopyAppearanceFrom(ConsoleFront[_playerPosition.X, _playerPosition.Y]);
            PlayerGlyph.CopyAppearanceTo(ConsoleFront[_playerPosition.X, _playerPosition.Y]);

        }
        public void DrawMapScreenBackground(Console map)
        {
             int glyph = 0;
                 for (int column = 0; column < 28; column++)
             { 
                 for (int row = 0; row < 50; row++)
             {

                     map.SetGlyph(row, column, glyph);
                     glyph++;
                 }
             }
           //map.SetGlyph(20, 20, 8);
        }
        public override bool ProcessKeyboard(Keyboard info)
        {
            Point newPlayerPosition = PlayerPosition;

            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
                newPlayerPosition += SadConsole.Directions.North;
            else if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
                newPlayerPosition += SadConsole.Directions.South;

            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
                newPlayerPosition += SadConsole.Directions.West;
            else if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
                newPlayerPosition += SadConsole.Directions.East;

            if (newPlayerPosition != PlayerPosition)
            {
                PlayerPosition = newPlayerPosition;
                return true;
            }

            return false;
        }
    }

}



