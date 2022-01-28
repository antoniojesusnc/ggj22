using UnityEngine;

[CreateAssetMenu(fileName = "TrackConfig", menuName = "Tracks/new Tracks Info", order = 1)]
public class TracksConfig : ScriptableObject
{
    public float minObstacleDistance;
    public float deltaHandicap;
    public float size;
}
