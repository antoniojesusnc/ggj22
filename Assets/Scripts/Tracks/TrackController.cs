using System;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    public const int Track01 = 0;
    public const int Track02 = 1;

    public const int TrackAmount = 2;
    
    [Header("Track Configs")]
    [SerializeField]
    private TracksConfig _tracksConfig;
    
    [SerializeField]
    private float _handicap;

    [Header("Track Pieces")]
    [SerializeField]
    private TrackFloor _floorPrefab;
    
    [SerializeField]
    private List<Transform> _floorPosition;

    private ITrackModel _trackModel;

    public Dictionary<int, TrackFloor> _trackFloors = new Dictionary<int, TrackFloor>();

    private void Awake()
    {
        //ClockService.Instance.UpdateEvent += CustomUpdate();
    }

    [ContextMenu("GenerateFloor")]
    private void GenerateFloorDebug()
    {
        GenerateTrack();
    }
    
    public void GenerateTrack()
    {
        var newTrack = GetTracks();
        _trackModel = newTrack;
        
        GenerateFloor(Track01);
        GenerateFloor(Track02);
    }

    private void GenerateFloor(int trackId)
    {
        var floor = Instantiate(_floorPrefab, _floorPosition[trackId]);
        floor.SetData(trackId, _trackModel);
    }

    public ITrackModel GetTracks()
    {
        return new TrackModelMock(_tracksConfig, _handicap);
    }

    [ContextMenu("Test")]
    private void Test()
    {
        var trackModel = new TrackModel(_tracksConfig, _handicap);

        string track = "Track01: ";
        for (int i = 0; i < trackModel.Tracks01.Count; i++)
        {
            track += $"{trackModel.Tracks01[i]}, ";
        }
        Debug.Log(track);
        
        track = "Track02: ";
        for (int i = 0; i < trackModel.Tracks02.Count; i++)
        {
            track += $"{trackModel.Tracks01[i]}, ";
        }
        Debug.Log(track);
    }
    
}
