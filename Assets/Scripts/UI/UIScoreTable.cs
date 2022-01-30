using System.Collections.Generic;
using UnityEngine;

public class UIScoreTable : MonoBehaviour
{
    public const string FORMAT = "{0} -";
    private const string TEMPORARY_NAME = "UNKNOWN";
    private const string NO_ENTRY_NAME = "NO-ENTRY";

    public UILeaderboardRow rowPrefab;
    public Transform parent;
    private UILeaderboardRow newRecord;

    void Start()
    {
        int distance = Mathf.RoundToInt(GameService.Instance.Distance);

        List<LeaderboardScore> leader = LeaderboardService.Instance.GetLeaderboardScores();
        List<UILeaderboardRow> rows = new List<UILeaderboardRow>();

        bool isNewRecord = false;

        for (int i = 0; i < leader.Count && rows.Count < leader.Count; i++)
        {
            var rank = string.Format(FORMAT, (i + 1).ToString("00"));
            var playerName = leader[i].username;
            var score = leader[i].score;

            if (distance > score && !isNewRecord)
            {
                isNewRecord = true;
                newRecord = GetRow();
                newRecord.NewData(rank, distance.ToString());
                rows.Add(newRecord);
                continue;
            }

            if (score == 0)
            {
                playerName = NO_ENTRY_NAME;
            }
            var newRow = GetRow();
            newRow.SetData(rank, playerName, score.ToString());
            rows.Add(newRow);
        }
    }

    private UILeaderboardRow GetRow()
    {
        return Instantiate(rowPrefab, parent);
    }

    public void SaveNewRecord()
    {
        if (newRecord != null)
        {
            int score = int.Parse(newRecord._score.text);
            string playerName = newRecord.newPlayer.text;

            if (playerName == null || playerName.Trim().Length == 0)
            {
                playerName = TEMPORARY_NAME;
            }

            LeaderboardService.Instance.AddScore(playerName, score);
        }
    }
}
