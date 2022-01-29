using System.Collections;
using UnityEngine;

public class ClockService : MonoBehaviorSingleton<ClockService>
{
    public delegate void CustomUpdateDelegate(float deltaTime);
    public event CustomUpdateDelegate OnUpdateEvent;
    public event CustomUpdateDelegate UpdateNonPausableEvent;

    private float _modTimeScale = 1f;
    
    protected override void Awake()
    {
        base.Awake();

        StartCoroutine(CustomUpdateCo());
    }

    private IEnumerator CustomUpdateCo()
    {
        while (true)
        {
            if (GameService.Instance.State != GameService.GameState.Playing)
            {
                yield return 0;
                continue;
            }
            
            OnUpdateEvent?.Invoke(Time.deltaTime*_modTimeScale);
            UpdateNonPausableEvent?.Invoke(Time.deltaTime);
            yield return 0;
        }
    }

    public void SetDefaultTimeScale() => SetTimeScale(1); 
    
    public void SetTimeScale(float newTimeScale)
    {
        _modTimeScale = newTimeScale;
    }
}
