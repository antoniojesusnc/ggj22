public class LeaderboardScore
{
    public string username { get; set; }
    public int score { get; set; }

    public LeaderboardScore()
    {}

    public LeaderboardScore(string username, int score)
    {
        this.username = username;
        this.score = score;
    }
}
