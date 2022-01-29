using UnityEngine;

[CreateAssetMenu(fileName = "RunnerConfig", menuName = "Runner/new Runner Config", order = 1)]
public class RunnerConfig : ScriptableObject
{
    [Header("Input")]
    public KeyCode keyToChangeTrack;


    [Header("Graphic")] 
    public Color activeColor;
    public Color inActiveColor;

    [Header("Config")] public int lives;
    public float rewardHPIncrease;
}
