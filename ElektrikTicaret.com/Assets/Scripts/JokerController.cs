using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JokerController : MonoBehaviour
{
    [SerializeField] GameObject yariYariya;
    [SerializeField] GameObject cifteSans;
    [SerializeField] GameObject ekZaman;
    [SerializeField] GameObject jokerInfo;
    [SerializeField] Text jokerInfoText;
    public GameController gameController;
    public ProgressSlider progressSlider;
    public void YariYariyaJoker()
    {
        progressSlider.isGoing = false;
        jokerInfo.SetActive(true);
        jokerInfoText.text = "Yar� yar�ya joker hakk�n�z� kulland�n�z!\nsizin i�in iki ��k elenecek.";
        Invoke("YariYari", 3f);
    }
    private void YariYari()
    {
        //g�r�nt�
        yariYariya.GetComponent<Button>().interactable = false; // Butonun t�klanabilirli�ini kapat�r.
        yariYariya.GetComponent<Image>().color = new Color(yariYariya.GetComponent<Image>().color.r, yariYariya.GetComponent<Image>().color.g, yariYariya.GetComponent<Image>().color.b, 0.5f); // Butonun saydaml���n� artt�r�r.
        //yanl�� iki ��kk� ele
        //burada yanl�� cevaplar� bir listeye ekliyorum ve daha sonra onlar� random se�ip 2 tanesini kapataca��m.
        List<int> ids = new List<int>();
        for (int i = 1; i <= 4/*cevap sayisi*/; i++)
        {
            if (i != GameController.activeQuestion.correctAnswerId)
                ids.Add(i);
        }
        int randomOne = ids[Random.Range(0, ids.Count)];
        ids.Remove(randomOne);
        int randomTwo = ids[Random.Range(0, ids.Count)];

        if (randomOne == 1 || randomTwo == 1)
        {
            gameController.a.SetActive(false);
        }
        if (randomOne == 2 || randomTwo == 2)
        {
            gameController.b.SetActive(false);

        }
        if (randomOne == 3 || randomTwo == 3)
        {
            gameController.c.SetActive(false);
        }
        if (randomOne == 4 || randomTwo == 4)
        {
            gameController.d.SetActive(false);
        }
        jokerInfo.SetActive(false);
        progressSlider.isGoing = true;
    }
    public void EkZamanJoker()
    {
        progressSlider.isGoing = false;
        jokerInfo.SetActive(true);
        jokerInfoText.text = "Ek zaman joker hakk�n�z� kulland�n�z!\nEk s�reniz eklendi!";
        Invoke("EkZaman", 3f);
    }
    public void EkZaman()
    {
        //g�r�nt�
        ekZaman.GetComponent<Button>().interactable = false; // Butonun t�klanabilirli�ini kapat�r.
        ekZaman.GetComponent<Image>().color = new Color(ekZaman.GetComponent<Image>().color.r, ekZaman.GetComponent<Image>().color.g, ekZaman.GetComponent<Image>().color.b, 0.5f); // Butonun saydaml���n� artt�r�r.
        //eks�re slider ekle
        progressSlider.slider.value = progressSlider.slider.maxValue;
        progressSlider.SayacControl = Mathf.RoundToInt(progressSlider.slider.maxValue);
        //activate
        jokerInfo.SetActive(false);
        progressSlider.isGoing = true;
    }
    public void CifteSansJoker()
    {
        //g�r�nt�
        cifteSans.GetComponent<Button>().interactable = false; // Butonun t�klanabilirli�ini kapat�r.
        cifteSans.GetComponent<Image>().color = new Color(cifteSans.GetComponent<Image>().color.r, cifteSans.GetComponent<Image>().color.g, cifteSans.GetComponent<Image>().color.b, 0.5f); // Butonun saydaml���n� artt�r�r.
    }
}
