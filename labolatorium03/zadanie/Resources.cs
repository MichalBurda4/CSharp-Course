using System;
using System.Collections.Generic;
using System.Linq;

public class Resources
{
    public List<Tweet> Data { get; set; } //Lista zawierająca Tweety
    
    public Resources()
    {
        Data = new List<Tweet>();
    }

    
    /* ***************************************************************************** ZAD3 ***********************************************************************************
    ************************************************************************************************************************************************************************ */

    //3. Funkcja sortująca po USERNAME
    public void SortUsername()
    {
        Data.Sort((tweet1, tweet2) => string.Compare(tweet1.UserName, tweet2.UserName));
    }

    

    //3. Funkcja sortująca po DATE
    public void SortDate()
    {
        Data.Sort((tweet1, tweet2) => DateTime.Compare(
            DateTime.ParseExact(tweet1.CreatedAt, "MMMM dd, yyyy 'at' hh:mmtt", System.Globalization.CultureInfo.InvariantCulture),
            DateTime.ParseExact(tweet2.CreatedAt, "MMMM dd, yyyy 'at' hh:mmtt", System.Globalization.CultureInfo.InvariantCulture)
        ));
    }


    
    
    
    /* ***************************************************************************** ZAD6 ***********************************************************************************
    ************************************************************************************************************************************************************************ */
    //6. Częstotliwośc występowania słów
    public void WordsFrequency(bool tenMostFrequent = false)
    {
        var wordsFrequencyDict = new Dictionary<string, int>();

        foreach (var t in Data)
        {
            foreach (var word in t.Text.Split(' '))
            {
                if (!wordsFrequencyDict.ContainsKey(word))
                    wordsFrequencyDict[word] = 1;
                else
                    wordsFrequencyDict[word]++;
            }
        }

        /* ***************************************************************************** ZAD7 ***********************************************************************************
        ************************************************************************************************************************************************************************ */
        //7. Znajdź i wypisz 10 najczęściej występujących w tweetach wyrazów o długości co najmniej 5 liter.
        var sortedDict = tenMostFrequent ?
        wordsFrequencyDict.OrderByDescending(pair => pair.Value).Where(pair => pair.Key.Length >= 5).Take(10) :
        wordsFrequencyDict;

        int i = 1;
        foreach (var pair in sortedDict)
        {
            Console.WriteLine($"Wyraz {i}: {pair.Key}");
            i++;
        }

        

    }
    
    /* ***************************************************************************** ZAD8 ***********************************************************************************
    ************************************************************************************************************************************************************************ */

    public Dictionary<string, double> Idf()
    {
        int tweetCount = Data.Count; 
        var wordsFrequencyDict = new Dictionary<string, int>();

        foreach (var t in Data)
        {
            foreach (var word in t.Text.Split(' ').Distinct())
            {
                if (!wordsFrequencyDict.ContainsKey(word))
                    wordsFrequencyDict[word] = 1;
                else
                    wordsFrequencyDict[word]++;
            }
        }

        return wordsFrequencyDict.ToDictionary(pair => pair.Key, pair => Math.Log(tweetCount / (double)pair.Value));
    }


    
    

    
    
}
