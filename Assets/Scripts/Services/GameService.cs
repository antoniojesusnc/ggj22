using System;
using UnityEngine;

public class GameService : MonoBehaviorSingleton<GameService>
{
    public enum GameState
    {
        None,
        Loading,
        Playing,
        GameOver,
    }
    public GameState State { get; private set; }

    [SerializeField]
    private SpeedController _speedController;
    //public float Speed { get; private set; }
    public float Speed => _speedController.CurrentSpeed;
    public float _distance;

    public event Action<float> OnDistanceChange;
    
    public event Action OnChangeState;
    
    protected override void Awake()
    {
        base.Awake();

        SuscribeToRunnerEvents();
        SetState(GameState.None);

        _speedController.Init();
        ClockService.Instance.OnUpdateEvent += CustomUpdate;
        
        // for know, start After time
        ClockService.Instance.AddTimer(2, false, Init);
    }

    private void CustomUpdate(float deltaTime)
    {
        if (Speed == 0 || State != GameState.Playing)
        {
            return;
        }

        _distance += Speed * deltaTime;
        OnDistanceChange?.Invoke(_distance);
    }

    private void SuscribeToRunnerEvents()
    {
        RunnerController.Instance.OnHit += OnRunnerHit;
        RunnerController.Instance.OnDie += OnRunnerDie;
    }

    private void OnRunnerDie()
    {
        GameOver();
    }

    private void OnRunnerHit()
    {
        _speedController.OnHit();
    }

    private void GameOver()
    {
        SetState(GameState.GameOver);
    }
    
    public void Init()
    {
        SetState(GameState.Playing);
    }

    public void SetState(GameState newState)
    {
        if (newState == State)
        {
            return;
        }

        State = newState;
        OnChangeState?.Invoke();
    }
}
