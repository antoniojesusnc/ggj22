using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private DifficultyConfig difficultyConfig;

    private void Start()
    {
        AudioController.Instance.PlaySound(AudioConfig.SoundIDs.startmusic);
    }

    public void PlayInDifficulty(int difficulty)
    {
        GeneralConfigsService.Instance.Tutorial(false);

        difficultyConfig.currentDifficulty = difficulty;
        SceneManager.LoadScene(SceneUtils.GameScene);
    }
}
