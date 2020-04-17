using System.Data;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Input;
using Console = SadConsole.Console;
using System;
using Microsoft.Data.Sqlite;
// using Microsoft.Xna.Framework.Input;
// using Microsoft.Xna.Framework.Media;
// using Microsoft.Xna.Framework.Graphics;
using SadConsole.Controls;

namespace Hydrozagadka2 {

    // MapScreen - player controls, displaying console

    public class Menu : ContainerConsole {

        public Console ConsoleMenu () {
            //Back console for menu
            var view = new View();
            SadConsole.Global.LoadFont ("main.font");
            var menuBackFont = SadConsole.Global.Fonts["main"].GetFont (SadConsole.Font.FontSizes.One);
            var mapConsoleWidth = (int) ((Global.RenderWidth / 32));
            var mapConsoleHeight = (int) ((Global.RenderHeight / 32));
            //consoleBackMenu.Components.Add(new MyMouseComponent ());
            var consoleBackMenu = new Console (mapConsoleWidth, mapConsoleHeight);
            consoleBackMenu.Font = menuBackFont;
            consoleBackMenu.Parent = this;
            view.DrawMapScreenBackground (consoleBackMenu, 28, 50);

            //Main menu buttons console
            var consoleMenu = new SadConsole.ControlsConsole (200, 1);
            //SadConsole.Global.FontDefault = SadConsole.Global.FontDefault.Master.GetFont(SadConsole.Font.FontSizes.Two);
            consoleMenu.Position = new Point (0, 56);
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

            var color = new SadConsole.Themes.Colors ();
            color.Appearance_ControlNormal.Background = Microsoft.Xna.Framework.Color.White;
            color.Appearance_ControlNormal.Foreground = Microsoft.Xna.Framework.Color.Black;
            color.Appearance_ControlOver.Background = Microsoft.Xna.Framework.Color.Black;
            color.Appearance_ControlOver.Foreground = Microsoft.Xna.Framework.Color.White;
            color.Appearance_ControlFocused.Background = Microsoft.Xna.Framework.Color.TransparentBlack;
            color.Appearance_ControlFocused.Foreground = Microsoft.Xna.Framework.Color.Black;
            color.Appearance_ControlMouseDown.Background = Microsoft.Xna.Framework.Color.Coral;
            color.Appearance_ControlMouseDown.Foreground = Microsoft.Xna.Framework.Color.White;
            consoleMenu.ThemeColors = color;

            void _cancelButton_Action (object sender, EventArgs e) {
                Board1 temp = new Board1 ();
                temp.IsFocused = true;
                Global.CurrentScreen = temp.ConsoleBoard1 ();
            }
            return consoleBackMenu;
        }

    }

    public class Board1 : ContainerConsole {
        public Console ConsoleFront { get; set; }
        public SadConsole.ControlsConsole ConsoleButtonDialogue { get; set; }
        public Console ConsoleDialogue;
        public Console ConsoleDialogueBck;
        public String name = "Kolega";
        public Font[] fontsBack = new Font[8];
        public Cell PlayerGlyph { get; set; }
        private Point _playerPosition;
        private Cell _playerPositionMapGlyph;
        View view = new View();

        

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
            //Font and font as background 
            SadConsole.Global.LoadFont ("colored.font");
            SadConsole.Global.LoadFont ("colored1.font");
            SadConsole.Global.LoadFont ("colored3.font");
            SadConsole.Global.LoadFont ("jola.font");
            SadConsole.Global.LoadFont ("kolega.font");
            SadConsole.Global.LoadFont ("profesor.font");
            SadConsole.Global.LoadFont ("agent.font");
            SadConsole.Global.LoadFont ("sklep.font");
            SadConsole.Global.LoadFont ("maharadza.font");

            // Local variable --> fonts 
            var normalSizedFont = SadConsole.Global.Fonts["colored"].GetFont (SadConsole.Font.FontSizes.One);
            var charactersFont = SadConsole.Global.Fonts["colored1"].GetFont (SadConsole.Font.FontSizes.One);
            var normalSizedFontPl = SadConsole.Global.Fonts["colored3"].GetFont (SadConsole.Font.FontSizes.One);
            var normalJola = SadConsole.Global.Fonts["jola"].GetFont (SadConsole.Font.FontSizes.One);
            var normalKolega = SadConsole.Global.Fonts["kolega"].GetFont (SadConsole.Font.FontSizes.One);
            var normalProfesor = SadConsole.Global.Fonts["profesor"].GetFont (SadConsole.Font.FontSizes.One);
            var normalAgent = SadConsole.Global.Fonts["agent"].GetFont (SadConsole.Font.FontSizes.One);
            var normalSklep = SadConsole.Global.Fonts["sklep"].GetFont (SadConsole.Font.FontSizes.One);
            var normalMaharadza = SadConsole.Global.Fonts["maharadza"].GetFont (SadConsole.Font.FontSizes.One);

