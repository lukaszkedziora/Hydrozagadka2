using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Input;
using Console = SadConsole.Console;
using System;
using Microsoft.Data.Sqlite;
using SadConsole.Controls;

namespace Hydrozagadka2 {

    // MapScreen - player controls, displaying console

    public class Menu : ContainerConsole {

        public Console ConsoleMenu () {
            //Back console for menu
            SadConsole.Global.LoadFont ("main.font");
            var menuBackFont = SadConsole.Global.Fonts["main"].GetFont (SadConsole.Font.FontSizes.One);
            var mapConsoleWidth = (int) ((Global.RenderWidth / 32));
            var mapConsoleHeight = (int) ((Global.RenderHeight / 32));
            //consoleBackMenu.Components.Add(new MyMouseComponent ());
            var consoleBackMenu = new Console (mapConsoleWidth, mapConsoleHeight);
            consoleBackMenu.Font = menuBackFont;
            consoleBackMenu.Parent = this;
            View.DrawMapScreenBackground (consoleBackMenu);

            //Main menu buttons console
            var consoleMenu = new SadConsole.ControlsConsole (200, 1);
            //SadConsole.Global.FontDefault = SadConsole.Global.FontDefault.Master.GetFont(SadConsole.Font.FontSizes.Two);
            consoleMenu.Position = new Point (0, 56);
            consoleMenu.Fill (null, null, null);;
            consoleMenu.Parent = consoleBackMenu;
            consoleMenu.IsFocused = false;

            Button newGameButton = null;
            Button loadButton = null;
            Button saveButton = null;
            Button creditsButton = null;
            Button exitButton = null;
            Button helpButton = null;

            consoleMenu.Add (newGameButton = new Button (25) {
                Text = "New game",
                    Position = new Point (0, 0)
            });

            consoleMenu.Add (loadButton = new Button (25) {
                Text = "Load game",
                    Position = new Point (26, 0)
            });

            consoleMenu.Add (saveButton = new Button (25) {
                Text = "Save game",
                    Position = new Point (52, 0)

            });

            consoleMenu.Add (helpButton = new Button (25) {
                Text = "Help",
                    Position = new Point (78, 0)

            });

            consoleMenu.Add (creditsButton = new Button (25) {
                Text = "Credits",
                    Position = new Point (104, 0)
            });

            consoleMenu.Add (exitButton = new Button (25) {
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
                Board1 temp = new Board1 ();
                temp.IsFocused = true;
                Global.CurrentScreen = temp.ConsoleBoard1 ();
                Console A = new Console (2, 5);
                string b;
                b = A.ToString ();

            }
            return consoleBackMenu;
        }

    }

    public class Board1 : ContainerConsole {
        public Console ConsoleFront { get; set; }
        public SadConsole.ControlsConsole ConsoleJola { get; set; }
        public String name;
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

