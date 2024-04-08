//zad6 stworz klase rachunekt bankowy  
//zad7
//zad10
using System;
using System.Collections.Generic;
using System.Text;


public class RachunekBankowy
{
    private string numer; //zad6 która bedzie miała pola numer stanRachunku czyDozwolonyDebet
    private decimal stanRachunku;
    private bool czyDozwolonyDebet; //zad6 pola mają być prywatne
    private List<PosiadaczRachunku> posiadaczeRachunku = new List<PosiadaczRachunku>(); //zad6 pole lista PosiadaczeRachunku
    private List<Transakcja> transakcje = new List<Transakcja>();// zad10 do klasy rachunek bankowy dodaj liste transakcji

    //zad7 Konstruktor klasy RachunekBankowy pobiera wartość pól tej klasy i 
    public RachunekBankowy(string numer, decimal stanRachunku, bool czyDozwolonyDebet, List<PosiadaczRachunku> posiadacze)
    {
        this.numer = numer;
        this.stanRachunku = stanRachunku;
        this.czyDozwolonyDebet = czyDozwolonyDebet;

        //zad7 Sprawdzenie, czy lista posiadaczy rachunku zawiera co najmniej jedną pozycję
        if (posiadacze == null || posiadacze.Count == 0)
        {
            throw new ArgumentException("Lista posiadaczy rachunku musi zawierać co najmniej jedną pozycję.");
        }

        this.posiadaczeRachunku = new List<PosiadaczRachunku>();
        //this.transakcje = new List<Transakcja>();
        
    }

    //zad6 dostęp do pół przez publiczne properties 
    public string Numer
    {
        get { return numer; }
        set { numer = value; }
    }

    public decimal StanRachunku
    {
        get { return stanRachunku; }
        set { stanRachunku = value; }
    }

    public bool CzyDozwolonyDebet
    {
        get { return czyDozwolonyDebet; }
        set { czyDozwolonyDebet = value; }
    }

    public List<PosiadaczRachunku> PosiadaczeRachunku
    {
        get { return posiadaczeRachunku; }
    }

    //zad6 Metoda dodająca posiadacza rachunku
    public void DodajPosiadacza(PosiadaczRachunku posiadacz)
    {
        posiadaczeRachunku.Add(posiadacz);
    }

    //zad6 Metoda usuwająca posiadacza rachunku
    public void UsunPosiadacza(PosiadaczRachunku posiadacz)
    {
        posiadaczeRachunku.Remove(posiadacz);
    }

    //zad6 Metoda zwracająca liczbę elementów na liście posiadaczy
    public int LiczbaPosiadaczy()
    {
        return posiadaczeRachunku.Count;
    }

    //zad6 Metoda sprawdzająca, czy lista posiadaczy jest pusta
    public bool ListaPusta()
    {
        return posiadaczeRachunku.Count == 0;
    }
    public List<Transakcja> Transakcje
    {
        get { return transakcje; }
    }

    public void DodajTransakcje(Transakcja transakcja)
    {
        transakcje.Add(transakcja);
    }

    public bool UsunTransakcje(Transakcja transakcja)
    {
        return transakcje.Remove(transakcja);
    }

    //zadD w klasie RachunekBankowy dodaj możliwość dodawania posiadaczy rachunku przy pomocy przeciążania operatora +
    public static RachunekBankowy operator +(RachunekBankowy rachunek, PosiadaczRachunku posiadacz)
    {
        if (posiadacz == null || rachunek.posiadaczeRachunku.Contains(posiadacz)) //zad Posiadacz już jest na liście rachunku posaidacza nie ma jeszcze na liście posaidaczy rachunku 
        {
            throw new ArgumentException("Nieprawidłowy posiadacz rachunku."); //zadD metoda ma rzucać wyjątek
        }

        rachunek.posiadaczeRachunku.Add(posiadacz);
        return rachunek;
    }

    public static RachunekBankowy operator -(RachunekBankowy rachunek, PosiadaczRachunku posiadacz)
    {
        if (rachunek.posiadaczeRachunku.Count - 1 < 1 || posiadacz == null || !rachunek.posiadaczeRachunku.Contains(posiadacz)) //zadD jeżeli liczba posiadaczy spadłaby poniżej 1
        {
            throw new ArgumentException("Nie można usunąć posiadacza rachunku."); //zadD metoda ma rzucać wyjątek
        }

        rachunek.posiadaczeRachunku.Remove(posiadacz);
        return rachunek;
    }

    //zad10 do klasy Rachunek banowy dodaj publiczną, statyczną metode DokonajTransakcji
    public static void DokonajTransakcji(RachunekBankowy rachunekZrodlowy, RachunekBankowy rachunekDocelowy, decimal kwota, string opis) 
    {
        if (kwota < 0 || (rachunekZrodlowy == null && rachunekDocelowy == null) || 
            (rachunekZrodlowy != null && !rachunekZrodlowy.CzyDozwolonyDebet && rachunekZrodlowy.StanRachunku < kwota)) //zad10 jesli kwota jest ujemna | oba rachunki są równe null | ...
        {
            throw new ArgumentException("Nieprawidłowe parametry transakcji."); //to metoda ma rzucić wyjątek
        }

        if (rachunekZrodlowy == null)  //zad10 Jesli rachunkŹródłowy jest róny null
        {
            //zad10 Wpłata gotówkowa 
            rachunekDocelowy.StanRachunku += kwota; //zad10 do StanuRachunku dodajemy kwote transakcji 
            Transakcja transakcja = new Transakcja(null, rachunekDocelowy, kwota, opis); //zad10 tworzymy nowy obiekt klasy Tansakcja
            rachunekDocelowy.DodajTransakcje(transakcja); //zad10 tak stworzony obiekt przekazumemy do listy transakcja
        }
        else if (rachunekDocelowy == null)
        {
            //zad10 Wypłata gotówkowa 
            rachunekZrodlowy.StanRachunku -= kwota;
            Transakcja transakcja = new Transakcja(rachunekZrodlowy, null, kwota, opis);
            rachunekZrodlowy.DodajTransakcje(transakcja);
        }
        else
        {
            //zad10 przelew
            rachunekZrodlowy.StanRachunku -= kwota;
            rachunekDocelowy.StanRachunku += kwota;
            Transakcja transakcja = new Transakcja(rachunekZrodlowy, rachunekDocelowy, kwota, opis);
            rachunekZrodlowy.DodajTransakcje(transakcja);
            rachunekDocelowy.DodajTransakcje(transakcja);
        }
        

    }

    //zadD
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Numer rachunku: {Numer}");
        sb.AppendLine($"Stan rachunku: {StanRachunku}");
        sb.AppendLine("Posiadacze rachunku:");
        foreach (var posiadacz in PosiadaczeRachunku)
        {
            sb.AppendLine(posiadacz.ToString());
        }
        sb.AppendLine("Transakcje:");
        foreach (var transakcja in transakcje)
        {
            sb.AppendLine(transakcja.ToString());
        }
        return sb.ToString();
    }


    
    
    
}