            //Fonts array
            fontsBack[2] = normalJola;
            fontsBack[3] = normalKolega;
            fontsBack[4] = normalAgent;
            fontsBack[5] = normalProfesor;
            fontsBack[7] = normalSklep;
            fontsBack[6] = normalMaharadza;

            //Console declarations
            int row = 1;
            var MapConsole = new Console ((Global.RenderWidth / 32), (Global.RenderHeight / 32));
            var consoleStats = new Console (100, 5);
            var consoleHeader = new SadConsole.ControlsConsole (201, 1);
            ConsoleFront = new Console (Global.RenderWidth / 64, Global.RenderHeight / 64);
            ConsoleButtonDialogue = new SadConsole.ControlsConsole (20, 1);
            ConsoleDialogue = new Console (80, 8);
            ConsoleDialogueBck = new Console (100, 10);

            // MapConsole
            MapConsole.Font = normalSizedFont;
            MapConsole.Parent = this;
            view.DrawMapScreenBackground (MapConsole, 28, 50);

            // Console for displaying stats
            consoleStats.Position = new Point (95, 2);
            consoleStats.Fill (null, Color.LightCoral, null);
            consoleStats.Parent = MapConsole;

            //Dialogue console with Jola(back)
            ConsoleDialogueBck.Parent = MapConsole;
            ConsoleDialogueBck.IsVisible = false;
            ConsoleDialogueBck.Position = new Point (50, 20);

            //Dialogue console with Jola(front)
            ConsoleDialogue.Parent = ConsoleDialogueBck;
            ConsoleDialogue.Font = normalSizedFontPl;
            ConsoleDialogue.Position = new Point (3, 0);

            //Dialogue console with Jola(button)      
            ConsoleButtonDialogue.Position = new Point (127, 28);
            ConsoleButtonDialogue.Parent = MapConsole;
            ConsoleButtonDialogue.IsVisible = false;
            Button nextButton = null;
            Button exitButton = null;
            ConsoleButtonDialogue.Add (nextButton = new Button (10) {
                Text = "A",
                    Position = new Point (0, 0),

            });

            ConsoleButtonDialogue.Add (exitButton = new Button (10) {
                Text = "B",
                    Position = new Point (10, 0)

            });

            nextButton.Click += new System.EventHandler (_nextButton_Action);
            exitButton.Click += new System.EventHandler (_exitButton_Action);

            void _nextButton_Action (object sender, EventArgs e) {
                ConsoleDialogue.Clear ();
                row++;
                view.PrintDialogues (ConsoleDialogue, row, name);

            }

            void _exitButton_Action (object sender, EventArgs e) {
                row = 1;
                ConsoleButtonDialogue.IsVisible = false;
                ConsoleFront.IsVisible = true;
                ConsoleDialogueBck.IsVisible = false;
                ConsoleDialogue.Clear ();

            }
            //button colour
            var color = new SadConsole.Themes.Colors ();
            color.Appearance_ControlNormal.Background = Microsoft.Xna.Framework.Color.White;
            color.Appearance_ControlNormal.Foreground = Microsoft.Xna.Framework.Color.Black;
            color.Appearance_ControlOver.Background = Microsoft.Xna.Framework.Color.Black;
            color.Appearance_ControlOver.Foreground = Microsoft.Xna.Framework.Color.White;
            color.Appearance_ControlFocused.Background = Microsoft.Xna.Framework.Color.TransparentBlack;
            color.Appearance_ControlFocused.Foreground = Microsoft.Xna.Framework.Color.Black;
            color.Appearance_ControlMouseDown.Background = Microsoft.Xna.Framework.Color.Coral;
            color.Appearance_ControlMouseDown.Foreground = Microsoft.Xna.Framework.Color.White;
            ConsoleButtonDialogue.ThemeColors = color;

            // ConsoleButtonDialogue.Invalidated += (s, e) => {
            //     var host = (ControlsConsole) s;
            //
            //    Rectangle boxArea = host.Controls[0].Bounds;
            //     boxArea.Inflate (1, 1);
            //     //host.DrawBox (boxArea, new Cell (Color.Yellow, Color.Transparent), null, CellSurface.ConnectedLineThin);

            //     var themeColors = host.ThemeColors ?? SadConsole.Themes.Library.Default.Colors;

            //     host.Fill (null, Color.Transparent, null);
            //     var host2 = new Console(100,8);
            //     host2.Font = normalSizedFontPl;

            //     host2.Parent = host;
            //     host2.Fill (Color.White, Color.Transparent, null);
            //     host2.Position = new Point(0,0);
            //     View.PrintDialogues (host2, row, name);
            // };

            // Console for displaying the header and menu
            consoleHeader.Position = new Point (0, 56);
            consoleHeader.Fill (null, Color.LightCoral, null);
            consoleHeader.Parent = MapConsole;
            consoleHeader.ThemeColors = color;
            Button menuButton;

