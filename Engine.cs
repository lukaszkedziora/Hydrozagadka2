using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Input;
using Console = SadConsole.Console;
using System;
using SadConsole.Components;
using SadConsole.Controls;
using SadConsole.Themes;

namespace Hydrozagadka2 {

    // MapScreen - player controls, displaying console
    public class MapScreen : ContainerConsole {
        public Console MapConsole { get; set; }
        public Console ConsoleFront { get; set; }
        public Console consoleBackMenu { get; set; }

        public Cell PlayerGlyph { get; set; }
        private Point _playerPosition;
        private Cell _playerPositionMapGlyph;

        public Point PlayerPosition {
            get => _playerPosition;
            private set {
                // Test new position
                if (value.X < 0 || value.X >= ConsoleFront.Width ||
                    value.Y < 2 || value.Y >= ConsoleFront.Height)
                    return;

                // Restore map cell
                _playerPositionMapGlyph.CopyAppearanceTo (ConsoleFront[_playerPosition.X, _playerPosition.Y]);
                // Move player
                _playerPosition = value;
                // Save map cell
                _playerPositionMapGlyph.CopyAppearanceFrom (ConsoleFront[_playerPosition.X, _playerPosition.Y]);
                // Draw player
                PlayerGlyph.CopyAppearanceTo (ConsoleFront[_playerPosition.X, _playerPosition.Y]);
                // Redraw the map
                ConsoleFront.IsDirty = true;
                
            }
        }

        public Console MainMenu () {
            //Console consoleBackMenu;
            SadConsole.Global.LoadFont ("main.font");
            var menuBackFont = SadConsole.Global.Fonts["main"].GetFont (SadConsole.Font.FontSizes.One);
            var mapConsoleWidth = (int) ((Global.RenderWidth / 32));
            var mapConsoleHeight = (int) ((Global.RenderHeight / 32));
            //consoleBackMenu.Components.Add(new MyMouseComponent ());

            consoleBackMenu = new Console (mapConsoleWidth, mapConsoleHeight);
            consoleBackMenu.Font = menuBackFont;
            DrawMapScreenBackground (consoleBackMenu);
            consoleBackMenu.Parent = this;

            var consoleMenu = new SadConsole.ControlsConsole (200, 2);
            //SadConsole.Global.FontDefault = SadConsole.Global.FontDefault.Master.GetFont(SadConsole.Font.FontSizes.Two);
            consoleMenu.Position = new Point (0, 55);
            consoleMenu.Fill (null, null, null);;
            consoleMenu.Parent = consoleBackMenu;
            consoleMenu.Components.Add(new MyMouseComponent());

            Button newGameButton = null;
            Button loadButton = null;
            Button saveButton = null;
            Button creditsButton = null;
            Button exitButton = null;
            Button helpButton = null;

            consoleMenu.Add (newGameButton = new Button (25, 2) {
                Text = "New game",
                    Position = new Point (0, 0)
            });
            
            consoleMenu.Add (loadButton = new Button (25, 2) {
                Text = "Load game",
                    Position = new Point (26, 0)
            });

            consoleMenu.Add (saveButton = new Button (25, 2) {
                Text = "Save game",
                    Position = new Point (52, 0)

            });

            consoleMenu.Add (helpButton = new Button (25, 2) {
                Text = "Help",
                    Position = new Point (78, 0)

            });

            consoleMenu.Add (creditsButton = new Button (25, 2) {
                Text = "Credits",
                    Position = new Point (104, 0)
            });

            consoleMenu.Add (exitButton = new Button (25, 2) {
                Text = "Exit",
                    Position = new Point (130, 0)
            });

            newGameButton.Click += new System.EventHandler (_cancelButton_Action);
            //loadButton.Click += new System.EventHandler (_cancelButton_Action);
            //saveButton.Click += new System.EventHandler (_cancelButton_Action);
            //exitButton.Click += new System.EventHandler (_cancelButton_Action);
            //creditsButton.Click += new System.EventHandler (_cancelButton_Action);
            //helpButton.Click += new System.EventHandler (_cancelButton_Action);

            void _cancelButton_Action (object sender, EventArgs e) {
                Global.CurrentScreen = Board1 ();
            }
            return consoleBackMenu;
        }

