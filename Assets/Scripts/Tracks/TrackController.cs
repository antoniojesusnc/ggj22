using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    public const int Track01 = 0;
    public const int Track02 = 1;

    public const int TrackAmount = 2;
    
    // size times that the element should live
    private const float HOW_FAR_BEFORE_BE_DELETED = 1.2f;
    
    // TODO Delete the serialize field
    [Header("Track Configs")]
    [SerializeField]
    [Tooltip("Do not user this by editor")]
    private TracksConfig _tracksConfig;

    [Header("Track Pieces")]
    [SerializeField]
    private TrackFloor _floorPrefab;

    [SerializeField]
    private List<Transform> _floorPosition;

    private ITrackModel _trackModel;
    private Vector3 _initialPosition;

    public Dictionary<int, TrackFloor> _trackFloors = new Dictionary<int, TrackFloor>();

    public float PercentageRunned => Mathf.Clamp01(-transform.position.x / _tracksConfig.size); 
    
    public bool IsCurrentTrack => transform.position.x > -_tracksConfig.size &&
                                  transform.position.x <= 0;

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
        _trackFloors.Add(trackId, floor);
    }

    private ITrackModel GetTracks()
    {
        return new TrackModel(_tracksConfig, GameService.Instance.Handicap);
    }

    public Vector3 GetFinalPosition()
    {
        return transform.localPosition + Vector3.right * _tracksConfig.size;
    }
    
    public bool ShouldBeDeleted()
    {
        return transform.localPosition.x < -_tracksConfig.size * HOW_FAR_BEFORE_BE_DELETED;
    }
    
    public void CleanTrack()
    {
        foreach (var track in _trackFloors)
        {
            track.Value.CleanTrack();
        }
    }

}
