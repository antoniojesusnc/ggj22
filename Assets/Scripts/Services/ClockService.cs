using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockService : MonoBehaviorSingleton<ClockService>
{
    public delegate void CustomUpdateDelegate(float deltaTime);
    public event CustomUpdateDelegate OnUpdateEvent;
    public event CustomUpdateDelegate UpdateNonPausableEvent;

    private float _modTimeScale = 1f;

    private List<ClockServiceTimers> _timers = new List<ClockServiceTimers>();

    private bool doUpdate;

    protected override void Awake()
    {
        base.Awake();

        StartCoroutine(CustomUpdateCo());
    }
    
    public override void Dispose()
    {
        OnUpdateEvent = null;
        UpdateNonPausableEvent = null;
        
        _timers.Clear();
    }
    
    public void AddTimer(float duration, bool pausable, Action callback)
    {
        _timers.Add(new ClockServiceTimers(duration, callback, pausable));
    }

    private IEnumerator CustomUpdateCo()
    {
        while (true)
        {
            UpdateNonPausableEvent?.Invoke(Time.deltaTime);
            CheckTimers(Time.deltaTime, false);

            if (!doUpdate)
            {
                yield return 0;
                continue;
            }    

            float deltaTimeScaled = Time.deltaTime * _modTimeScale;
            OnUpdateEvent?.Invoke(deltaTimeScaled);
            CheckTimers(deltaTimeScaled, true);
            yield return 0;
        }
    }

    public void Play()
    {
        doUpdate = true;
    }

    public void GameOver()
    {
        doUpdate = false;
    }
    
    private void CheckTimers(float deltaTime, bool canBePausedTimers)
    {
        for (int i = _timers.Count - 1; i >= 0; i--)
        {
            if (_timers[i].isPausable != canBePausedTimers)
            {
                continue;
            }
            
            _timers[i].timer -= deltaTime;
            if (_timers[i].timer <= 0)
            {
                _timers[i].callback?.Invoke();
                _timers.RemoveAt(i);
            }
        }
    }

    public void SetDefaultTimeScale() => SetTimeScale(1); 
    
    public void SetTimeScale(float newTimeScale)
    {
        _modTimeScale = newTimeScale;
    }

    private class ClockServiceTimers
    {
        public float timer;
        public Action callback;
        public bool isPausable; 
        
        public ClockServiceTimers(float duration, Action action, bool pausable)
        {
            timer = duration;
            callback = action;
            isPausable = pausable;
        }
    }
}
