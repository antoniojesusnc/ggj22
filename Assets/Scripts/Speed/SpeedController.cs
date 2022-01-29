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

    private float _timeStampNormalIncrease;
    
    // lerp
    private float _timeStamp;
    private float _realSpeed;
    private bool _doingLerp;
    private float _initialSpeed;
    private float _finalSpeed;
    private float _duration;
    private Action _afterLerpAction;
    public void Init()
    {
        ClockService.Instance.OnUpdateEvent += CustomUpdate;
    }

    private void CustomUpdate(float deltaTime)
    {
        if (_doingLerp)
        {
            DoingLerp(deltaTime);
        }
        else
        {
            IncrementNormalSpeed(deltaTime);
        }
    }


    private void IncrementNormalSpeed(float deltaTime)
    {
        _timeStampNormalIncrease += deltaTime;
        var factor = Mathf.Clamp01(_timeStampNormalIncrease / _speedConfig.timeToReachMaxSpeed);
        var curveFactor = _speedConfig.speedIncreaseCurve.Evaluate(factor);
        _currentSpeed = Mathf.Lerp(_speedConfig.minSpeed,  _speedConfig.maxSpeed, curveFactor);
    }
    private void DoingLerp(float deltaTime)
    {
        _timeStamp += deltaTime;
        
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
