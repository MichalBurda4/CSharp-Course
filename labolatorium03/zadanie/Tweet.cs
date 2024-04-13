public class Tweet
{
    public string Text { get; set; }
    public string UserName { get; set; }
    public string LinkToTweet { get; set; }
    public string FirstLinkUrl { get; set; }
    public string CreatedAt { get; set; }
    public string TweetEmbedCode { get; set; }

    public Tweet(string text, string userName, string linkToTweet, string firstLinkUrl, string createdAt, string tweetEmbedCode)
    {
        Text = text;
        UserName = userName;
        LinkToTweet = linkToTweet;
        FirstLinkUrl = firstLinkUrl;
        CreatedAt = createdAt;
        TweetEmbedCode = tweetEmbedCode;
    }

    
    public Tweet()
    {
    }

    public override string ToString()
    {
        return $"Treść tweeta: {Text}\nNazwa użytkownika: {UserName}\nLink do tweeta: {LinkToTweet}\nPierwszy link URL: {FirstLinkUrl}\nUtworzono: {CreatedAt}\nKod osadzenia tweeta: {TweetEmbedCode}";
    }

}
