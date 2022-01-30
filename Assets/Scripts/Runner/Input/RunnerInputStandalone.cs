using System;
using UnityEngine;

public class RunnerInputStandalone : IRunnerInput
{
    public event Action OnKeyPressed;

    private RunnerConfig _runnerConfig;
    
    public RunnerInputStandalone(RunnerConfig runnerConfig, Action onInput)
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
        if (GameService.Instance.State != GameService.GameState.Playing)
        {
            return;
        }
        if (Input.GetKeyDown(_runnerConfig.keyToChangeTrack))
        {
            OnKeyPressed?.Invoke();
        }
    }
}
