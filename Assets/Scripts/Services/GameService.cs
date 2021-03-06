using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private DifficultyConfig _difficultyConfig;

    public DifficultyConfig.DifficultyConfigInfo CurrentDifficulty => _difficultyConfig.CurrentDifficulty;
    public int CurrentDifficultyNumber => _difficultyConfig.currentDifficulty;
    
    
    [SerializeField]
    private SpeedController _speedController;
    [SerializeField]
    private HandicapController _handicapController;
    //public float Speed { get; private set; }
    public float Speed => _speedController.CurrentSpeed;
    public float Handicap => _handicapController.Handicap;
    public float Distance => _distance;
    private float _distance;

    public event Action<float> OnDistanceChange;
    
    public event Action OnChangeState;
    
    protected override void Awake()
    {
        base.Awake();

        SuscribeToRunnerEvents();
        SetState(GameState.None);

        _speedController.Init();
        ClockService.Instance.OnUpdateEvent += CustomUpdate;
    }

    void Update()
    {
        if (GeneralConfigsService.Instance.IsTutorialShown)
        {
            if (Input.GetKeyDown(CurrentDifficulty.runnerConfig.keyToChangeTrack) ||
                Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (State == GameState.None)
                {
                    Init();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (State == GameState.Playing)
            {
                GameOver();
            }
        }
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
        RunnerController.Instance.OnHitObstacle += OnRunnerHitObstacle;
        RunnerController.Instance.OnDie += OnRunnerDie;
    }

    private void OnRunnerDie()
    {
        GameOver();
    }

    private void OnRunnerHitObstacle()
    {
        _speedController.OnHit();

        if (!RunnerController.Instance.IsDead)
        {
            TrackManager.Instance.CleanAfterHit();
        }
    }

    private void GameOver()
    {
        SetState(GameState.GameOver);
        ClockService.Instance.GameOver();
    }
    
    public void Init()
    {
        SetState(GameState.Playing);
        ClockService.Instance.Play();
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

    public void ReloadGame()
    {
        ClockService.Instance.Dispose();
        
        SceneManager.LoadScene(SceneUtils.GameScene);
    }
}
