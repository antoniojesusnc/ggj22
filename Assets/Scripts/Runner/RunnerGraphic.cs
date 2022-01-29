using UnityEngine;
using UnityEngine.Serialization;

public class RunnerGraphic : RunnerSwitcher
{
    [SerializeField] 
    private SpriteRenderer _spriteRenderer;
    
    protected override void SetAsAvailable()
    {
        _spriteRenderer.color = _runnerController.RunnerConfig.activeColor;
    }
    
    protected override void SetAsUnAvailable()
    {
        _spriteRenderer.color = _runnerController.RunnerConfig.inActiveColor;
    } 
}
