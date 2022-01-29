using UnityEngine;
using UnityEngine.Serialization;

public class RunnerGraphic : MonoBehaviour
{
    [SerializeField] 
    private SpriteRenderer _spriteRenderer;
    
    [SerializeField] 
    private int _trackId;

    private RunnerController _runnerController;
    
    public void Init(RunnerController runnerController, int trackId, Vector3 position, bool activeTrack = false)
    {
        transform.position = position;
        
        _runnerController = runnerController;
        _trackId = trackId;

        runnerController.OnTrackChanged += OnTrackChanged;
        OnTrackChanged();
    }

    private void OnTrackChanged()
    {
        if (_runnerController.CurrentTrack == _trackId)
        {
            SetGraphicAvailable();
        }
        else
        {
            SetGraphicAsUnAvailable();
        }
    }

    private void SetGraphicAvailable()
    {
        _spriteRenderer.color = _runnerController.RunnerConfig.activeColor;
    }
    
    private void SetGraphicAsUnAvailable()
    {
        _spriteRenderer.color = _runnerController.RunnerConfig.inActiveColor;
    }

}
