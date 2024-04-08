//zad4 Stworz klase OsobaPrawna  
//zad5
using System;

public class OsobaPrawna : PosiadaczRachunku //zad4 która dziedziczy po klasie PosiadaczRachunku
{
    private string nazwa; //zad4 klasa ma zawierać prywatne pola nazwa siedziba
    private string siedziba;

    //zad5 konstruktor klasy który pobiera nazwe i nazwesiedziby
    public OsobaPrawna(string nazwa, string siedziba)
    {
        this.nazwa = nazwa;
        this.siedziba = siedziba;
    }

    //zad4 dostep do pól ma być poprzez własnośc publiczne properties
    public string Nazwa
    {
        get { return nazwa; } //wlasnosci maja pozwalac jedynie na odczyt 
    }

    public string Siedziba
    {
        get { return siedziba; }
    }

    public override string ToString() //zad4 metoda ToString() zwraca napis że jest to osoba prawna nazwe i nazwe siedziby
    {
        return "Osoba prawna: " + Nazwa + ", siedziba: " + Siedziba;
    }
}

