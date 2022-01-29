using System.Collections.Generic;
using UnityEngine;

public class UIService : MonoBehaviorSingleton<UIService>
{
    public const string DISTANCE_COUNTER_FORMAT = "{0} m"; 
    
    [Header("Lives")] 
    [SerializeField] public UILives _livesPrefab;
    [SerializeField] public Transform _livesParent;
    
    [Header("Distance Counter")]
    [SerializeField] public TMPro.TextMeshProUGUI _distanceCounter;
    
    [Header("Popups")]
    [SerializeField] public UIGameOver _gameOver;
    

    private List<UILives> _lives = new List<UILives>();
    RunnerController _runnerController;
    
    protected void Start()
    {
        _runnerController = RunnerController.Instance;
       
        SetLives();
        SetDistanceCounter();
        InitPopups();
    }

    private void InitPopups()
    {
        _gameOver.Init();
    }

    private void SetDistanceCounter()
    {
        GameService.Instance.OnDistanceChange += OnDistanceChange;
        OnDistanceChange(0);
    }

    private void OnDistanceChange(float distance)
    {
        _distanceCounter.text = string.Format(DISTANCE_COUNTER_FORMAT, distance.ToString("N0"));
    }

    private void SetLives()
    {
        for (int i = 0; i < _runnerController.MaxLives; i++)
        {
            var live = Instantiate(_livesPrefab, _livesParent);
            live.Init();
            _lives.Add(live);
        }
        RunnerController.Instance.OnHit += OnRunnerHit;
        OnRunnerHit();
    }
    private void OnRunnerHit()
    {
        for (int i = 0; i < _runnerController.MaxLives; i++)
        {
            _lives[i].SetFill(_runnerController.CurrentLives-i);
        }       
    }
}
