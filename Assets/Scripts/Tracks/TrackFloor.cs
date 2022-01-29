using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TrackFloor : MonoBehaviour
{
    [Header("Graphic")] 
    [SerializeField]
    private Transform _graphic;
    
    [Header("Obstacles")]
    [SerializeField]
    private TrackObstacle _obstaclesPrefab;
    
    [SerializeField]
    private Transform _obstaclesParent;
    
    private int _trackId;
    private ITrackModel _trackModel;
    
    public Dictionary<int, List<TrackObstacle>> _trackObstacles = new Dictionary<int, List<TrackObstacle>>();

    public void SetData(int trackId, ITrackModel trackModel)
    {
        _trackId = trackId;
        _trackModel = trackModel;

        GenerateGraphic();
        GenerateTrackPart(trackId,
            trackId == TrackController.Track01
                ? trackModel.Tracks01
                : trackModel.Tracks02);
    }

    private void GenerateGraphic()
    {
        _graphic.localScale = new Vector3(_trackModel.Config.size, 1, 1);
        _graphic.transform.localPosition = Vector3.right * _graphic.localScale.x * 0.5f;
    }

    private void GenerateTrackPart(int trackId, List<int> tracks)
    {
        for (int i = 0; i < tracks.Count; i++)
        {
            var obstacle = Instantiate(_obstaclesPrefab, _obstaclesParent);
            obstacle.SetData(trackId, tracks[i]);

            if (_trackObstacles.TryGetValue(trackId, out var tracksList))
            {
                tracksList.Add(obstacle);
            }
            else
            {
                _trackObstacles.Add(trackId, new List<TrackObstacle>(){obstacle});
            }
        }
    }

    public void CleanObstacles()
    {
        for (int i = _obstaclesParent.childCount - 1; i >= 0; i--)
        {
            var child = _obstaclesParent.GetChild(i);
            if (child.transform.position.x >= 0)
            {
                var trackObstacle = child.GetComponent<TrackObstacle>();
                _trackObstacles[trackObstacle.TrackId].Remove(trackObstacle);
                Destroy(child.gameObject);
            }
        }
    }
}