        public Console Board1 () {
            SadConsole.Global.LoadFont ("colored.font");
            SadConsole.Global.LoadFont ("colored1.font");
            var charactersSizedFont = SadConsole.Global.Fonts["colored1"].GetFont (SadConsole.Font.FontSizes.One);
            var normalSizedFont = SadConsole.Global.Fonts["colored"].GetFont (SadConsole.Font.FontSizes.One);
            var mapConsoleWidth = (int) ((Global.RenderWidth / 32));
            var mapConsoleHeight = (int) ((Global.RenderHeight / 32));
            var consoleStats = new Console (100, 5);
            var consoleHeader = new SadConsole.ControlsConsole (201, 2);
            ConsoleFront = new Console (Global.RenderWidth / 64, Global.RenderHeight / 64);
            consoleHeader.Components.Add (new MyMouseComponent ());

            // Setup map
            MapConsole = new Console (mapConsoleWidth, mapConsoleHeight);
            MapConsole.Font = normalSizedFont;
            DrawMapScreenBackground (MapConsole);
            MapConsole.Parent = this;

            // Console for displaying stats
            consoleStats.Position = new Point (95, 2);
            consoleStats.Fill (null, Color.LightCoral, null);
            consoleStats.Print (1, 1, "Player stats");
            consoleStats.Parent = MapConsole;

            // Console for displaying the header and menu
            consoleHeader.Position = new Point (0, 55);
            consoleHeader.Fill (null, Color.LightCoral, null);
            consoleHeader.Print (65, 1, "HYDROZAGADKA");
            consoleHeader.Parent = MapConsole;
            Button menuButton;

            consoleHeader.Add (menuButton = new Button (20, 2) {
                Text = "Menu",
                    Position = new Point (0, 0)
            });
            menuButton.Click += new System.EventHandler (_cancelButton_Action);

            void _cancelButton_Action (object sender, EventArgs e) {
                Global.CurrentScreen = MainMenu();

            }

            // Console for displaying front and characters
            ConsoleFront.Font = charactersSizedFont;
            ConsoleFront.Position = new Point (0, 0);
            ConsoleFront.Fill (null, null, null);
            ConsoleFront.Parent = MapConsole;
            ConsoleFront.SetGlyph (2, 4, 2);
            ConsoleFront.SetGlyph (3, 5, 5);
            ConsoleFront.SetGlyph (19, 8, 6);
            ConsoleFront.SetGlyph (16, 2, 3);
            ConsoleFront.SetGlyph (9, 2, 7);
            ConsoleFront.SetGlyph (17, 3, 4);

            // Setup player
            PlayerGlyph = new Cell (Color.White, Color.Transparent, 1);
            _playerPosition = new Point (23, 12);
            _playerPositionMapGlyph = new Cell ();
            _playerPositionMapGlyph.CopyAppearanceFrom (ConsoleFront[_playerPosition.X, _playerPosition.Y]);
            PlayerGlyph.CopyAppearanceTo (ConsoleFront[_playerPosition.X, _playerPosition.Y]);

            return MapConsole;

        }
        public void DrawMapScreenBackground (Console map) {
            int glyph = 0;
            for (int column = 0; column < 28; column++) {
                for (int row = 0; row < 50; row++) {
                    map.SetGlyph (row, column, glyph);
                    glyph++;
                }
            }
        }
        public override bool ProcessKeyboard (Keyboard info) {
            Point newPlayerPosition = PlayerPosition;

            if (info.IsKeyPressed (Microsoft.Xna.Framework.Input.Keys.Up))
                newPlayerPosition += SadConsole.Directions.North;
            else if (info.IsKeyPressed (Microsoft.Xna.Framework.Input.Keys.Down))
                newPlayerPosition += SadConsole.Directions.South;

            if (info.IsKeyPressed (Microsoft.Xna.Framework.Input.Keys.Left))
                newPlayerPosition += SadConsole.Directions.West;
            else if (info.IsKeyPressed (Microsoft.Xna.Framework.Input.Keys.Right))
                newPlayerPosition += SadConsole.Directions.East;

            if (newPlayerPosition != PlayerPosition) {
                PlayerPosition = newPlayerPosition;
                return true;
            }
            return false;
        }
    }
    class MyMouseComponent : MouseConsoleComponent {
        public override void ProcessMouse (SadConsole.Console console, MouseConsoleState state, out bool handled) {
            if (state.IsOnConsole)
                console.SetForeground (state.CellPosition.X, state.CellPosition.Y, Color.Black);

            handled = false;
        }
    }

    

    class MyTheme : SadConsole.Themes.ControlsConsoleTheme {
        Cell CustomPrintStyle;

        public override void Draw (ControlsConsole console, CellSurface hostSurface) {
            // Use the existing theme's drawing which clears the console with the FillStyle property
            base.Draw (console, hostSurface);

            hostSurface.Print (1, 1, "Hello World 2", CustomPrintStyle);
        }
    }

    /*class MyConsole : SadConsole.ControlsConsole
    {
        public MyConsole(int width, int height) : base(width, height) { }

        public override void Invalidate()
        {
            base.Invalidate();

            Print(1, 1, "Hello World", Theme.Colors.Green, Theme.Colors.GreenDark);
        }
    }*/

}