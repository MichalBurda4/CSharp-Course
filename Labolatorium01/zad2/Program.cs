//zad2
using System;
using System.IO;

class Program
{
    static void Main()
    {
        string filePath = "tekst.txt";

        Console.WriteLine("Podaj ciąg znaków: ");
        Console.WriteLine("Aby zakończyć, działąnie programu napisz: 'koniec!'.");

        string lastString = null;

        using (StreamWriter sw = new StreamWriter(filePath, append: true))
        {
            string input;
            do
            {
                Console.Write("Wprowadź napis: ");
                input = Console.ReadLine();

                if (input.ToLower() == "koniec!")
                    break;

                sw.WriteLine(input);
                lastString = input;

            } while (true);
        }

        if (lastString != null)
        {
            Console.WriteLine($"Ostatni napis w kolejności leksykalnograficznej to : {SortString(lastString)}");
        }
        else
        {
            Console.WriteLine("Nie wprowadzono żadnych napisów.");
        }
    }

    static string SortString(string input)
    {
        char[] chars = input.ToCharArray();
        Array.Sort(chars);
        return new string(chars);
    }
}
