using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObstacle : MonoBehaviour
{
    public void SetPosition(int obstaclePosition)
    {
        transform.localPosition = Vector3.right * obstaclePosition;
    }
}
