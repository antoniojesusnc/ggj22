using System;
using System.Collections;
using UnityEngine;

public class ClockService : MonoBehaviorSingleton<ClockService>
{
    public event Action<float> UpdateEvent;
    public event Action<float> UpdateNonPausableEvent;

    private float _modtimeScale;
    protected override void Awake()
    {
        base.Awake();

        StartCoroutine(CustomUpdateCo());
    }

    private IEnumerator CustomUpdateCo()
    {
        yield return 0;
        UpdateEvent?.Invoke(Time.deltaTime*_modtimeScale);
        UpdateNonPausableEvent?.Invoke(Time.deltaTime);
    }

    public void SetDefaultTimeScale() => SetTimeScale(1); 
    
    public void SetTimeScale(float newTimeScale)
    {
        _modtimeScale = newTimeScale;
    }
}
