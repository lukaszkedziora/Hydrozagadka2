using System;
using Microsoft.Data.Sqlite;

namespace Hydrozagadka2
{
    class Dialogi
    {
        public void SQLTest()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();

            //Use DB in project directory.  If it does not exist, create it:
            connectionStringBuilder.DataSource = "./Hydrozagadka2.db";

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                // read characters (position and glyph) - As excluded
                var selectCharacters = connection.CreateCommand();
                selectCharacters.CommandText = "SELECT * FROM Characters WHERE Name != 'As'";

                using (var reader = selectCharacters.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        
                        int positionX = Convert.ToInt32($"{reader.GetString(2)}");
                        int positionY = Convert.ToInt32($"{reader.GetString(3)}");
                        int glyph = Convert.ToInt32($"{reader.GetString(4)}");
                        System.Console.WriteLine(positionX + ", " + positionY + ", " + glyph);
                                                                      
                    }
                }

                // read dialogues - As and Jola
                var selectDIalogue = connection.CreateCommand();
                selectDIalogue.CommandText = "SELECT * FROM Player LEFT JOIN null ON SequenceAs = Sequence";

                using (var reader = selectDIalogue.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        System.Console.WriteLine($"Jola: {reader.GetString(5)}\n");
                        System.Console.WriteLine($"Odpowiedz:\n 1 {reader.GetString(2)} \n 2 Wyjdź");
                        string asAnswear = System.Console.ReadLine();

                        if (asAnswear == "2"){                                                  // TODO zrób coś ładniejszego :( - isAnswearCorrect?
                            break;
                        }                    
                        else if (asAnswear == "1"){
                            continue;
                        } 
                    }
                }
            }
        }
    }
}