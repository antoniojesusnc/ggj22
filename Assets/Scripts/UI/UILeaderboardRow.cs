using UnityEngine;

public class UILeaderboardRow : MonoBehaviour
{
    public TMPro.TextMeshProUGUI _rank;
    public TMPro.TextMeshProUGUI _player;
    public TMPro.TextMeshProUGUI _score;

    public TMPro.TMP_InputField newPlayer;

    public void SetData(string rank, string playerString, string score)
    {
        _rank.text = rank;
        _player.text = playerString;
        _score.text = score;
        _player.gameObject.SetActive(true);
        newPlayer.gameObject.SetActive(false);
    }

    public void NewData(string rank, string score)
    {
        _rank.text = rank;
        _score.text = score;
        _player.gameObject.SetActive(false);
        newPlayer.gameObject.SetActive(true);
    }

}
