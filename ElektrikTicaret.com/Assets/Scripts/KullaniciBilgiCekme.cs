using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KullaniciBilgiCekme : MonoBehaviour
{
    [SerializeField] Text playerName;
    [SerializeField] Text playerScore;
    public LeaderBoard leaderboard;
    private void Start()
    {
        playerName.text = PlayerPrefs.GetString("playerName");
        if (playerScore != null)
            playerScore.text = PlayerPrefs.GetInt("playerScore").ToString();
        //int count = leaderboard.kullaniciBilgileri.Count;
        //name.text = leaderboard.kullaniciBilgileri[count - 1].Name;
        //if (score != null)
        //    score.text = leaderboard.kullaniciBilgileri[count - 1].Score.ToString();
    }
}
