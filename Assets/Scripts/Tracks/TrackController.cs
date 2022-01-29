using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    public const int Track01 = 0;
    public const int Track02 = 1;

    public const int TrackAmount = 2;
    
    // size times that the element should live
    private const int HOW_FAR_BEFORE_BE_DELETED = 2;
    
    // TODO Delete the serialize field
    [Header("Track Configs")]
    [SerializeField]
    [Tooltip("Do not user this by editor")]
    private TracksConfig _tracksConfig;
    
    [SerializeField]
    private float _handicap;

    [Header("Track Pieces")]
    [SerializeField]
    private TrackFloor _floorPrefab;
    
    [SerializeField]
    private List<Transform> _floorPosition;

    private ITrackModel _trackModel;
    private Vector3 _initialPosition;

    public Dictionary<int, TrackFloor> _trackFloors = new Dictionary<int, TrackFloor>();

    public void Init(TracksConfig tracksConfig, Vector3 initialPosition)
    {
        _initialPosition = initialPosition;
        _tracksConfig = tracksConfig;
        
        ClockService.Instance.OnUpdateEvent += CustomOnUpdate;

        GenerateTrack();
    }
    
    public void Destroy()
    {
        ClockService.Instance.OnUpdateEvent -= CustomOnUpdate;
        Destroy(gameObject);
    }

    private void CustomOnUpdate(float deltaTime)
    {
        transform.Translate(Vector3.left * deltaTime * GameService.Instance.Speed);
    }
    
    public List<Vector3> GetTrackPositions()
    {
        List<Vector3> trackPositions = new List<Vector3>();
        for (int i = 0; i < _floorPosition.Count; i++)
        {
            trackPositions.Add(_floorPosition[i].position);
        }

        return trackPositions;
    }
    
    private void GenerateFloorDebug()
    {
        GenerateTrack();
    }
    
    private void GenerateTrack()
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

    private ITrackModel GetTracks()
    {
        return new TrackModelMock(_tracksConfig, _handicap);
    }

    public Vector3 GetFinalPosition()
    {
        return transform.position + Vector3.right * _tracksConfig.size;
    }
    
    public bool ShouldBeDeleted()
    {
        return transform.position.x < -_tracksConfig.size * HOW_FAR_BEFORE_BE_DELETED;
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
