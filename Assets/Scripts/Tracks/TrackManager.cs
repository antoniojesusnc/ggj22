using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviorSingleton<TrackManager>
{
    [SerializeField] private TrackController _trackPrefab;
    [SerializeField] private Transform _tracksParent;

    [Header("Track Configs")]
    [SerializeField]
    private TracksConfig _tracksConfig;

    private List<TrackController> _currentTracks = new List<TrackController>();


    protected override void Awake()
    {
        base.Awake();

        Init();
    }
    
    private void Init()
    {
        GenerateInitialTracks();
        ClockService.Instance.UpdateEvent += CustomUpdate;
    }
    
    private void GenerateInitialTracks()
    {
        for (int i = 0; i < _tracksConfig.numTracksAlive; i++)
        {
            GenerateTrack();
        }
    }

    private void GenerateTrack()
    {
        var nextPosition = GetNextPosition();
        var newTrack = GetTrackController(nextPosition);
        newTrack.Init(_tracksConfig, nextPosition);
        _currentTracks.Add(newTrack);
    }

    private Vector3 GetNextPosition()
    {
        if (_currentTracks.Count <= 0)
        {
            return Vector3.zero;
        }

        return _currentTracks[_currentTracks.Count - 1].GetFinalPosition();
    }

    private void CustomUpdate(float deltaTime)
    {
        if (_currentTracks.Count <= 0)
        {
            return;
        }

        if (TryCleanTracks())
        {
            GenerateTrack();
        }
    }

    private bool TryCleanTracks()
    {
        if (!_currentTracks[0].ShouldBeDeleted())
        {
            return false;
        }

        _currentTracks[0].Destroy();
        _currentTracks.RemoveAt(0);
        return true;
    }

    private TrackController GetTrackController(Vector3 initialPosition)
    {
        return Instantiate(_trackPrefab, initialPosition, Quaternion.identity, _tracksParent);
    }

    public List<Vector3> GetTrackPositions()
    {
        return _currentTracks[0].GetTrackPositions();
    }
}
