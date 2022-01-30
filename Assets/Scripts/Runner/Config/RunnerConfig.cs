using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RunnerConfig", menuName = "Runner/new Runner Config", order = 1)]
public class RunnerConfig : ScriptableObject
{
    [Header("Input")]
    public KeyCode keyToChangeTrack;

    [Header("Config")] public int lives;
    public float rewardHPIncrease;

    [Header("Graphics")] 
    public Sprite normalSprite; 
    public Sprite blackSprite;
    
    public List<RunnerConfigAnimationInfo> animationsClipsBySpeed;
    public List<RunnerConfigAnimationInfo> animationsBlackClipsBySpeed;
    
    [System.Serializable]
    public class RunnerConfigAnimationInfo
    {
        public float speedInit;
        public AnimationUtils.RunnerAnims animation;
    }
}
