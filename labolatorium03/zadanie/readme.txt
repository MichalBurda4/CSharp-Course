Celem laboratorium jest zapoznanie z metodami programowania przy uĹźyciu kolekcji w jÄzyku C#. W tym celu naleĹźy wczytaÄ plik zawierajÄcy dane zakodowane w formacie JSON a nastÄpnie dokonaÄ na nich szeregu operacji.

1. [1 punkt] Wczytaj dane z pliku favorite-tweets.jsonl Po wczytaniu poszczegĂłlne tweety powinny znajdowaÄ siÄ w osobnych obiektach a obiekty na liĹcie.  MoĹźesz dowolnie modyfikowaÄ strukturÄ pliku, ale nie modyfikuj danych poszczegĂłlnych tweetĂłw.

2. [1 punkt] Napisz funkcjÄ, ktĂłry pozwoli na przekonwertowanie wczytanych w punkcie 1 danych do formatu XML. Funkcja ma pozwalaÄ zarĂłwno na zapis do pliku w formacie XML danych o tweetach jak i wczytanie tych danych z pliku.    

3. [1 punkt] Napisz funkcjÄ sortujÄce tweety po nazwie uĹźytkownikĂłw jak i funkcjÄ sortujÄcÄ uĹźytkownikĂłw po dacie utworzenie tweetu.

4. [1 punkt] Wypisz najnowszy i najstarszy tweet znaleziony wzglÄdem daty jego utworzenia.

5. [1 punkt] StwĂłrz sĹownik, ktĂłry bÄdzie indeksowany po username i bÄdzie przechowywaĹ jako listÄ tweety uĹźytkownika o danym username.

6. [1 punkt] Oblicz czÄstoĹÄ wystÄpowania sĹĂłw, ktĂłre w treĹci tweetĂłw (w polu TEXT).

7. [2 punkty] ZnajdĹş i wypisz 10 najczÄĹciej wystÄpujÄcych w tweetach wyrazĂłw o dĹugoĹci co najmniej 5 liter.

8. [2 punkty] Policz IDF dla wszystkich sĹĂłw w tweetach zgodnie z definicjÄ podanÄ http://www.tfidf.com/ Posortuj IDF malejÄco i wypisz 10 wyrazĂłw o najwiÄkszej wartoĹci IDF.

Powodzenia!

Klasa TweetsDict ma za zadanie przechowywać tweety w formie słownika, gdzie kluczem jest nazwa użytkownika (UserName), a wartością jest lista tweetów przypisanych do danego użytkownika.

Klasa Tweet reprezentuje pojedynczy tweet na platformie społecznościowej.

