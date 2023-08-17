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
        //string a = "";
        //for (int i = 0; i < 5; i++)
        //{
        //    User user = new User();
        //    a += "a";
        //    user.Name = a;
        //    user.Score = Random.Range(0, 5000);
        //    leaderboard.kullaniciBilgileri.Add(user);
        //}
        int x = 0;
        leaderboard.SortLeaderboard();
        if(leaderboard.kullaniciBilgileri.Count <= 10)
        {
            //olan kullanýcýlarý sýralar yazdýrýr
            for (int i = 0; i < leaderboard.kullaniciBilgileri.Count; i++)
            {
                siralamaObjeleri[i].name.text = leaderboard.kullaniciBilgileri[i].Name;
                siralamaObjeleri[i].score.text = leaderboard.kullaniciBilgileri[i].Score.ToString();
            }
        }
        else
        {
            //10dan fazla kullanýcý varsa bu döngüyü 10 defa yapacaðý için hata vermez
            foreach (var user in siralamaObjeleri)
            {
                user.name.text = leaderboard.kullaniciBilgileri[x].Name;
                user.score.text = leaderboard.kullaniciBilgileri[x].Score.ToString();
                x++;
            }
        }
        
    }
}
