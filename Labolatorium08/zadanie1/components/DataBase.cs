using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace lab8
{
    public class DataBase
    {
        // Zadanie 1: Metoda do wczytywania danych z pliku CSV.
        public (List<List<object>?>, List<string>) readCSV(string path, char delimiter)
        {
            Console.WriteLine("Odczytanie danych z pliku CSV");
            List<List<object>?> data = new List<List<object>?>();
            List<string> header = new List<string>();
            string[] lines = System.IO.File.ReadAllLines(path);

            // Utwórz nagłówek.
            header = lines[0].Split(delimiter).ToList();

            //Utwórz dane pętla po liniach
            foreach (string line in lines.Skip(1))
            {
                string[] values = line.Split(delimiter);
                List<object>? row = new List<object>();
                if (values.Length != header.Count)
                    throw new Exception("Invalid CSV format");


                // Jeśli wartość jest pusta.
                foreach (string value in values)
                {
                    // Jeśli wartość jest pusta.
                    if (string.IsNullOrWhiteSpace(value))
                        row.Add(null);

                    // Spróbuj parsować jako double.
                    else if (value.All(c => char.IsDigit(c) || c == '.'))
                        row.Add(Convert.ToDouble(double.Parse(value)));



                    // Spróbuj parsować jako string.
                    else
                        row.Add(value);
                }
                data.Add(row);
            }

            // Pętla po kolumnach danych, jeśli cała kolumna jest typu int, sparsuj ją do int. Pętla po kolumnach.
            for (int i = 0; i < header.Count; i++)
            {
                bool isIntColumn = true;
                int intValue = 0;

                // Pętla sprawdzająca typy.
                foreach (List<object>? row in data)
                    if (row != null && !int.TryParse(row[i]?.ToString(), out intValue))
                    {
                        isIntColumn = false;
                        break;
                    }
                // Pętla parsująca.
                if (isIntColumn)
                {
                    for (int j = 0; j < data.Count; j++)
                        if (data[j] != null)
                        {
                            data[j][i] = int.Parse(data[j][i].ToString());
                        }
                }
            }


            // Wypisanie
            System.Console.WriteLine(string.Join(" ", header));
            foreach (List<object> row in data)
                System.Console.WriteLine(string.Join(" | ", row));

            return (data, header);
        }


        // Zadanie 2: Określenie typów kolumn.
        public Dictionary<string, Tuple<Type, bool>> ColumnTypes(List<List<object>?> data, List<string> headers)
        {
            Console.WriteLine("Określenie typów kolumn");
            var columnTypes = new Dictionary<string, Tuple<Type, bool>>();

            // Pętla po kolumnach.
            foreach (var header in headers)
            {
            var column = data.Select(row => row[headers.IndexOf(header)]).ToList();
            var canBeNull = column.Contains(null);

            // Typ pierwszej niepustej wartości.
            var type = column.First(value => value != null).GetType();

            // Sprawdź, czy wszystkie wartości mają ten sam typ.
            var allSameType = column.All(value => value == null || value.GetType() == type);

            // Dodaj do słownika.
            columnTypes.Add(header, new Tuple<Type, bool>(type, canBeNull));

            // Wydrukuj informacje o kolumnie.
            Console.WriteLine($"Kolumna {header} jest typu {type} i może być pusta: {canBeNull}");

            // Jeśli wartości w kolumnie nie są jednego typu.
            if (!allSameType)
                Console.WriteLine($"Kolumna {header} ma różne typy.");
            }
            return columnTypes;
        }


        // Zadanie 3: Utwórz tabelę w bazie danych na podstawie typów kolumn.
        public void createTable(Dictionary<string, Tuple<Type, bool>> columns, string tableName, SqliteConnection connection)
        {
            Console.WriteLine("Utworzenie tabeli");
            // Usuń tabelę, jeśli istnieje.
            SqliteCommand delTableCmd = connection.CreateCommand();
            delTableCmd.CommandText = "DROP TABLE IF EXISTS " + tableName;
            delTableCmd.ExecuteNonQuery();

            // Utwórz tabelę.
            SqliteCommand createTableCmd = connection.CreateCommand();
            string create_command = "CREATE TABLE " + tableName + " (";
            foreach (KeyValuePair<string, Tuple<Type, bool>> column in columns)
            {
                create_command += $"{column.Key} {GetSqliteType(column.Value.Item1)}";
                // Jeśli niepusty.
                if (!column.Value.Item2)
                    create_command += " NOT NULL";
                create_command += ",";
            }
            create_command = create_command.TrimEnd(',') + ")";

            // Wyślij polecenie.
            System.Console.WriteLine(create_command);
            createTableCmd.CommandText = create_command;
            createTableCmd.ExecuteNonQuery();

        }

        // Zadanie 4: Wstawianie danych do tabeli.
        public void InsertData(List<List<object>?> data, List<string> header, string tableName, SqliteConnection connection)
        {
            Console.WriteLine("Wstawienie danych do tabeli");
            var insertCmd = connection.CreateCommand();

            foreach (var row in data)
            {
                var insertCommandBuilder = new StringBuilder();
                insertCommandBuilder.Append($"INSERT INTO {tableName} VALUES (");

                foreach (var value in row ?? Enumerable.Repeat((object)null, data.First().Count))
                {
                    if (value == null)
                        insertCommandBuilder.Append("NULL, ");
                    else if (value is string)
                        insertCommandBuilder.Append($"'{value}', ");
                    else
                        insertCommandBuilder.Append($"{value}, ");
                }

                var insertCommandText = insertCommandBuilder.ToString().TrimEnd(',', ' ') + ")";
                Console.WriteLine(insertCommandText);
        
                insertCmd.CommandText = insertCommandText;
                insertCmd.ExecuteNonQuery();
            }
        }


        // Zadanie 5: Wydrukuj zawartość tabeli.
        public void printTable(string tableName, SqliteConnection connection)
        {
            Console.WriteLine("Wyświetlenie tabeli");
            // Wyświetl tabelę.
            SqliteCommand printCmd = connection.CreateCommand();
            printCmd.CommandText = "SELECT * FROM " + tableName;
            SqliteDataReader reader = printCmd.ExecuteReader();

            // Nagłówek
            for (int i = 0; i < reader.FieldCount; i++)
                System.Console.Write(reader.GetName(i) + " ");
                System.Console.WriteLine("");
            
            // Records
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                    System.Console.Write(reader.GetValue(i) + " | ");
                System.Console.WriteLine();
            }
        }












        private string GetSqliteType(Type type)
        {
            // Map C# types to SQLite types
            if (type == typeof(int))
                return "INTEGER";
            else if (type == typeof(double))
                return "REAL";
            else if (type == typeof(string))
                return "TEXT";
            else if (type == typeof(bool))
                return "BOOLEAN";
            else
                throw new NotSupportedException($"Type {type.FullName} not supported by SQLite.");

        }


    }
}
