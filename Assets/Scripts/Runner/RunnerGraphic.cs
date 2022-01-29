using System.Collections.Generic;
using UnityEngine;

public class RunnerGraphic : RunnerSwitcher
{
    [SerializeField] 
    private SpriteRenderer _spriteRenderer;
    [SerializeField] 
    private Animator _animator;

    private AnimationClip _currentAnimationClip;
    private AnimationUtils.RunnerAnims _currentAnimation;
    
    private bool _available;
    public override void Init(RunnerController runnerController)
    {
        base.Init(runnerController);

        _currentAnimation = AnimationUtils.RunnerAnims.NONE;
        _available = _trackId == TrackController.Track01;
        CheckAnimation();
        
        ClockService.Instance.OnUpdateEvent += CustomUpdate;
    }

    private void CustomUpdate(float deltaTime)
    {
        CheckAnimation();
    }

    private void CheckAnimation()
    {
        CheckAnimation(
            _available
                ? _runnerController.RunnerConfig.animationsClipsBySpeed
                : _runnerController.RunnerConfig.animationsBlackClipsBySpeed
            ,
            _available
                ? _runnerController.RunnerConfig.normalSprite
                : _runnerController.RunnerConfig.blackSprite);
    }

    private void CheckAnimation(List<RunnerConfig.RunnerConfigAnimationInfo> animations, Sprite sprite)
    {
        var animation = animations[0];
        var speed = GameService.Instance.Speed;
        for (int i = 0; i < animations.Count; i++)
        {
            if (animations[i].speedInit < speed)
            {
                animation = animations[i];
            }
            else
            {
                break;
            }
        }

        if (_currentAnimation != animation.animation)
        {
            _animator.SetTrigger(animation.animation.ToString());
            _spriteRenderer.sprite = sprite;
            _currentAnimation = animation.animation;
        }
    }

    protected override void SetAsAvailable()
    {
        _available = true;
    }

    protected override void SetAsUnAvailable()
    {
        _available = false;
    }
}
