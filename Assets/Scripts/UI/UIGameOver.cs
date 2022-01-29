using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    public void Init()
    {
        GameService.Instance.OnChangeState += OnChangeState;
        gameObject.SetActive(false);
    }
    
    private void OnChangeState()
    {
        if (GameService.Instance.State == GameService.GameState.GameOver)
        {
            gameObject.SetActive(true);
        }
    }

    public void ClickInPlayAgain()
    {
        GameService.Instance.ReloadGame();
    }
    
    public void ClickInMainMenu()
    {
        SceneManager.LoadScene(SceneUtils.MainMenu);
    }

    
}
