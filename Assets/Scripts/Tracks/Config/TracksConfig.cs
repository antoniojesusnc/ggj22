using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "TrackConfig", menuName = "Tracks/new Tracks Info", order = 1)]
public class TracksConfig : ScriptableObject
{
    public int minObstacleDistance;
    public float deltaHandicap;
    public int maxBlockPerSegment;
    public int segments;
    public int size;

    [FormerlySerializedAs("_numTracksAlive")] [Header("Tracks Properties")] 
    public int numTracksAlive;
}
