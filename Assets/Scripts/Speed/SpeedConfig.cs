using UnityEngine;

[CreateAssetMenu(fileName = "SpeedConfig", menuName = "Speed/new Speed Config", order = 1)]
public class SpeedConfig : ScriptableObject
{
    [Header("Absolute values")] 
    public float minSpeed;
    public float maxSpeed;
    public float speedIncrementPerSeconds;
    
    [Header("After Hit")]
    public float timeToSlowAfterHit;
    public float timeToRecoverSpeed;
}
