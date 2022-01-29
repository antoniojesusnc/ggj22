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

    [SerializeField] public float _speed;
    //public float Speed { get; private set; }
    public float Speed => _speed;

    protected override void Awake()
    {
        base.Awake();

        State = GameState.None;
    }

    [ContextMenu("Init")]
    public void Init()
    {
        State = GameState.Playing;
    }
    
}
