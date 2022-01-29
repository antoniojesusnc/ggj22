using UnityEngine;

public abstract class RunnerSwitcher : MonoBehaviour
{
    [SerializeField]
    protected int _trackId;

    public int TrackId => _trackId;
    
    protected RunnerController _runnerController;
    
    public virtual void Init(RunnerController runnerController)
    {
        _runnerController = runnerController;

        transform.position = TrackManager.Instance.GetTrackPositions()[_trackId];
        SetTrackId(_trackId);
    }

    public void SetTrackId(int trackId)
    {
        _trackId = trackId;
    }
}
