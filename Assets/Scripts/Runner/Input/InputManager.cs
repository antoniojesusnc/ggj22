using System;
using UnityEngine;

public class InputManager
{
    private IRunnerInput _runnerInput;
    private RunnerConfig _runnerConfig;
    
    public InputManager(RunnerConfig runnerConfig, Action onInput)
    {
        _runnerConfig = runnerConfig;
        InitInput(onInput);
    }
    
    public void Destroy()
    {
        _runnerInput.Destroy();
    }
    
    private void InitInput(Action OnInput)
    {
        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _runnerInput = new RunnerInputMobile(_runnerConfig, OnInput);
        }
        else
        {
            _runnerInput = new RunnerInputStandalone(_runnerConfig, OnInput);
        }
    }
}
