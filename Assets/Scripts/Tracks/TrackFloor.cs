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
    
    [SerializeField]
    private TrackReward _rewardPrefab;
    
    private int _trackId;
    private ITrackModel _trackModel;
    
    public Dictionary<int, List<TrackObstacle>> _trackObstacles = new Dictionary<int, List<TrackObstacle>>();
    public Dictionary<int, List<TrackReward>> _trackRewards = new Dictionary<int, List<TrackReward>>();

    public void SetData(int trackId, ITrackModel trackModel)
    {
        _trackId = trackId;
        _trackModel = trackModel;

        GenerateGraphic();
        GenerateTrackObstacles(trackId,
            trackId == TrackController.Track01
                ? trackModel.Tracks01
                : trackModel.Tracks02);
        
        GenerateTrackRewards(trackId,
            trackId == TrackController.Track01
                ? trackModel.Rewards01
                : trackModel.Rewards02);
    }

    private void GenerateTrackRewards(int trackId, List<int> trackRewards)
    {
        for (int i = 0; i < trackRewards.Count; i++)
        {
            var reward = Instantiate(_rewardPrefab, _obstaclesParent);
            reward.SetData(trackId, trackRewards[i]);

            if (_trackRewards.TryGetValue(trackId, out var tracksList))
            {
                tracksList.Add(reward);
            }
            else
            {
                _trackRewards.Add(trackId, new List<TrackReward>(){reward});
            }
        }    }

    private void GenerateGraphic()
    {
        float numFloors = _trackModel.Config.size / _trackModel.Config.floorSize;
        Vector3 gap = _trackId == TrackController.Track01 ? Vector3.right * 2 : Vector3.zero;
        Vector3 acumPosition = _graphic.transform.localPosition - Vector3.right * 2 + gap;
        for (int i = 0; i < numFloors; i++)
        {
            var newFloor = GameObject.Instantiate(_graphic, _graphic.parent);
            newFloor.transform.localPosition = newFloor.transform.localPosition + acumPosition; 
            newFloor.gameObject.GetComponent<SpriteRenderer>().sprite = _trackModel.Config.floorGraphic[_trackId];
            
            acumPosition += Vector3.right * _trackModel.Config.floorSize;
        }
        
        Destroy(_graphic.gameObject);
        //_graphic.localScale = new Vector3(_trackModel.Config.size, 1, 1);
        //_graphic.transform.localPosition = Vector3.right * _graphic.localScale.x * 0.5f;
    }

    private void GenerateTrackObstacles(int trackId, List<int> tracks)
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

    public void CleanTrack()
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
        
        for (int i = _obstaclesParent.childCount - 1; i >= 0; i--)
        {
            var child = _obstaclesParent.GetChild(i);
            if (child.transform.position.x >= 0)
            {
                var trackObstacle = child.GetComponent<TrackObstacle>();
                if (trackObstacle != null)
                {
                    _trackObstacles[trackObstacle.TrackId].Remove(trackObstacle);
                    Destroy(child.gameObject);
                }
                else
                {
                    var trackReward = child.GetComponent<TrackReward>();
                    
                    if (trackReward != null)
                    {
                        _trackRewards[trackReward.TrackId].Remove(trackReward);
                        Destroy(child.gameObject);
                    }   
                }
            }
        }
    }
}
