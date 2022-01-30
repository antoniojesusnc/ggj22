using System;
using UnityEngine;
using UnityEngine.Events;

public class FMODMusic : MonoBehaviour
{
    private const string VELOCITY_NAME = "Velocidad";

    private FMOD.Studio.EventInstance instance;
    [SerializeField]
    private FMODUnity.EventReference fmodEvent;
    private SpeedConfig _speedConfig;
    [SerializeField]
    private FMODUnity.EditorParamRef _parameter;

    private int _velocity;
    private int _mod;
    private float _musicTracksAmount;
    
    void Start()
    {
        _speedConfig = GameService.Instance.CurrentDifficulty.speedConfig;
        
        GameService.Instance.OnChangeState += OnChangedState;
        OnChangedState();
   
    }

    private void OnChangedState()
    {
        if (GameService.Instance.State == GameService.GameState.Playing)
        {
            instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
            _musicTracksAmount = _parameter.Max;
            _mod = Mathf.RoundToInt(_speedConfig.maxSpeed / _musicTracksAmount);
            ClockService.Instance.OnUpdateEvent += CustomUpdate;

            StartMusic();

        }
        else if (GameService.Instance.State == GameService.GameState.GameOver)
        {
            StopMusic();
            ClockService.Instance.OnUpdateEvent -= CustomUpdate;
        }
    }

    private void CustomUpdate(float deltaTime)
    {
        
        if (_velocity == ((Mathf.RoundToInt(GameService.Instance.Speed) / _mod) + 1))
        {
            return;
        }
        _velocity = ((Mathf.RoundToInt(GameService.Instance.Speed) / _mod) + 1);
        SetMusicTrack();
    }

    private void SetMusicTrack()
    {
        instance.setParameterByName(VELOCITY_NAME, _velocity);
    }

    private void StartMusic()
    {
        instance.start();
    }

    public void StopMusic()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}

