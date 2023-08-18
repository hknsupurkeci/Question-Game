using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ways : MonoBehaviour
{
    public void HediyeyiAl()
    {
        SceneManager.LoadScene("FormEkrani");
    }
    public void SonucGonder()
    {
        SceneManager.LoadScene("HediyeTeslimEkrani");
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
