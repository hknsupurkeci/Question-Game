using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour
{
    [System.Serializable]
    public class GameObjectList
    {
        public Text name;
        public Text score;
    }
    [SerializeField] List<GameObjectList> siralamaObjeleri = new List<GameObjectList>();
    public LeaderBoard leaderboard;

    void Start()
    {
        int x = 0;
        leaderboard.SortLeaderboard();
        if(leaderboard.kullaniciBilgileri.Count <= 10)
        {
            //olan kullan�c�lar� s�ralar yazd�r�r
            for (int i = 0; i < leaderboard.kullaniciBilgileri.Count; i++)
            {
                siralamaObjeleri[i].name.text = leaderboard.kullaniciBilgileri[i].Name;
                siralamaObjeleri[i].score.text = leaderboard.kullaniciBilgileri[i].Score.ToString();
            }
        }
        else
        {
            //10dan fazla kullan�c� varsa bu d�ng�y� 10 defa yapaca�� i�in hata vermez
            foreach (var user in siralamaObjeleri)
            {
                user.name.text = leaderboard.kullaniciBilgileri[x].Name;
                user.score.text = leaderboard.kullaniciBilgileri[x].Score.ToString();
                x++;
            }
        }
        
    }
}
