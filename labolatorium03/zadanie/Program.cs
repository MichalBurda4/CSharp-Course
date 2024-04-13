using Newtonsoft.Json;
using System.Security.Principal;
using System.Xml.Serialization;

public class Program{
    public static void Main(){
        //1. Wczytaj dane z pliku favorite-tweets.jsonl
        String file = File.ReadAllText("data.json");
        Resources ?resources = JsonConvert.DeserializeObject<Resources>(file);
        var path = "favourite-tweets.xml";
        File.WriteAllText(path, "");
        
        //2. Wczytanie do formatu xml
        xmlWriterReader(resources);
        

        Console.WriteLine(); 
        Console.WriteLine();
        
        
    
        //3. Sortowanie po:
        resources.SortUsername(); //3. Sortowanie po USERNAME
        resources.SortDate(); //3. Sortowanie po DATE

        
        //4. Wypisz najnowszy i najstarszy tweet
        Console.WriteLine("Najnowszy i Najstarszy tweet:");
        var newestTweet = resources.Data.Last();
        var oldestTweet = resources.Data.First();
        Console.WriteLine("Najnowszy: " + newestTweet.Text);
        Console.WriteLine("Najstarszy: " + oldestTweet.Text);
        // Console.WriteLine("Najnowszy: " + newestTweet.Text +" Data: "+resources.Data.Last().Text);
        // Console.WriteLine("Najstarszy: "+ oldestTweet.Text +" Data: "+resources.Data.First().Text);

        
        //5. Stwórz słownik, który będzie indeksowany po username i będzie przechowywał jako listę tweety użytkownika o danym username.
        TweetsDict tweetsDict = new TweetsDict(resources);


        Console.WriteLine(); 
        Console.WriteLine();

        //6. Oblicz częstość występowania słów, które w treści tweetów (w polu TEXT).
        //7. Znajdź i wypisz 10 najczęściej występujących w tweetach wyrazów o długości co najmniej 5 liter.
        Console.WriteLine("Znajdź i wypisz 10 najczęściej występujących w tweetach wyrazów o długości co najmniej 5 liter");
        resources.WordsFrequency(true);


        Console.WriteLine();
        Console.WriteLine(); 


    
        /* ***************************************************************************** ZAD8 ***********************************************************************************
    ************************************************************************************************************************************************************************ */
        Console.WriteLine("10 słów z najwyższymi wartościami IDF");
        var idfs = resources.Idf();
        // Posortuj słowa według wartości IDF malejąco.
        var sortedIdfs = idfs.OrderByDescending(pair => pair.Value);
        // Wyświetl 10 słów z najwyższymi wartościami IDF.
        
        int i = 0;
        foreach (var pair in sortedIdfs)
        {
            if (i >= 10)
            {
                break;
            }
            Console.WriteLine($"Wyraz {i+1}: {pair.Key}  IDF: {pair.Value}");
            i++;
        }
    }

    static void xmlWriterReader(Resources ?resources = null, bool readFromFile = false, int index = -1, string path = "favourite-tweets.xml"){
        XmlSerializer x = new XmlSerializer(typeof(Resources));
        if(readFromFile){
            using(StreamReader reader = new StreamReader(path)){
                Resources tweetsRead = (Resources)x.Deserialize(reader);
                if(index >= 0 && index < tweetsRead.Data.Count){
                    Console.WriteLine(tweetsRead.Data[index].ToString());
                }
                else{
                    Console.WriteLine(tweetsRead.ToString());
                }
            }
        }
        else{
            using(StreamWriter writer = File.CreateText(path)){
                x.Serialize(writer, resources);
            }
        }

        Console.WriteLine();
        
    }


    
    
}