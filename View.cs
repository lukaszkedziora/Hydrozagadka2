using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Input;
using Console = SadConsole.Console;
using System;
using Microsoft.Data.Sqlite;
using SadConsole.Controls;

namespace Hydrozagadka2 {

    public class View {

        public SqliteConnectionStringBuilder connectionStringBuilder;
        


        public View(){
            connectionStringBuilder = new SqliteConnectionStringBuilder ();
            connectionStringBuilder.DataSource = "./Hydrozagadka2.db";

        }
        public void DrawMapScreenBackground (Console map, int x, int y) {
            int glyph = 0;
            for (int column = 0; column < x; column++) {
                for (int row = 0; row < y; row++) {
                    map.SetGlyph (row, column, glyph);
                    glyph++;
                }
            }
        }

        public void PutCharactersOnBoard (Console console) {

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

        public void PrintDialogues (Console console, int row, string name) {

        using (var connection = new SqliteConnection (connectionStringBuilder.ConnectionString)) {
                connection.Open ();
                var countRows = connection.CreateCommand ();
                countRows.CommandText = $"SELECT Count(TalksTo) FROM Player LEFT JOIN {name} ON SequenceAs = Sequence WHERE TalksTo = '{name}'";

                int count = Convert.ToInt32 (countRows.ExecuteScalar ());

                if (name != null && row <= count && row != 0) {
                    var selectDIalogue = connection.CreateCommand ();
                    selectDIalogue.CommandText = $"SELECT * FROM Player LEFT JOIN {name} ON SequenceAs = Sequence WHERE TalksTo = '{name}' AND  Sequence = {row}";
                    using (var reader = selectDIalogue.ExecuteReader ()) {
                        reader.Read ();
                        console.Print (0, 2, $"{name}: {reader.GetString(5)}", Color.Black, Color.White);
                        console.Print (0, 5, "Odpowiedź:", Color.Black);
                        console.Print (0, 6, $"A:{reader.GetString(2)}", Color.Black, Color.White);
                        console.Print (0, 7, "B: Wyjdź", Color.Black, Color.White);
                        //TODO: StatusChange to zero :))

                    }
                    if (count == row) {
                        UpdateCharacterStatus(name);
                    }
                }

            }

        }
        public void UpdateCharacterStatus (string name) {
        
            using (var connection = new SqliteConnection (connectionStringBuilder.ConnectionString)) {
                connection.Open ();

                var checkStatus = connection.CreateCommand ();
                checkStatus.CommandText = $"SELECT ChStatus FROM Characters WHERE Name = '{name}'";
                var unlockDialogue = checkStatus.ExecuteScalar ();

                var updateStatus = connection.CreateCommand ();
                updateStatus.CommandText = $"UPDATE Characters SET Status = 1 WHERE Name = '{unlockDialogue}'";
                var statusChanged = updateStatus.ExecuteScalar ();
            }
        }
    }
}