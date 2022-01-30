using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private DifficultyConfig difficultyConfig; 
    
    public void PlayInDifficulty(int difficulty)
    {
        difficultyConfig.currentDifficulty = difficulty;
        SceneManager.LoadScene(SceneUtils.GameScene);
    }
}
