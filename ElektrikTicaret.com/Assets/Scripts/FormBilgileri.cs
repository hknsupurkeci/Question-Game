using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class FormBilgileri : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI EMail;
    [SerializeField] TextMeshProUGUI Telephone;
    [SerializeField] TextMeshProUGUI FirmaAdi;

    public void SonucGonder()
    {
        string value = string.Empty;
        foreach (char item in EMail.text)
        {
            if (!Convert.ToInt32(item).Equals(8203)) // 8203 asci kodlu "Sýfýr Geniþlik Boþluk"
                value += item;
        }
        PlayerPrefs.SetString("playerEmail", value);
        User user = new User(EMail.text, Telephone.text, FirmaAdi.text, PlayerPrefs.GetString("playerName"), PlayerPrefs.GetInt("playerScore"));
        GameController.textReader.SaveUser(user);
        SceneManager.LoadScene("HediyeTeslimEkrani");
    }
}
