using System;
using UnityEngine;

public class RunnerController : MonoBehaviorSingleton<RunnerController>
{
    [SerializeField] private RunnerGraphic _runner01;
    [SerializeField] private RunnerGraphic _runner02;
    
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
        CurrentTrack = SwitchTrack(CurrentTrack);

        SwitchPositions();
        
        OnTrackChanged?.Invoke();
    }

    private void SwitchPositions()
    {
        _runner01.SetTrackId(SwitchTrack(_runner01.TrackId));
        _runner02.SetTrackId(SwitchTrack(_runner02.TrackId));

        var transform1 = _runner01.transform;
        var transform2 = _runner02.transform;
        (transform1.position, transform2.position) = (transform2.position, transform1.position);
    }

    private int SwitchTrack(int track)
    {
        return track == TrackController.Track01
            ? TrackController.Track02
            : TrackController.Track01;
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
        _currentLives = Mathf.Clamp(_currentLives, 0, _runnerConfig.lives);
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
