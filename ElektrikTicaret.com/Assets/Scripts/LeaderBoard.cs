using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeaderBoard", menuName = "Custom/LeaderBoard")]
public class LeaderBoard : ScriptableObject
{
    public List<User> kullaniciBilgileri = new List<User>();
    public void SortLeaderboard()
    {
        kullaniciBilgileri.Sort((user1, user2) => user2.Score.CompareTo(user1.Score)); // En yüksek skordan en düþüðe sýralama
    }
}
