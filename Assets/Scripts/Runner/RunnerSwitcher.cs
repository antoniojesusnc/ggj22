using UnityEngine;

public abstract class RunnerSwitcher : MonoBehaviour
{
    [SerializeField]
    protected int _trackId;
    
    protected RunnerController _runnerController;
    
    public virtual void Init(RunnerController runnerController)
    {
        _runnerController = runnerController;
        
        transform.position = TrackManager.Instance.GetTrackPositions()[_trackId];

        runnerController.OnTrackChanged += OnTrackChanged;
        OnTrackChanged();
    }

    private void OnTrackChanged()
    {
        if (_runnerController.CurrentTrack == _trackId)
        {
            SetAsAvailable();
        }
        else
        {
            SetAsUnAvailable();
        }
    }

    protected abstract void SetAsAvailable();

    protected abstract void SetAsUnAvailable();
}
