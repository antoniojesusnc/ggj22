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
        Config = tracksConfig;
        Tracks01 = new List<int>();
        Tracks02 = new List<int>();

        List<int> tracks = generateTrack(Config.size, Config.segments);

        foreach (var item in tracks)
        {
            if (Random.Range(0, 2) == 0)
            {
                Tracks01.Add(item);
            }
            else
            {
                Tracks02.Add(item);
            }
        }
    }


    private List<int> generateTrack(int trackSize, int segments)
    {
        int segmentSize = trackSize / segments;
        Debug.Log("segmentSize: " + segmentSize);

        int maxBlockPerSegment = 20;
        List<int> track = new List<int>();

        for (int i = 0; i < segments; i++)
        {
            int initValue = (i * segmentSize) + 1;
            track.AddRange(generateTrackSegment(maxBlockPerSegment, segmentSize, initValue));
        }

        track.Sort();

        return track;
    }

    private List<int> generateTrackSegment(int maxBlockPerSegment, int segmentSize, int initValue)
    {
        List<int> segmentTrack = new List<int>();

        for (int i = 0; i < maxBlockPerSegment; i++)
        {
            int randPosition = Random.Range(initValue, segmentSize);
            if (!segmentTrack.Contains(randPosition))
            {
                segmentTrack.Add(randPosition);
            }
        }

        return segmentTrack;
    }
}
