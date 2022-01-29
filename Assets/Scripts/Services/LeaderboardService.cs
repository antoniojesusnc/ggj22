using System.Collections.Generic;
using UnityEngine;

public class LeaderboardService : MonoBehaviorSingleton<LeaderboardService>
{
    public const int EntryCount = 10;
    private const string PlayerPrefsBaseKey = "leaderboard";

    public string debugName;
    public int debugScore;

    [ContextMenu("addScore")]
    public void AddScoreDebug()
    {
        AddScore(debugName, debugScore);
    }

    [ContextMenu("getScore")]
    public void getScoreDebug()
    {
        string log = "";
        foreach (var item in GetLeaderboardScores())
        {
            log += "Name: " + item.username + ", Score: " + item.score + "\n";
        }
        Debug.Log(log);
    }

    public List<LeaderboardScore> GetLeaderboardScores()
    {
        List<LeaderboardScore> leaderboardScores = new List<LeaderboardScore>();
        for (int i = 0; i < EntryCount; ++i)
        {
            leaderboardScores.Add(Entries[i]);
        }
        return leaderboardScores;
    }

    public void AddScore(string username, int score)
    {
        Entries.Add(new LeaderboardScore(username, score));
        SortScores();
        Entries.RemoveAt(Entries.Count - 1);
        SaveScores();
    }

    private List<LeaderboardScore> _entries;

    private List<LeaderboardScore> Entries
    {
        get
        {
            if (_entries == null)
            {
                _entries = new List<LeaderboardScore>();
                LoadScores();
            }
            return _entries;
        }
    }

    private void SortScores()
    {
        _entries.Sort((a, b) => b.score.CompareTo(a.score));
    }

    private void LoadScores()
    {
        _entries.Clear();

        for (int i = 0; i < EntryCount; ++i)
        {
            LeaderboardScore entry = new LeaderboardScore();
            entry.username = PlayerPrefs.GetString(PlayerPrefsBaseKey + "[" + i + "].username", "");
            entry.score = PlayerPrefs.GetInt(PlayerPrefsBaseKey + "[" + i + "].score", 0);
            _entries.Add(entry);
        }

        SortScores();
    }

    private void SaveScores()
    {
        for (int i = 0; i < EntryCount; ++i)
        {
            var entry = _entries[i];
            PlayerPrefs.SetString(PlayerPrefsBaseKey + "[" + i + "].username", entry.username);
            PlayerPrefs.SetFloat(PlayerPrefsBaseKey + "[" + i + "].score", entry.score);
        }
    }
}
