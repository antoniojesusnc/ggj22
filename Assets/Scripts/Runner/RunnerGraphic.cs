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
        SetInitAnimation();

        runnerController.OnHitObstacle += OnHitWithObstacle;
        
        ClockService.Instance.OnUpdateEvent += CustomUpdate;
    }

    private void SetInitAnimation()
    {
        var animation = _available
            ? AnimationUtils.RunnerAnims.INIT
            : AnimationUtils.RunnerAnims.INIT_BLACK;  
        _animator.SetTrigger(animation.ToString());
        
        _spriteRenderer.sprite = GetSprite();
        _currentAnimation = animation;
    }

    private void OnHitWithObstacle()
    {
        var animation = _available
            ? AnimationUtils.RunnerAnims.HIT
            : AnimationUtils.RunnerAnims.HIT_BLACK;  
        _animator.SetTrigger(animation.ToString());
        
        _spriteRenderer.sprite = GetSprite();
        _currentAnimation = animation;
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
                : _runnerController.RunnerConfig.animationsBlackClipsBySpeed);
    }

    private Sprite GetSprite()
    {
        return _available
            ? _runnerController.RunnerConfig.normalSprite
            : _runnerController.RunnerConfig.blackSprite;
    }

    private void CheckAnimation(List<RunnerConfig.RunnerConfigAnimationInfo> animations)
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
            _animator.ResetTrigger(_currentAnimation.ToString());
            _animator.SetTrigger(animation.animation.ToString());
            _spriteRenderer.sprite = GetSprite();
            _currentAnimation = animation.animation;
        }
    }
}
