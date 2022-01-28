using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracksController : MonoBehaviour
{
    [SerializeField]
    private TracksConfig _tracksConfig;
    
    [SerializeField]
    private float _handicap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public ITrackModel GetTracks()
    {
        return new TrackModelMock(_tracksConfig, _handicap);
    }

    [ContextMenu("Test")]
    private void Test()
    {
        var trackModel = new TrackModel(_tracksConfig, _handicap);

        string track = "Track01: ";
        for (int i = 0; i < trackModel.Tracks01.Count; i++)
        {
            track += $"{trackModel.Tracks01[i]}, ";
        }
        Debug.Log(track);
        
        track = "Track02: ";
        for (int i = 0; i < trackModel.Tracks02.Count; i++)
        {
            track += $"{trackModel.Tracks01[i]}, ";
        }
        Debug.Log(track);
    }
    
}
