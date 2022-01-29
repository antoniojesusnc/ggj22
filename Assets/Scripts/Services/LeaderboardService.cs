using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardService : MonoBehaviorSingleton<LeaderboardService>
{


    public string debugName;
    public float debugScore;

    [ContextMenu("addScore")]
    public void AddScoreDebug()
    {
        AddScore(debugName, debugScore);
    }

    private void AddScore(string debugName, float debugScore)
    {
        throw new NotImplementedException();
    }
}
