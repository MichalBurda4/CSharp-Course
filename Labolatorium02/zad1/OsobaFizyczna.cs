//zad2 Stworz klase OsobaFizyczna 
//zad3
//zadD

public class OsobaFizyczna : PosiadaczRachunku //zad2 która dziedziczy po klasei PosiadaczRachunku 
{
    private string imie; //zad2 klasa ma zawierać pola: imie nazwisko drugieImie pesel numerPaszportu
    private string nazwisko; //zad2 mają być to pola prywantne
    private string drugieImie;
    private string pesel;
    private string numerPaszportu;

    //zad3 konstruktor klasy OsobaFizyczna który pobiera wartości do wszystkich pól tej klasy
    public OsobaFizyczna(string imie, string nazwisko, string drugieImie, string pesel, string numerPaszportu)
    {   
        //zad3 w ciele konstruktora przypisujemy wartości parametrów do odpowiadających im pól klasy
        this.imie = imie;
        this.nazwisko = nazwisko;
        this.drugieImie = drugieImie;

        if (pesel == null && numerPaszportu == null) //zad3 jeśli pesel i numer paszportu wynoszą null ma zostać rzucony wyjątek 
        {
            throw new Exception("PESEL albo numer paszportu muszą być nie null"); //zad3 przykładowy wyjątek
        }

        if (string.IsNullOrEmpty(pesel) || pesel.Length != 11) //zadD pesel ma mieć 11 cyfr i aby nie był nullem
        {
            throw new ArgumentException("PESEL musi składać się z 11 cyfr.");
        }

        this.pesel = pesel;
        this.numerPaszportu = numerPaszportu;

    }

    //zad2 dostęp do pól ma się odbywać poprzez publiczne właściwości properties
    public string Imie
    {
        get { return imie; } //zwraca wartosc pola imie
        set { imie = value; } //przypisanie imienia do pola imie
    }

    public string Nazwisko
    {
        get { return nazwisko; }
        set { nazwisko = value; }
    }

    public string DrugieImie
    {
        get { return drugieImie; }
        set { drugieImie = value; }
    }

    public string PESEL
    {
        get { return pesel; } //zadD
        set
        {
            if (string.IsNullOrEmpty(value) || value.Length != 11)
            {
                throw new ArgumentException("PESEL musi składać się z 11 cyfr.");
            }
            pesel = value;
        }
    }

    public string NumerPaszportu
    {
        get { return numerPaszportu; }
        set { numerPaszportu = value; }
    }
    //zad2 Metoda ToString ma zwracać że jest to osoba Fizyczna oraz imie i nazwisko
    public override string ToString()
    {
        return "Osoba fizyczna: " + Imie + " " + Nazwisko;
    }

    

}