            consoleHeader.Add (menuButton = new Button (20) {
                Text = "Menu",
                    Position = new Point (0, 0)
            });
            menuButton.Click += new System.EventHandler (_menuButton_Action);

            void _menuButton_Action (object sender, EventArgs e) {
                Menu temp = new Menu ();
                temp.IsFocused = true;
                Global.CurrentScreen = temp.ConsoleMenu ();
            }


            // Console for displaying front and characters
            ConsoleFront.Font = charactersFont;
            ConsoleFront.Position = new Point (0, 0);
            ConsoleFront.Parent = MapConsole;
            view.PutCharactersOnBoard (ConsoleFront);

            // Setup player
            PlayerGlyph = new Cell (Color.White, Color.Transparent, 1);
            _playerPosition = new Point (23, 12);
            _playerPositionMapGlyph = new Cell ();
            _playerPositionMapGlyph.CopyAppearanceFrom (ConsoleFront[_playerPosition.X, _playerPosition.Y]);
            PlayerGlyph.CopyAppearanceTo (ConsoleFront[_playerPosition.X, _playerPosition.Y]);

            return MapConsole;
        }

        public string CheckCharactersPosition (Point point) {

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
                        int characterNumber = reader.GetInt16 (0);
                        int characterStatus = reader.GetInt16 (6);

                        Point character = new Point (positionX, positionY);
                        if (character == point && characterStatus == 1) {
                            ConsoleFront.IsVisible = false;
                            ConsoleButtonDialogue.IsVisible = true;
                            ConsoleDialogueBck.IsVisible = true;
                            ConsoleDialogueBck.Font = fontsBack[characterNumber];
                            string name = reader.GetString (1);
                            view.DrawMapScreenBackground (ConsoleDialogueBck, 10, 100);
                            view.PrintDialogues (ConsoleDialogue, 1, name);

                            return name;

                        } else {
                            ConsoleDialogue.Clear ();
                            ConsoleDialogueBck.IsVisible = false;
                            ConsoleFront.IsVisible = true;
                            ConsoleButtonDialogue.IsVisible = false;
                            continue;
                        }
                    }
                }
                return name;
            }

        }

        public override bool ProcessKeyboard (SadConsole.Input.Keyboard info) {
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
                name = CheckCharactersPosition (PlayerPosition);

                return true;
            }
            return false;
        }
    }

    // public class Sound : Microsoft.Xna.Framework.Game {

    //     GraphicsDeviceManager graphics;
    //     SpriteBatch spriteBatch;
    //     Song song;

    //     public Sound () {
    //         graphics = new GraphicsDeviceManager (this);
    //         Content.RootDirectory = "bin";
    //     }

    //     protected override void Initialize () {
    //         base.Initialize ();
    //     }

    //     protected override void LoadContent () {
    //         spriteBatch = new SpriteBatch (GraphicsDevice);

    //         this.song = Content.Load<Song> ("l");
    //         MediaPlayer.Play (song);
    //         //  Uncomment the following line will also loop the song
    //         //  MediaPlayer.IsRepeating = true;
    //         MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
    //     }

    //     void MediaPlayer_MediaStateChanged (object sender, System.EventArgs e) {
    //         // 0.0f is silent, 1.0f is full volume
    //         MediaPlayer.Volume -= 0.1f;
    //         MediaPlayer.Play (song);
    //     }

    //     protected override void Update (GameTime gameTime) {
    //         if (GamePad.GetState (PlayerIndex.One).Buttons.Back ==
    //             ButtonState.Pressed || Microsoft.Xna.Framework.Input.Keyboard.GetState ().IsKeyDown (
    //                 Keys.Escape))
    //             Exit ();

    //         base.Update (gameTime);
    //     }

    //     protected override void Draw (GameTime gameTime) {
    //         GraphicsDevice.Clear (Color.CornflowerBlue);
    //         base.Draw (gameTime);
    //     }
    // }

}
// public class Dialogue : SadConsole.ControlsConsole {
//     public Console ConsoleFront { get; set; }
//     public Console ConsoleButtonDialogue { get; set; }
//     public Cell PlayerGlyph { get; set; }
//     private Point _playerPosition;
//     private Cell _playerPositionMapGlyph;

//     public Dialogue (int width, int height) : base (width, height) { }

//     protected override void Invalidate () {
//         base.Invalidate ();
//         // Dialogue console with Jola
//         //ConsoleButtonDialogue.Font = normalSizedFont;
//         Position = new Point (50, 20);
//         //ConsoleButtonDialogue.DefaultBackground = Color.Transparent;
//         Fill (Color.White, Color.LightCoral, 0);
//         //ConsoleButtonDialogue.Components.Add (new MyMouseComponent ());
//         //View.PrintDialogues (ConsoleButtonDialogue);
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
//         //ConsoleButtonDialogue.ThemeColors = color;

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