        public Console ConsoleBoard1 () {
            SadConsole.Global.LoadFont ("colored.font");
            SadConsole.Global.LoadFont ("colored1.font");
            SadConsole.Global.LoadFont ("colored3.font");
            int row = 1;

            var charactersSizedFont = SadConsole.Global.Fonts["colored1"].GetFont (SadConsole.Font.FontSizes.One);
            var normalSizedFont = SadConsole.Global.Fonts["colored"].GetFont (SadConsole.Font.FontSizes.One);
            var normalSizedFontSmall = SadConsole.Global.Fonts["colored3"].GetFont (SadConsole.Font.FontSizes.One);

            var MapConsole = new Console ((Global.RenderWidth / 32), (Global.RenderHeight / 32));

            var consoleStats = new Console (100, 5);
            var consoleHeader = new SadConsole.ControlsConsole (201, 1);
            ConsoleFront = new Console (Global.RenderWidth / 64, Global.RenderHeight / 64);
            ConsoleJola = new SadConsole.ControlsConsole (100, 25);

            //consoleHeader.Components.Add (new MyMouseComponent ());

            // Setup map

            MapConsole.Font = normalSizedFont;
            View.DrawMapScreenBackground (MapConsole);
            MapConsole.Parent = this;

            // Console for displaying stats
            consoleStats.Position = new Point (95, 2);
            consoleStats.Fill (null, Color.LightCoral, null);
            consoleStats.Font = normalSizedFontSmall;
            consoleStats.Print (1, 1, "Ą | Ć | Ę | Ł | Ń | Ó | Ś | Ź | Ż");
            consoleStats.Parent = MapConsole;

            //Dialogue console with Jola
            //ConsoleJola.Font = normalSizedFont;
            ConsoleJola.Position = new Point (50, 20);
            //ConsoleJola.DefaultBackground = Color.Transparent;
            //View.PrintDialogues (ConsoleJola);
            ConsoleJola.Parent = MapConsole;
            ConsoleJola.IsVisible = false;

            //Button dialogue console 

            Button nextButton = null;
            Button exitButton = null;
            // var consoleTheme = SadConsole.Themes.Library.Default.Clone();
            // var color = new SadConsole.Themes.Colors ();
            // nextButton.ThemeColors = Microsoft.Xna.Framework.Color.Aquamarine;
            // color.Appearance_ControlNormal.Background = Microsoft.Xna.Framework.Color.Transparent;
            // color.Appearance_ControlNormal.Foreground = Microsoft.Xna.Framework.Color.Black;
            // color.Appearance_ControlFocused.Foreground = Microsoft.Xna.Framework.Color.Black;
            //color.Appearance_ControlSelected.Background = Microsoft.Xna.Framework.
            //ConsoleJola.ThemeColors = color;

            ConsoleJola.Add (nextButton = new Button (20) {
                Text = "A",
                    Position = new Point (25, 19),

            });

            ConsoleJola.Add (exitButton = new Button (20) {
                Text = "B",
                    Position = new Point (55, 19)

            });

            nextButton.Click += new System.EventHandler (_nextButton_Action);

            void _nextButton_Action (object sender, EventArgs e) {
                row++;

            }

            ConsoleJola.Invalidated += (s, e) => {
                var host = (ControlsConsole) s;
                Rectangle boxArea = host.Controls[0].Bounds;
                boxArea.Inflate (1, 1);
                //host.DrawBox (boxArea, new Cell (Color.Yellow, Color.Transparent), null, CellSurface.ConnectedLineThin);

                var themeColors = host.ThemeColors ?? SadConsole.Themes.Library.Default.Colors;

                //host.Fill (Color.White, Color.LightCoral, null);
                host.Font = normalSizedFontSmall;
                //var host2 = new Console(5,6);
                //host2.Parent = host;
                //host2.Fill (Color.White, Color.Black, null);
                //host2.Position = new Point(0,0);
                View.PrintDialogues (host, row, name);
            };

            // Console for displaying the header and menu
            consoleHeader.Position = new Point (0, 56);
            consoleHeader.Fill (null, Color.LightCoral, null);
            consoleHeader.Parent = MapConsole;
            Button menuButton;

            consoleHeader.Add (menuButton = new Button (20) {
                Text = "Menu",
                    Position = new Point (0, 0)
            });
            menuButton.Click += new System.EventHandler (_cancelButton_Action);

            void _cancelButton_Action (object sender, EventArgs e) { }

            // Console for displaying front and characters
            ConsoleFront.Font = charactersSizedFont;
            ConsoleFront.Position = new Point (0, 0);
            ConsoleFront.Parent = MapConsole;
            //ConsoleFront.IsVisible = false;
            View.PutCharactersOnBoard (ConsoleFront);

            // Setup player
            PlayerGlyph = new Cell (Color.White, Color.Transparent, 1);
            _playerPosition = new Point (23, 12);
            _playerPositionMapGlyph = new Cell ();
            _playerPositionMapGlyph.CopyAppearanceFrom (ConsoleFront[_playerPosition.X, _playerPosition.Y]);
            PlayerGlyph.CopyAppearanceTo (ConsoleFront[_playerPosition.X, _playerPosition.Y]);

            return MapConsole;
        }

