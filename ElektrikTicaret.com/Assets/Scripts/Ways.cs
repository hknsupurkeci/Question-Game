using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ways : MonoBehaviour
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
    public void HediyeyiAl()
    {
        SceneManager.LoadScene("FormEkrani");
    }
    public void SonucGonder()
    {
        SceneManager.LoadScene("HediyeTeslimEkrani");
    }
    public void SampiyonPozuVer()
    {

    }
    public void GirisSayfasinaDon()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void LeaderBoard()
    {
        SceneManager.LoadScene("LeaderboardEkrani");
    }
}
