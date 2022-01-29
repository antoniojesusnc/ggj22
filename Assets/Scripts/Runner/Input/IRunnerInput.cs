using System;


public interface IRunnerInput
{
    public event Action OnKeyPressed;
    void Destroy();
}
