using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JokerController : MonoBehaviour
{
    public GameObject cifteSans;
    [SerializeField] GameObject yariYariya;
    [SerializeField] GameObject ekZaman;
    [SerializeField] GameObject jokerInfo;
    [SerializeField] Text jokerInfoText;
    public GameController gameController;
    public ProgressSlider progressSlider;
    //Sounds
    [SerializeField] AudioSource bildirimSound;
    [SerializeField] AudioSource yariYariyaSound;
    [SerializeField] AudioSource ekSureSound;
    [SerializeField] AudioSource cifteSansSound;
    //
    public bool isOnCifteSans = false;
    public void YariYariyaJoker()
    {
        if (progressSlider.isGoing)
        {
            bildirimSound.Play();
            progressSlider.isGoing = false;
            jokerInfo.SetActive(true);
            jokerInfoText.text = "Yarý yarýya joker hakkýnýzý kullandýnýz!\nsizin için iki þýk elenecek.";
            Invoke("YariYari", 3f);
        }
    }
    private void YariYari()
    {
        yariYariyaSound.Play();
        //görüntü
        yariYariya.GetComponent<Button>().interactable = false; // Butonun týklanabilirliðini kapatýr.
        yariYariya.GetComponent<Image>().color = new Color(yariYariya.GetComponent<Image>().color.r, yariYariya.GetComponent<Image>().color.g, yariYariya.GetComponent<Image>().color.b, 0.5f); // Butonun saydamlýðýný arttýrýr.
        //yanlýþ iki þýkký ele
        //burada yanlýþ cevaplarý bir listeye ekliyorum ve daha sonra onlarý random seçip 2 tanesini kapatacaðým.
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
        if (progressSlider.isGoing)
        {
            bildirimSound.Play();
            progressSlider.isGoing = false;
            jokerInfo.SetActive(true);
            jokerInfoText.text = "Ek zaman joker hakkýnýzý kullandýnýz!\nEk süreniz eklendi!";
            Invoke("EkZaman", 3f);
        }
    }
    public void EkZaman()
    {
        ekSureSound.Play();
        //görüntü
        ekZaman.GetComponent<Button>().interactable = false; // Butonun týklanabilirliðini kapatýr.
        ekZaman.GetComponent<Image>().color = new Color(ekZaman.GetComponent<Image>().color.r, ekZaman.GetComponent<Image>().color.g, ekZaman.GetComponent<Image>().color.b, 0.5f); // Butonun saydamlýðýný arttýrýr.
        //eksüre slider ekle
        progressSlider.slider.value = progressSlider.slider.maxValue;
        progressSlider.SayacControl = Mathf.RoundToInt(progressSlider.slider.maxValue);
        //activate
        jokerInfo.SetActive(false);
        progressSlider.isGoing = true;
    }
    public void CifteSansJoker()
    {
        if (progressSlider.isGoing)
        {
            cifteSansSound.Play();
            progressSlider.isGoing = false;
            jokerInfo.SetActive(true);
            jokerInfoText.text = "Cift joker hakkýnýzý kullandýnýz!\nÝki seçenecek seçebilirsiniz!";
            Invoke("CifteSans", 3f);
        }
    }
    public void CifteSans()
    {
        //görüntü
        cifteSans.GetComponent<Button>().interactable = false; // Butonun týklanabilirliðini kapatýr.
        cifteSans.GetComponent<Image>().color = new Color(cifteSans.GetComponent<Image>().color.r, cifteSans.GetComponent<Image>().color.g, cifteSans.GetComponent<Image>().color.b, 0.5f); // Butonun saydamlýðýný arttýrýr.
        //cifte sans aktif
        isOnCifteSans = true;
        //activate
        jokerInfo.SetActive(false);
        progressSlider.isGoing = true;
    }
}
