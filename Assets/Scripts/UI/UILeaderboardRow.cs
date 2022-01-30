using System.Collections;
using System.Collections.Generic;
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
    }

}
