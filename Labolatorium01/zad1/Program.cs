//Program dowolny
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Podaj pierwszą liczbę:");
        string liczba1 = Console.ReadLine();

        Console.WriteLine("Podaj drugą liczbę:");
        string liczba2 = Console.ReadLine();

        double num1, num2;

        if (!double.TryParse(liczba1, out num1) || !double.TryParse(liczba2, out num2))
        {
            Console.WriteLine("Podane wartości nie są liczbami.");
            return;
        }

        double sum = num1 + num2;
        Console.WriteLine($"Wynik dodawania: {sum}");
    }
}

