Celem laboratorium jest zapoznanie z zastosowaniem LINQ do obsługi danych w kolekcjach. W celu realizacji laboratorium należy wczytać pliki zawierające dane zakodowane w formacie CSV a następnie dokonać na nich szeregu operacji.

[3 punkty] odwzoruj rekordy danych z plików regions.csv, territories.csv, employee_territories.csv, employees.csv przy pomocy odpowiednich klas. Dla uproszczenia uznaj, że każde pole jest typu String. Wczytaj wszystkie dane do czterech kolekcji typu List zawierających obiekty tych klas.

[1 punkt] wybierz nazwiska wszystkich pracowników.

[1 punkt] wypisz nazwiska pracowników oraz dla każdego z nich nazwę regionu i terytorium gdzie pracuje. Rezultatem kwerendy LINQ będzie "płaska" lista, więc nazwiska mogą się powtarzać (ale każdy rekord będzie unikalny).

[1 punkt] wypisz nazwy regionów oraz nazwiska pracowników, którzy pracują w tych regionach, pracownicy mają być zagregowani po regionach, rezultatem ma być lista regionów z podlistą pracowników (odpowiednik groupjoin).

[1 punkt] wypisz nazwy regionów oraz liczbę pracowników w tych regionach.

[3 punkty] wczytaj do odpowiednich struktur dane z plików orders.csv oraz orders_details.csv. Następnie dla każdego pracownika wypisz liczbę dokonanych przez niego zamówień, średnią wartość zamówienia oraz maksymalną wartość zamówienia.