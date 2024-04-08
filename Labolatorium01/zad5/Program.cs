//zad5
using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0) //jesli nie podano nazwy pliku (ścieżki do pliku)
        {
            Console.WriteLine("Podaj ścieżkę do pliku jako argument.");
            return;
        }

        string fileName = args[0];

        if (!File.Exists(fileName))
        {
            Console.WriteLine($"Nie odnaleziono pliku o nazwie: {fileName}"); //jesli podany plik nie istniej
            return;
        }

        // Otwórz plik i wczytaj wszystkie linie
        string[] lines = File.ReadAllLines(fileName);

        // Liczba linii w pliku
        int liczbalini = lines.Length;
        Console.WriteLine($"Liczba linii w pliku: {liczbalini}");

        // Liczba znaków w pliku
        int liczbaznakow = lines.Sum(line => line.Length);
        Console.WriteLine($"Liczba znaków w pliku: {liczbaznakow}");

        // Największa liczba w pliku
        double max = lines.SelectMany(line => line.Split(' ')) 
                                .Where(str => double.TryParse(str, out _)) 
                                .Select(double.Parse) 
                                .DefaultIfEmpty(double.MinValue) 
                                .Max(); 
        Console.WriteLine($"Największa liczba w pliku: {max}");

        // Najmniejsza liczba w pliku
        double min = lines.SelectMany(line => line.Split(' ')) 
                                .Where(str => double.TryParse(str, out _)) 
                                .Select(double.Parse) 
                                .DefaultIfEmpty(double.MaxValue) 
                                .Min(); 
        Console.WriteLine($"Najmniejsza liczba w pliku: {min}");

        // Średnia liczba w pliku
        double srednia = lines.SelectMany(line => line.Split(' ')) 
                                    .Where(str => double.TryParse(str, out _)) 
                                    .Select(double.Parse) 
                                    .DefaultIfEmpty(0) 
                                    .Average(); 
        Console.WriteLine($"Średnia liczba w pliku: {srednia}");
    }
}

