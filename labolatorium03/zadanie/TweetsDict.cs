//zad5 Słownik
public class TweetsDict
{
    public Dictionary<string, List<Tweet>> TweetsDictionary { get; private set; }

    public TweetsDict(Resources tweets)
    {
        TweetsDictionary = new Dictionary<string, List<Tweet>>();

        foreach (var tweet in tweets.Data)
        {
            // Sprawdź, czy użytkownik istnieje już w słowniku.
            if (!TweetsDictionary.ContainsKey(tweet.UserName))
            {
                // Jeśli użytkownik nie istnieje, utwórz nową listę tweetów i dodaj do słownika.
                TweetsDictionary[tweet.UserName] = new List<Tweet>();
            }

            // Dodaj tweet do listy tweetów danego użytkownika.
            TweetsDictionary[tweet.UserName].Add(tweet);
        }
    }
}
