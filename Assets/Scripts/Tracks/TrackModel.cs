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

        List<int> tracks = generateTrack(Config.size, Config.segments, Config.maxBlockPerSegment);

        float factorToPutDown = 0.5f;
        
        foreach (var item in tracks)
        {
            if (Random.value >= factorToPutDown)
            {
                Tracks01.Add(item);
                factorToPutDown = 0.7f;
            }
            else
            {
                Tracks02.Add(item);
                factorToPutDown = 0.3f;
            }
        }
    }


    private List<int> generateTrack(int trackSize, int segments, int maxBlockPerSegment)
    {
        int segmentSize = trackSize / segments;
        // Debug.Log("segmentSize: " + segmentSize);

        // int maxBlockPerSegment = 10; // 20 * handicap
        List<int> track = new List<int>();

        for (int i = 0; i < segments; i++)
        {
            int minValue = (i * segmentSize) + 1;
            int maxValue = minValue + segmentSize - 1;
            addTrackSegment(track, maxBlockPerSegment, minValue, maxValue);
        }

        track.Sort();

        return track;
    }

    private List<int> addTrackSegment(List<int> track, int maxBlockPerSegment, int minValue, int maxValue)
    {

        for (int i = 0; i < maxBlockPerSegment; i++)
        {
            int randPosition = Random.Range(minValue, maxValue);
            int rangeMin = randPosition - Config.minObstacleDistance;
            int rangeMax = randPosition + Config.minObstacleDistance;
            if (!track.Exists(block => block >= rangeMin && block <= rangeMax))
            {
                track.Add(randPosition);
            }
        }

        return track;
    }
}
