using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackModel : ITrackModel
{
    public List<int> Tracks01 { get; private set; }
    public List<int> Tracks02 { get; private set; }
    public TracksConfig Config { get; private set; }

    public TrackModel(TracksConfig tracksConfig, float handicap)
    {
        Tracks01 = new List<int>();
        Tracks02 = new List<int>();

        Config = tracksConfig;
    }
}
