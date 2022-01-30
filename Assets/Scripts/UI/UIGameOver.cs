using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    [SerializeField]
    private UIScoreTable scoreTable;

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
        scoreTable.SaveNewRecord();
        GameService.Instance.ReloadGame();
    }
    
    public void ClickInMainMenu()
    {
        scoreTable.SaveNewRecord();
        SceneManager.LoadScene(SceneUtils.MainMenu);
    }

    
}
