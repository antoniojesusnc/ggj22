using System;
using UnityEngine;

public class RunnerInputMobile : IRunnerInput
{
    public event Action OnKeyPressed;
    private RunnerConfig _runnerConfig;
    
    public RunnerInputMobile(RunnerConfig runnerConfig, Action onInput)
    {
        _runnerConfig = runnerConfig;
        OnKeyPressed += onInput;
        ClockService.Instance.UpdateNonPausableEvent += CustomUpdate;
    }
    public void Destroy()
    {
        ClockService.Instance.UpdateNonPausableEvent -= CustomUpdate;
    }
    
    private void CustomUpdate(float deltaTime)
    {
        if (Input.touchCount > 0)
        {
            OnKeyPressed?.Invoke();
        }
    }
}
