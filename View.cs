using System;
using System.Linq;
using Microsoft.Xna.Framework;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Data.Sqlite;

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

        public static void PutCharactersOnBoard (Console console) {

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

                        int positionX = Convert.ToInt32 ($"{reader.GetString(2)}");
                        int positionY = Convert.ToInt32 ($"{reader.GetString(3)}");
                        int glyph = Convert.ToInt32 ($"{reader.GetString(4)}");
                        console.SetGlyph (positionX, positionY, glyph);

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
}