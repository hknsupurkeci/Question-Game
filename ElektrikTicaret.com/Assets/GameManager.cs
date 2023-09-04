using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance

    public LeaderBoard leaderboard;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void AddUserToLeaderboard(User user)
    {
        leaderboard.kullaniciBilgileri.Add(user);
        //leaderboard.SortLeaderboard();  // Eðer ekledikten sonra sýralamak istiyorsanýz
    }
}
