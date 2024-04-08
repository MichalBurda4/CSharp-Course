using System;
//zad8
//zad9
public class Transakcja //zad7 stworz klase Transakcja 
{
    private RachunekBankowy rachunekZrodlowy; //zad 7 klasa posaida prywatne pola 
    private RachunekBankowy rachunekDocelowy;
    private decimal kwota;
    private string opis;

    //zad9 konstruktor klasy transakcja ktory pobiera wartosci do wszystkich pol tej klasy 
    public Transakcja(RachunekBankowy rachunekZrodlowy, RachunekBankowy rachunekDocelowy, decimal kwota, string opis)
    {
        //zad9 konstruktor musi sprawdzic czy rachunekzdrodlowy i docelowy mają wartośc null jesli tak to musi rzucic wyjątek
        if (rachunekZrodlowy == null || rachunekDocelowy == null)
        {
            throw new ArgumentNullException("Rachunek źródłowy i docelowy nie mogą być null.");
        }

        this.rachunekZrodlowy = rachunekZrodlowy;
        this.rachunekDocelowy = rachunekDocelowy;
        this.kwota = kwota;
        this.opis = opis;
    }

    //zad 8 dostep do pol przez publiczne properties 
    public RachunekBankowy RachunekZrodlowy
    {
        get { return rachunekZrodlowy; }
        set { rachunekZrodlowy = value; }
    }

    public RachunekBankowy RachunekDocelowy
    {
        get { return rachunekDocelowy; }
        set { rachunekDocelowy = value; }
    }

    public decimal Kwota
    {
        get { return kwota; }
        set { kwota = value; }
    }

    public string Opis
    {
        get { return opis; }
        set { opis = value; }
    }

    //zadD W klasie Transakcja dodaj przeciazenie metody ToString która zwróci napis z numerem rachunku źrógłowego, docelowego kwotą i opisem
    public override string ToString()
    {
        string numerZ = rachunekZrodlowy != null ? rachunekZrodlowy.Numer : "Brak";
        string numerD = rachunekDocelowy != null ? rachunekDocelowy.Numer : "Brak";
        return $"Transakcja: Rachunek źródłowy: {numerZ}, Rachunek docelowy: {numerD}, Kwota: {kwota}, Opis: {opis}";
    }
}

