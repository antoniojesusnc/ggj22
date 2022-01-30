using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObstacle : MonoBehaviour
{
    public int TrackId { get; private set; }

    [SerializeField] public int _rotationSpeed;
    [SerializeField] public Transform _graphic;

    [SerializeField] public float _speedFactor;


    void Start()
    {
        _graphic.transform.Rotate(Vector3.forward*-1*Random.value*359f);
    }
    public void SetData(int trackId, int obstaclePosition)
    {
        TrackId = trackId;
        
        transform.localPosition = Vector3.right * obstaclePosition;
    }

    void Update()
    {
        float factor = GameService.Instance.Speed * _speedFactor;
        _graphic.transform.Rotate(Vector3.forward*-1*_rotationSpeed*Time.deltaTime * factor);
    }
}
