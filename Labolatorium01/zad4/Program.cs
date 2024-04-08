// zad4
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 5) // sprawdza czy została podana odpowiednia ilosc arumentów (czyli 5 argumentów )
        {
            Console.WriteLine("Podaj parametry: nazwa pliku | ilosc liczb | przedział wartosci np 1 100 | seed | calkowite = true niecalkowie = false ");
            return;
        }

        string fileName = args[0]; //nazwa pliku
        int n = int.Parse(args[1]); //ilosc liczb
        int zakresl = int.Parse(args[2]); //zakres min
        int zakresp = int.Parse(args[3]); //zakres max
        int seed = int.Parse(args[4]); //ziarno 
        bool rl = bool.Parse(args[5]); //calkowite = true rzeczywiste = false 

        using (StreamWriter sw = new StreamWriter(fileName))
        {
            Random random = new Random(seed);

            for (int i = 0; i < n; i++)
            {
                double randomNumber;
                if (rl)
                {
                    randomNumber = random.Next(zakresl, zakresp + 1);
                }
                else
                {
                    randomNumber = zakresl + (random.NextDouble() * (zakresp - zakresl));
                }

                sw.WriteLine(randomNumber);
            }
        }

        Console.WriteLine("Liczby zostaly zapisane");
    }
}

