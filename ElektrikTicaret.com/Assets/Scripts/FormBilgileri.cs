using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FormBilgileri : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI EMail;
    [SerializeField] TextMeshProUGUI Telephone;
    [SerializeField] TextMeshProUGUI FirmaAdi;

    public void SonucGonder()
    {
        User user = new User(EMail.text, Telephone.text, FirmaAdi.text, PlayerPrefs.GetString("playerName"), PlayerPrefs.GetInt("playerScore"));
        GameController.textReader.SaveUser(user);
        SceneManager.LoadScene("HediyeTeslimEkrani");
    }
}
