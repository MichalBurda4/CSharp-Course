// zad3
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Podaj argumenty: nazwa pliku | ciąg znaków");
            return;
        }

        string fileName = args[0];
        string szukanyString = args[1];

        if (!File.Exists(fileName))
        {
            Console.WriteLine("Podany plik nie istnieje");
            return;
        }

        Console.WriteLine("Szukanie ciąg znaków: ");

        using (StreamReader sr = new StreamReader(fileName))
        {
            int lineNumber = 0;
            while (!sr.EndOfStream)
            {
                lineNumber++;
                string line = sr.ReadLine();
                int index = line.IndexOf(szukanyString);
                while (index != -1)
                {
                    Console.WriteLine($"Znaleziono w linijce: {lineNumber}, pozycja: {index}");
                    index = line.IndexOf(szukanyString, index + 1);
                }
            }
        }
    }
}


