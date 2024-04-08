using System;


class Program
{
    static void Main(string[] args)
    {
        // Tworzenie instancji OsobaFizyczna
        OsobaFizyczna osoba1 = new OsobaFizyczna("Jan", "Kowalski", "Michal", "12345678901", "ABC123456");
        //OsobaFizyczna osoba3 = new OsobaFizyczna("Anna", "Nowak", "Maja", "98765432111", "2345");
        
        // Wyświetlanie informacji o OsobaFizyczna
        Console.WriteLine("Informacje o osobie fizycznej:");
        Console.WriteLine(osoba1);

        // Tworzenie instancji OsobaPrawna
        OsobaPrawna osoba2 = new OsobaPrawna("Firma XYZ", "ul. Przykładowa 1");
        
        // Wyświetlanie informacji o OsobaPrawna
        Console.WriteLine("\nInformacje o osobie prawnej:");
        Console.WriteLine(osoba2);

        List<PosiadaczRachunku> posiadacze = new List<PosiadaczRachunku>();
        
        posiadacze.Add(osoba1);
        //posiadacze.Add(osoba3);

        //Rachunek bankowy
        RachunekBankowy rachunek1 = new RachunekBankowy("123456789", 1000, true, posiadacze);
        RachunekBankowy rachunek2 = new RachunekBankowy("987654321", 500, false, posiadacze);
        rachunek1 += osoba1;
        //rachunek2 += osoba3;
        Transakcja transakcja1 = new Transakcja(rachunek1, rachunek2, 200, "Przelew");


        // Wyświetlanie informacji o rachunkach przed transakcjami
        Console.WriteLine("\nInformacje o rachunkach przed transakcjami:");
        Console.WriteLine(rachunek1);
    


        
        //WYJĄTKI
        
        //ZLA TRANSAKCJA
        //var transakcja2 = new Transakcja(null, null, 100, "Przelew");
        

        //ZŁY PESEL
        // Wyświetlanie informacji o OsobaFizyczna
        OsobaFizyczna osoba3 = new OsobaFizyczna("Michal", "Burda", "Kuba", "123", "ABC123458");
        Console.WriteLine("Informacje o osobie fizycznej:");
        Console.WriteLine(osoba2);

        // Próba wykonania transakcji
        // try
        // {
        //     // Dokonanie transakcji
        //     RachunekBankowy.DokonajTransakcji(rachunek1, rachunek2, -3000, "Przelew");
        // }
        // catch (ArgumentException ex)
        // {
        //     Console.WriteLine("Błąd transakcji: " + ex.Message);
        // }

        

    }
}
