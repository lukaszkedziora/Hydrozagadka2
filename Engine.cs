using System;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
// using SadConsole.Components;
using SadConsole.Input;

namespace Hydrozagadka2 {

    public class Engine {
    }


    // MapScreen - player controls, displaying console
    public class MapScreen: ContainerConsole
    {
        public Console MapConsole { get; }
        public Cell PlayerGlyph { get; set; }
        private Point _playerPosition;
        private Cell _playerPositionMapGlyph;

        public Point PlayerPosition
        {
            get => _playerPosition;
            private set
            {
                // Test new position
                if (value.X < 0 || value.X >= MapConsole.Width ||
                    value.Y < 2 || value.Y >= MapConsole.Height-10)
                    return;

                // Restore map cell
                _playerPositionMapGlyph.CopyAppearanceTo(MapConsole[_playerPosition.X, _playerPosition.Y]);
                // Move player
                _playerPosition = value;
                // Save map cell
                _playerPositionMapGlyph.CopyAppearanceFrom(MapConsole[_playerPosition.X, _playerPosition.Y]);
                // Draw player
                PlayerGlyph.CopyAppearanceTo(MapConsole[_playerPosition.X, _playerPosition.Y]);
                // Redraw the map
                MapConsole.IsDirty = true;
            }
        }
        
        public MapScreen()
        {
            var mapConsoleWidth = (int)((Global.RenderWidth / Global.FontDefault.Size.X) * 1.0);
            var mapConsoleHeight = (int)((Global.RenderHeight / Global.FontDefault.Size.Y) * 1.0);
            var consoleStats = new Console(150, 20);
            var consoleHeader = new Console(150, 2);

            // Setup map
            MapConsole = new Console(mapConsoleWidth, mapConsoleHeight);
            MapConsole.DrawBox(new Microsoft.Xna.Framework.Rectangle(0, 0, MapConsole.Width, MapConsole.Height), new Cell(Color.White, Color.DarkGray, 0));
            MapConsole.Parent = this;

            //MapConsole.Position = new Point(0, 5);
            MapConsole.Fill(null, Color.Black, null);

            // Console for displaying stats
            consoleStats.Position = new Point(0, 40);
            consoleStats.Fill(null, Color.LightCoral, null);
            consoleStats.Print(1, 1, "Player stats");
            consoleStats.Parent = MapConsole;

            // Console for diplaying the header and menu
            consoleHeader.Position = new Point(0, 0);
            consoleHeader.Fill(null, Color.LightCoral, null);
            consoleHeader.Print(65, 1, "HYDROZAGADKA");
            consoleHeader.Parent = MapConsole;           

            // Setup player
            PlayerGlyph = new Cell(Color.White, Color.Black, 1);
            _playerPosition = new Point(5, 5);
            _playerPositionMapGlyph = new Cell();
            _playerPositionMapGlyph.CopyAppearanceFrom(MapConsole[_playerPosition.X, _playerPosition.Y]);
            PlayerGlyph.CopyAppearanceTo(MapConsole[_playerPosition.X, _playerPosition.Y]);
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