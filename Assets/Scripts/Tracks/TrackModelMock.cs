using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackModelMock :  ITrackModel
{
    public List<int> Tracks01 { get; } 
    public List<int> Tracks02 { get; }
    public TracksConfig Config { get; }

    public TrackModelMock(TracksConfig tracksConfig, float handicap)
    {
        Config = tracksConfig;
        
        Tracks01 = new List<int>() {1, 15, 30};
        Tracks02 = new List<int>() {5, 20, 50};
    }
}
