using System.Collections.Generic;
using UnityEngine;

public class TrackModel : ITrackModel
{

    private const int REWARD_LIMIT_ADJUSTMENT = 20;

    public List<int> Tracks01 { get; private set; } = new List<int>();
    public List<int> Tracks02 { get; private set; } = new List<int>();

    public List<int> Rewards01 { get; private set; } = new List<int>();
    public List<int> Rewards02 { get; private set; } = new List<int>();

    public TracksConfig Config { get; private set; }

    public TrackModel(TracksConfig tracksConfig, float handicap)
    {
        Config = tracksConfig;
        int maxBlockPerSegment = Mathf.RoundToInt(Config.maxBlockPerSegment * handicap);

        List<int> track = GenerateTrack(Config.size, Config.segments, maxBlockPerSegment);
        SetTracks(track, Config.factorToPutDown);

        if (track != null && track.Count > 0)
        {
            SetRewards(track, Config.rewardAmount, REWARD_LIMIT_ADJUSTMENT, Config.size - REWARD_LIMIT_ADJUSTMENT);
        }

    }

    private void SetTracks(List<int> track, float configFactorToPutDown)
    {
        float factorToPutDown = 0.5f;

        foreach (var item in track)
        {
            if (Random.value >= factorToPutDown)
            {
                Tracks01.Add(item);
                factorToPutDown = configFactorToPutDown;
            }
            else
            {
                Tracks02.Add(item);
                factorToPutDown = 1 - configFactorToPutDown;
            }
        }
    }

    private void SetRewards(List<int> track, int rewardAmount, int minValue, int maxValue)
    {
        foreach (var item in GenerateTrackRewardSegment(track, rewardAmount, minValue, maxValue))
        {
            if (Tracks01.Count < Tracks02.Count && !Tracks01.Contains(item))
            {
                Rewards01.Add(item);
            }
            else
            {
                Rewards02.Add(item);
            }
        }
    }

    private List<int> GenerateTrack(int trackSize, int segments, int maxBlockPerSegment)
    {
        int segmentSize = trackSize / segments;
        List<int> track = new List<int>();

        for (int i = 0; i < segments; i++)
        {
            int minValue = (i * segmentSize) + 1;
            int maxValue = minValue + segmentSize - 1;
            AddTrackSegment(track, maxBlockPerSegment, minValue, maxValue);
        }

        track.Sort();

        return track;
    }

    private void AddTrackSegment(List<int> track, int maxBlockPerSegment, int minValue, int maxValue)
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
    }

    private List<int> GenerateTrackRewardSegment(List<int> track, int rewardAmount, int minValue, int maxValue)
    {
        List<int> newSegmentTrack = new List<int>();

        while (newSegmentTrack.Count < rewardAmount)
        {
            int randPosition = Random.Range(minValue, maxValue);
            int rangeMin = randPosition - Config.minObstacleDistance;
            int rangeMax = randPosition + Config.minObstacleDistance;
            if (!track.Exists(block => block >= rangeMin && block <= rangeMax))
            {
                newSegmentTrack.Add(randPosition);
            }
        }

        return newSegmentTrack;
    }
}