        public void CheckCharactersPosition (Point point) {

            var connectionStringBuilder = new SqliteConnectionStringBuilder ();

            //Use DB in project directory.  If it does not exist, create it:
            connectionStringBuilder.DataSource = "./Hydrozagadka2.db";

            using (var connection = new SqliteConnection (connectionStringBuilder.ConnectionString)) {
                connection.Open ();

                // read characters (position and glyph) - As excluded
                var selectCharacters = connection.CreateCommand ();
                selectCharacters.CommandText = "SELECT * FROM Characters WHERE Name != 'As'";

                using (var reader = selectCharacters.ExecuteReader ()) {

                    while (reader.Read ()) {

                        int positionX = reader.GetInt16 (2);
                        int positionY = reader.GetInt16 (3);
                        string name = reader.GetString (1);

                        Point character = new Point (positionX, positionY);
                        if (character == point) {
                            ConsoleFront.IsVisible = false;
                            ConsoleJola.IsVisible = true;
                            //return name;
                            break;

                        } else {
                            ConsoleFront.IsVisible = true;
                            ConsoleJola.IsVisible = false;
                            //return null;
                        }

                    }
                }
                //return null;

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
                CheckCharactersPosition (PlayerPosition);
                return true;
            }
            return false;
        }
    }
    // public class Dialogue : SadConsole.ControlsConsole {
    //     public Console ConsoleFront { get; set; }
    //     public Console ConsoleJola { get; set; }
    //     public Cell PlayerGlyph { get; set; }
    //     private Point _playerPosition;
    //     private Cell _playerPositionMapGlyph;

    //     public Dialogue (int width, int height) : base (width, height) { }

    //     protected override void Invalidate () {
    //         base.Invalidate ();
    //         // Dialogue console with Jola
    //         //ConsoleJola.Font = normalSizedFont;
    //         Position = new Point (50, 20);
    //         //ConsoleJola.DefaultBackground = Color.Transparent;
    //         Fill (Color.White, Color.LightCoral, 0);
    //         //ConsoleJola.Components.Add (new MyMouseComponent ());
    //         //View.PrintDialogues (ConsoleJola);
    //         Print (3, 3, "fff");
    //         //this.Parent = this;
    //         this.IsVisible = true;

    //         //Button dialogue console 

    //         Button nextButton = new SadConsole.Controls.Button (11);
    //         Button exitButton = null;
    //         //var consoleTheme = SadConsole.Themes.Library.Default.Clone();
    //         var color = new SadConsole.Themes.Colors ();
    //         //nextButton.ThemeColors = Microsoft.Xna.Framework.Color.Aquamarine;
    //         //color.Appearance_ControlNormal.Background = Microsoft.Xna.Framework.Color.Transparent;
    //         //color.Appearance_ControlNormal.Foreground = Microsoft.Xna.Framework.Color.Black;
    //         //color.Appearance_ControlFocused.Foreground = Microsoft.Xna.Framework.Color.Black;

    //         //color.Appearance_ControlSelected.Background = Microsoft.Xna.Framework.
    //         //ConsoleJola.ThemeColors = color;

    //         this.Add (nextButton = new Button (25) {
    //             Text = "next",
    //                 Position = new Point (0, 0),

    //         });

    //         this.Add (exitButton = new Button (25, 2) {
    //             Text = "exit",
    //                 Position = new Point (26, 10)

    //         });
    //     }
    // }

    // class MyMouseComponent : MouseConsoleComponent {
    //     public override void ProcessMouse (SadConsole.Console console, MouseConsoleState state, out bool handled) {
    //         if (state.IsOnConsole)
    //             console.SetForeground (state.CellPosition.X, state.CellPosition.Y, Color.Black);
    //         console.SetBackground (state.CellPosition.X, state.CellPosition.Y, Color.Black);

    //         handled = false;
    //     }
    // }

    // class MyTheme : SadConsole.Themes.ControlsConsoleTheme {

    // }

    // class MyConsole : SadConsole.ControlsConsole {
    //     public MyConsole (int width, int height) : base (width, height) { }

    //     protected override void Invalidate () {
    //         base.Invalidate ();

    //         Print (1, 1, "Hello World", Color.Aqua, Color.Black);
    //         Fill (Color.Black, Color.White, null);
    //     }
    // }

}