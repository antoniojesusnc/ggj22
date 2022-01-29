using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObstacle : MonoBehaviour
{
    public int TrackId { get; private set; }
    
    public void SetData(int trackId, int obstaclePosition)
    {
        TrackId = trackId;
        
        transform.localPosition = Vector3.right * obstaclePosition;
    }
}
