using Microsoft.Data.Sqlite;
using lab8;
// Instaluj sqlitebrowser za pomocą "snap install sqlitebrowser", aby hostować bazę danych.
// Inicjalizacja obiektu bazy danych
DataBase db = new DataBase();
var connectionStringBuilder = new SqliteConnectionStringBuilder();
connectionStringBuilder.DataSource = "database/db.db";

// Utworzenie połączenia z bazą danych
using var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
connection.Open();

Console.WriteLine();


// Zadanie 1: Odczyt danych z pliku CSV
Console.WriteLine("Zad1");
var (data, header) = db.readCSV("input/input1.csv", ',');
Console.WriteLine();

// Zadanie 2: Określenie typów kolumn
Console.WriteLine("Zad2");
var columns = db.ColumnTypes(data, header);
Console.WriteLine();

// Zadanie 3: Utwórz tabelę w bazie danych na podstawie typów kolumn.
Console.WriteLine("Zad3");
db.createTable(columns, "x", connection);
Console.WriteLine();

// Zadanie 4: Wstaw dane do utworzonej tabeli.
Console.WriteLine("Zad4");
db.InsertData(data, header, "x", connection);
Console.WriteLine();

// Zadanie 5: Wydrukuj zawartość tabeli.
Console.WriteLine("Zad5");
db.printTable("x", connection);
Console.WriteLine();

// Zamknij połączenie z bazą danych.
connection.Close();
