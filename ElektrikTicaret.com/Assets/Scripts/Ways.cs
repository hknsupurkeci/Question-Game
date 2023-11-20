using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Ways : MonoBehaviour
{
    public void HediyeyiAl()
    {
        SceneManager.LoadScene("FormEkrani");
    }
    public void KameraEkrani()
    {
        SceneManager.LoadScene("FotografEkrani");
    }
    public void GirisSayfasinaDon()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void LeaderBoard()
    {
        SceneManager.LoadScene("LeaderboardEkrani");
    }
    public void KuralEkrani()
    {
        SceneManager.LoadScene("KuralEkrani");
    }
}
