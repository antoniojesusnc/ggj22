using System;
using UnityEngine;

public class RunnerController : MonoBehaviour
{
    [SerializeField] private RunnerConfig _runnerConfig;
    
    public RunnerConfig RunnerConfig => _runnerConfig;

    public int CurrentTrack { get; private set; }
    
    private InputManager _inputManager;

    public event Action OnTrackChanged;
    
    void Start()
    {
        Init();
    }

    private void Init()
    {
        _inputManager = new InputManager(_runnerConfig, OnInput);
        
        var runnerGrahics = GetComponentsInChildren<RunnerSwitcher>();
        for (int i = 0; i < runnerGrahics.Length; i++)
        {
            runnerGrahics[i].Init(this);
        }
    }

    private void OnInput()
    {
        ChangeTrack();
    }

    private void ChangeTrack()
    {
        CurrentTrack = CurrentTrack == TrackController.Track01
            ? TrackController.Track02
            : TrackController.Track01;

        OnTrackChanged?.Invoke();
    }

    public void OnHitWithObstacle()
    {
        Debug.Log("OnHitWithObstacle");

        GameService.Instance.OnHitWithObstacle();
    }
}
