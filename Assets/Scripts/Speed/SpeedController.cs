using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class SpeedController : MonoBehaviour
{
    [SerializeField]
    private SpeedConfig _speedConfig;
    
    [SerializeField] public float _currentSpeed;
    public float CurrentSpeed => _currentSpeed;

    [SerializeField] 
    private float _timeStamp;
    
    [SerializeField]
    private float _realSpeed;
    
    [SerializeField]
    private bool _doingLerp;

    [SerializeField]
    private float _initialSpeed;
    [SerializeField]
    private float _finalSpeed;
    [SerializeField]
    private float _duration;
    private Action _afterLerpAction;
    public void Init()
    {
        ClockService.Instance.OnUpdateEvent += CustomUpdate;
    }

    private void CustomUpdate(float deltaTime)
    {
        _timeStamp += deltaTime;

        if (_doingLerp)
        {
            DoingLerp();
        }
        else
        {
            IncrementNormalSpeed();
        }
    }


    private void IncrementNormalSpeed()
    {
        if (_timeStamp > 1)
        {
            _currentSpeed += _speedConfig.speedIncrementPerSeconds;
            _timeStamp = 0;
        }
    }
    private void DoingLerp()
    {
        _currentSpeed = Mathf.Lerp(_initialSpeed, _finalSpeed, 
            _timeStamp / _duration );

        if (_timeStamp > _duration)
        {
            _doingLerp = false;
            var callback = _afterLerpAction;
            _afterLerpAction = null;
            callback?.Invoke();
        }
    }

    public void OnHit()
    {
        float currentSpeed = _currentSpeed; 
        // slowing down
        DoLerp(
            _currentSpeed, 
            _speedConfig.minSpeed,  
            _speedConfig.timeToSlowAfterHit,
        () =>
            // recovering speed
            DoLerp(
                _speedConfig.minSpeed, 
                currentSpeed, 
                _speedConfig.timeToRecoverSpeed)
            );
    }

    private void DoLerp(float initialSpeed, float finalSpeed, float duration, Action afterLerpAction = null)
    {
        _doingLerp = true;
        _timeStamp = 0;

        _initialSpeed = initialSpeed;
        _finalSpeed = finalSpeed;
        _duration = duration;

        _afterLerpAction = afterLerpAction;
    }
}
