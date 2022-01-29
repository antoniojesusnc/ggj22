using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackModelMock :  ITrackModel
{
    public List<int> Tracks01 { get; } = new List<int>();
    public List<int> Tracks02 { get; } = new List<int>();
    public List<int> Rewards01 { get; } = new List<int>();
    public List<int> Rewards02 { get; } = new List<int>();
    public TracksConfig Config { get; }

    public TrackModelMock(TracksConfig tracksConfig, float handicap)
    {
        Config = tracksConfig;

        var blocks = Mathf.CeilToInt(Time.realtimeSinceStartup / 10);
        float step = tracksConfig.size / (blocks + 1);
        float acumStep = 0;
        for (int i = 0; i < blocks; i++)
        {
            acumStep += step;
            Tracks01.Add(Mathf.FloorToInt(acumStep));
            Tracks02.Add(Mathf.FloorToInt(acumStep+step*0.5f));
        }
    }
}
