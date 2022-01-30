using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyConfig", menuName = "Game/new Difficulty Config", order = 1)]
public class DifficultyConfig : ScriptableObject
{
    [SerializeField] public int currentDifficulty;

    public List<DifficultyConfigInfo> difficultyConfigInfos;
    
    public DifficultyConfigInfo CurrentDifficulty => difficultyConfigInfos[currentDifficulty];
 
    
    [System.Serializable]
    public class DifficultyConfigInfo
    {
        public BackgroundConfig backgroundConfig;
        public HandicapConfig handicapConfig;
        public RunnerConfig runnerConfig;
        public SkyConfig skyConfig;
        public SpeedConfig speedConfig;
        public TracksConfig tracksConfig;
    }
}
