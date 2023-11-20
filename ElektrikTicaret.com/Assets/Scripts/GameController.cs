using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VectorGraphics;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [System.Serializable] public class Questions
    {
        public string quest;
        public string answerOne;
        public string answerTwo;
        public string answerThree;
        public string answerFour;
        public int correctAnswerId;
        public int questTime;
        public int questScore;
    }
    //Screens
    [SerializeField] GameObject SoruEkrani;
    [SerializeField] GameObject StartEkrani;
    [SerializeField] GameObject GecisEkrani;
    //Question Variables
    [SerializeField] List<Questions> _questions;
    //Soru ekrani soru bilgileri
    [SerializeField] Text questionText;
    public GameObject a;
    public GameObject b;
    public GameObject c;
    public GameObject d;
    [SerializeField] GameObject explosion;
    Color defaultColorButtons;
    //Active soru numarasi
    [SerializeField] Text questId;
    float questIdNumber = 1;
    //User bilgileri
    public static User ActiveUser;
    [SerializeField] Text GamePersonName;
    [SerializeField] Text UserScore;
    //Backgrounds
    [SerializeField] GameObject backgroundPanel;
    [SerializeField] Sprite inGameImage;
    private Sprite startScreenImage;
    //Questions info
    public static Questions activeQuestion;
    List<int> selectionQuests = new List<int>(); //ekrana gelmiþ soru idleri
    //Input alani
    public Animator anim;
    [SerializeField] TMP_InputField adSoyadInputText;
    string startValueInput = string.Empty;
    //Slider timer
    public ProgressSlider slider;
    //Max Quest Count
    public int maxQuestCount = 12;
    //leaderboard
    public LeaderBoard leaderboard;
    //Sounds
    [SerializeField] AudioSource baslatSound;
    [SerializeField] AudioSource correctAnswer;
    [SerializeField] AudioSource wrongAnswer;
    //test kullanici verisi
    private string[] names = { "Ali Rýza", "Uður Aslan", "Kemal Keskin", "Gürkan Güleþ", "Sümeyra Þaka", "Tuðba Uyumaz", "Elif Can Kuþ", "Hakan Süpürkeci", "Bilal Yakýcý", "Nazým Bardaklý" };
    //Joker Controller
    public JokerController jokerController;
    //Gift object
    [SerializeField] GameObject Gift;
    //Text reader
    public static TextReader textReader = new TextReader();
    // Selected GameObject
    private GameObject selectedGameObject;
    //
    public GameObject PopupScreen;
    public Button PopupButton;
    private void Start()
    {
        Debug.Log(selectionQuests.Count);
        jokerController.cifteSans.GetComponent<Button>().interactable = false; // Butonun týklanabilirliðini kapatýr.
        leaderboard.kullaniciBilgileri.Clear();
        leaderboard.kullaniciBilgileri.AddRange(textReader.LoadAllUsers());
        FakeUsers();
        ActiveUser = new User();
        startValueInput = adSoyadInputText.text;
        startScreenImage = backgroundPanel.GetComponent<Image>().sprite;
        defaultColorButtons = a.GetComponent<SVGImage>().color;
    }
    private void Update()
    {
        if(slider.slider.value <= 0 && slider.isGoing)
        {
            slider.isGoing = false;
            wrongAnswer.Play();
            Invoke("WrongAnswer", 3f);
        }
    }

    private void FakeUsers()
    {
        Debug.Log($"User count : {textReader.UsersCount}");
        if (textReader.UsersCount == 0)
        {
            for (int i = 0; i < names.Length; i++)
            {
                User user = new User();
                user.Name = names[i];
                user.Score = Random.Range(0, 1000);
                user.EMail = "test@testmail.com";
                user.Telephone = "05555555555";
                user.Firma = "EAE Elektrik";
                leaderboard.kullaniciBilgileri.Add(user);
                //textReader
                textReader.SaveUser(user);
            }
        }
    }

    public void StartGame()
    {
        //Application.OpenURL("https://www.elektrikticaret.com/");
        //Ad Soyad kýsmý boþ ise
        if (adSoyadInputText.text == startValueInput)
        {
            anim.SetTrigger("hover");
        }
        else
        {
            //sound
            baslatSound.Play();
            //User bilgileri düzenleme
            GamePersonName.text = adSoyadInputText.text;
            //Sahne geçiþi
            StartEkrani.SetActive(false);
            SoruEkrani.SetActive(true);
            //Select Question
            SelectQuestion();
            //Soru ekranýnda gelecek olan background
            backgroundPanel.GetComponent<Image>().sprite = inGameImage;
            //User bilgileri
            ActiveUser.Name = adSoyadInputText.text;
            UserScore.text = ActiveUser.Score.ToString();
            slider.questTime = _questions[selectionQuests.Count - 1].questTime;
        }
    }
    private void EndGame()
    {
        //adSoyadInputText.text = startValueInput;
        //GamePersonName.text = startValueInput;
        //backgroundPanel.GetComponent<Image>().sprite = startScreenImage;
        //SoruEkrani.SetActive(false);
        //StartEkrani.SetActive(true);

        //selectionQuests.Clear();

        User user = new User();
        user.Name = ActiveUser.Name;
        user.Score = ActiveUser.Score;
        //leaderboard.kullaniciBilgileri.Add(user);
        GameManager.Instance.AddUserToLeaderboard(user);
        //PlayerPrefs
        PlayerPrefs.SetString("playerName", ActiveUser.Name);
        PlayerPrefs.SetInt("playerScore", ActiveUser.Score);

        //ActiveUser = new User();
        //questIdNumber = 1;
        SceneManager.LoadScene("SonucEkrani");
    }

    /// <summary>
    /// Sorularýn içerisinden random bir soru seçer ve o soru oyun boyunca bir daha gelmez.
    /// </summary>
    private void SelectQuestion()
    {
        while (true)
        {
            int randomQuestionId = Random.Range(0, _questions.Count);
            if (!selectionQuests.Contains(randomQuestionId))
            {
                GecisEkrani.SetActive(false);
                SoruEkrani.SetActive(true);
                activeQuestion = _questions[randomQuestionId];
                questionText.text = _questions[randomQuestionId].quest;
                //seçili sorular
                selectionQuests.Add(randomQuestionId);
                a.GetComponentInChildren<Text>().text = "A: "+_questions[randomQuestionId].answerOne;
                b.GetComponentInChildren<Text>().text = "B: "+_questions[randomQuestionId].answerTwo;
                c.GetComponentInChildren<Text>().text = "C: "+_questions[randomQuestionId].answerThree;
                d.GetComponentInChildren<Text>().text = "D: "+_questions[randomQuestionId].answerFour;

                //aktivete joker and gift
                if (questIdNumber == 6)
                    HalfGameObjectsTrue();

                break;
            }
            else if (selectionQuests.Count == _questions.Count)
            {
                EndGame();
                break;
            }
        }
        
    }
    #region Answers
    public void AnswerA(int id = 1)
    {
        ButtonsDeactiveted();

        if (id == activeQuestion.correctAnswerId)
        {
            //Confeti
            Instantiate(explosion, Vector3.zero, Quaternion.identity);
            //Color change
            a.GetComponent<SVGImage>().color = new Color(0f, 1f, 0.439f);
            //Yeni Soru
            //Invoke olabilir.
            Invoke("NextQuestion", 3f);
            correctAnswer.Play();
        }
        else
        {
            a.GetComponent<SVGImage>().color = new Color(0.8f, 0f, 0f);
            ToWhiteAnswerChildText(a);
            Invoke("WrongAnswer", 3f);
            wrongAnswer.Play();

        }
        selectedGameObject = a;
        slider.isGoing = false;
    }

    public void AnswerB(int id = 2)
    {
        ButtonsDeactiveted();

        if (id == activeQuestion.correctAnswerId)
        {
            //Confeti
            Instantiate(explosion, Vector3.zero, Quaternion.identity);
            //Color change
            b.GetComponent<SVGImage>().color = new Color(0f, 1f, 0.439f);
            //Yeni Soru
            //Invoke olabilir.
            Invoke("NextQuestion", 3f);
            correctAnswer.Play();

        }
        else
        {
            b.GetComponent<SVGImage>().color = new Color(0.8f, 0f, 0f);

            ToWhiteAnswerChildText(b);

            Invoke("WrongAnswer", 3f);
            wrongAnswer.Play();

        }
        selectedGameObject = b;
        slider.isGoing = false;
    }
    public void AnswerC(int id = 3)
    {
        ButtonsDeactiveted();

        if (id == activeQuestion.correctAnswerId)
        {
            //Confeti
            Instantiate(explosion, Vector3.zero, Quaternion.identity);
            //Color change
            c.GetComponent<SVGImage>().color = new Color(0f, 1f, 0.439f);
            //Yeni Soru
            //Invoke olabilir.
            Invoke("NextQuestion", 3f);
            correctAnswer.Play();

        }
        else
        {
            c.GetComponent<SVGImage>().color = new Color(0.8f, 0f, 0f);

            ToWhiteAnswerChildText(c);

            Invoke("WrongAnswer", 3f);
            wrongAnswer.Play();

        }
        selectedGameObject = c;
        slider.isGoing = false;
    }
    public void AnswerD(int id = 4)
    {
        ButtonsDeactiveted();

        if (id == activeQuestion.correctAnswerId)
        {
            //Confeti
            Instantiate(explosion, Vector3.zero, Quaternion.identity);
            //Color change
            d.GetComponent<SVGImage>().color = new Color(0f, 1f, 0.439f);
            //Yeni Soru
            //Invoke olabilir.
            Invoke("NextQuestion", 3f);
            correctAnswer.Play();


        }
        else
        {
            d.GetComponent<SVGImage>().color = new Color(0.8f, 0f, 0f);

            ToWhiteAnswerChildText(d);

            Invoke("WrongAnswer", 3f);
            wrongAnswer.Play();
        }
        selectedGameObject = d;
        slider.isGoing = false;
    }
    #endregion
    private void NextQuestion()
    {
        if (questIdNumber < maxQuestCount)
        {
            //Joker cifte sans control
            jokerController.isOnCifteSans = jokerController.isOnCifteSans ? false : false; // burada çifte þans varken doðru bildiyse geri false oluyor
            //Gecis ekrani
            SoruEkrani.SetActive(false);
            GecisEkrani.SetActive(true);
            //Game Screen ayarlamalarý süre, soru, puan
            Debug.Log((_questions[selectionQuests.Count - 1].questScore + " - " + slider.SayacControl));
            ActiveUser.Score += (_questions[selectionQuests.Count - 1].questScore * slider.SayacControl); // burada bana her zaman son gelen sorunun puanýný getirecek
            slider.questTime = _questions[selectionQuests.Count - 1].questTime;
            
            slider.ResetScore();

            UserScore.text = ActiveUser.Score.ToString();
            slider.isGoing = true;
            //SelectQuestion();
            //2 saniye sonra yeni soru gelecek
            Invoke("SelectQuestion", 2f);
            ButtonsActiveted();
            questIdNumber++;
            questId.text = questIdNumber.ToString();
        }
        else
        {
            ActiveUser.Score += (_questions[selectionQuests.Count - 1].questScore * slider.SayacControl); // burada bana her zaman son gelen sorunun puanýný getirecek
            UserScore.text = ActiveUser.Score.ToString();
            EndGame();
        }
    }
    private void WrongAnswer()
    {
        if(questIdNumber < maxQuestCount || jokerController.isOnCifteSans)
        {
            //joker control
            if (jokerController.isOnCifteSans)
            {
                AfterJokerButtonsActivated();
                selectedGameObject.SetActive(false);
                jokerController.isOnCifteSans = false;
            }
            else
            {
                Debug.Log(questIdNumber);
                //Gecis ekrani
                SoruEkrani.SetActive(false);
                GecisEkrani.SetActive(true);
                //
                slider.questTime = _questions[selectionQuests.Count - 1].questTime;
                slider.ResetScore();
                //ActiveUser.Score += _questions[selectionQuests.Count - 1].questScore; // burada bana her zaman son gelen sorunun puanýný getirecek
                //UserScore.text = ActiveUser.Score.ToString();
                slider.isGoing = true;
                ButtonsActiveted();
                //EndGame();
                //questIdNumber = 1;
                questIdNumber++;
                questId.text = questIdNumber.ToString();
                Invoke("SelectQuestion", 3f);
                //SelectQuestion();
                //SceneManager.LoadScene("FormEkrani");
            }
        }
        else
        {
            EndGame();
        }
    }

    /// <summary>
    /// Yeni soruya geçmeden önce buttonlarý düzeltir.
    /// </summary>
    private void ButtonsActiveted()
    {
        GameObject[] gameObjects = new GameObject[] { a, b, c, d }; 
        a.GetComponent<Button>().enabled = true;
        a.SetActive(true);
        a.GetComponent<SVGImage>().color = defaultColorButtons;
        b.GetComponent<Button>().enabled = true;
        b.SetActive(true);
        b.GetComponent<SVGImage>().color = defaultColorButtons;
        c.GetComponent<Button>().enabled = true;
        c.SetActive(true);
        c.GetComponent<SVGImage>().color = defaultColorButtons;
        d.GetComponent<Button>().enabled = true;
        d.SetActive(true);
        d.GetComponent<SVGImage>().color = defaultColorButtons;
        PopupButton.enabled = true;
        for (int i = 0; i < gameObjects.Length; i++)
        {
            Transform parentTransform = gameObjects[i].transform; // Ana objeyi seçin

            // Tüm child objeleri döngü ile iþleyin
            foreach (Transform child in parentTransform)
            {
                child.GetComponent<Text>().color = new Color(0f, 0f, 0f);
            }
        }
    }
    private void AfterJokerButtonsActivated()
    {
        a.GetComponent<Button>().enabled = true;
        a.GetComponent<SVGImage>().color = defaultColorButtons;
        b.GetComponent<Button>().enabled = true;
        b.GetComponent<SVGImage>().color = defaultColorButtons;
        c.GetComponent<Button>().enabled = true;
        c.GetComponent<SVGImage>().color = defaultColorButtons;
        d.GetComponent<Button>().enabled = true;
        d.GetComponent<SVGImage>().color = defaultColorButtons;
        PopupButton.enabled = true;
    }
    /// <summary>
    /// Butonlarýn enable deðiþkenini false yapar bu sayede bir daha seçim yapýlamaz.
    /// </summary>
    private void ButtonsDeactiveted()
    {
        a.GetComponent<Button>().enabled = false;
        b.GetComponent<Button>().enabled = false;
        c.GetComponent<Button>().enabled = false;
        d.GetComponent<Button>().enabled = false;
        PopupButton.enabled = false;
    }

    private void ToWhiteAnswerChildText(GameObject x)
    {
        Transform parentTransform = x.transform; // Ana objeyi seçin
                                                 // Tüm child objeleri döngü ile iþleyin
        foreach (Transform child in parentTransform)
        {
            // Child objeye eriþim saðlandý
            // Örnek olarak child objenin ölçülerini deðiþtirme
            child.GetComponent<Text>().color = new Color(1f, 1f, 1f);
        }
    }

    /// <summary>
    /// oyunun yarýsýna geldiðinde belirli objeleri kullanýlýr hale getirir.
    /// </summary>
    public void HalfGameObjectsTrue()
    {
        jokerController.cifteSans.GetComponent<Button>().interactable = true; // Butonun týklanabilirliðini kapatýr.
        jokerController.cifteSans.GetComponent<Image>().color =
            new Color(jokerController.cifteSans.GetComponent<Image>().color.r, jokerController.cifteSans.GetComponent<Image>().color.g, jokerController.cifteSans.GetComponent<Image>().color.b, 1f); // Butonun saydamlýðýný arttýrýr.
        Gift.SetActive(true);
        Gift.GetComponent<Animator>().SetTrigger("play");
    }

    public void PopUpOpenClose(bool value)
    {
        if(value)
        {
            PopupScreen.SetActive(true);
            slider.isGoing = false;
        }
        else
        {
            PopupScreen.SetActive(false);
            slider.isGoing = true;
        }
    }
}
