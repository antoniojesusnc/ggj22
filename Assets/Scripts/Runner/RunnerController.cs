using System;
using UnityEngine;

public class RunnerController : MonoBehaviorSingleton<RunnerController>
{
    [SerializeField] private float _currentLives;

    [SerializeField] private RunnerConfig _runnerConfig;
    public RunnerConfig RunnerConfig => _runnerConfig;
    public int CurrentTrack { get; private set; }
    public float CurrentLives => _currentLives;
    public int MaxLives => _runnerConfig.lives;
    
    private InputManager _inputManager;
    public event Action OnTrackChanged;
    public event Action OnHitObstacle;
    public event Action OnHitReward;
    public event Action OnDie;
    
    void Start()
    {
        Init();
    }

    private void Init()
    {
        _inputManager = new InputManager(_runnerConfig, OnInput);
        _currentLives = _runnerConfig.lives;
        
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
        --_currentLives;
        OnHitObstacle?.Invoke();
        
        CheckIfDie();
    }

    public void OnHitWithReward()
    {
        _currentLives += _runnerConfig.rewardHPIncrease;
        OnHitReward?.Invoke();
    }

    private void CheckIfDie()
    {
        if (_currentLives < 1)
        {
            OnDie?.Invoke();
        }
    }
}
