using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KullaniciBilgiCekme : MonoBehaviour
{
    [SerializeField] Text name;
    [SerializeField] Text score;
    public LeaderBoard leaderboard;
    private void Start()
    {
        int count = leaderboard.kullaniciBilgileri.Count;
        name.text = leaderboard.kullaniciBilgileri[count - 1].Name;
        if (score != null)
            score.text = leaderboard.kullaniciBilgileri[count - 1].Score.ToString();
    }
}
