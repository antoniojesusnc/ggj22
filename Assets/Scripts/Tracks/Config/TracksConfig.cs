using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "TrackConfig", menuName = "Tracks/new Tracks Info", order = 1)]
public class TracksConfig : ScriptableObject
{
    [Header("Tracks Properties")] 
    public int minObstacleDistance;
    public int maxBlockPerSegment;
    public int segments;
    public int size;
    public int numTracksAlive;
    public float factorToPutDown;

    [Header("After hit cleaning")]
    [Range(0,1)]
    public float deleteObstaclesIfLessThanPercentageElapsed;

    [Header("Graphic Config")]
    public Sprite[] floorGraphic;
    public float floorSize;
}
