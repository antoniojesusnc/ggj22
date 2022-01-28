using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "TrackConfig", menuName = "Tracks/new Tracks Info", order = 1)]
public class TracksConfig : ScriptableObject
{
    public float minObstacleDistance;
    public float deltaHandicap;
    public float size;

    [FormerlySerializedAs("_numTracksAlive")] [Header("Tracks Properties")] 
    public int numTracksAlive;
}
