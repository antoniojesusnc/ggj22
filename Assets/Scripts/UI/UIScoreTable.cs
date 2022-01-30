using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScoreTable : MonoBehaviour
{

    public UILeaderboardRow rowPrefab;
    public Transform parent;

    void Start()
    {
        var leader = LeaderboardService.Instance.GetLeaderboardScores();
        
        for (int i = 0; i < leader.Count; i++)
        {
            var row = GetRow();
            var rank = i.ToString();

            row.SetData(rank, leader[i].username, leader[i].score.ToString());
        }
    }

    private UILeaderboardRow GetRow()
    {
        return Instantiate(rowPrefab, parent);
    }
}
