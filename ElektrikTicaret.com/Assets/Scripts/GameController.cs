using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VectorGraphics;

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
    }

    [SerializeField] List<Questions> _questions;
    [SerializeField] GameObject SoruEkrani;
    [SerializeField] GameObject StartEkrani;

    [SerializeField] GameObject questionPanel;
    [SerializeField] GameObject a;
    [SerializeField] GameObject b;
    [SerializeField] GameObject c;
    [SerializeField] GameObject d;
    [SerializeField] GameObject startGameButton;
    [SerializeField] GameObject explosion;

    [SerializeField] TextMeshProUGUI questId;
    [SerializeField] TextMeshProUGUI GamePersonName;

    [SerializeField] TMP_InputField adSoyadInputText;

    [SerializeField] GameObject backgroundPanel;
    [SerializeField] Sprite inGameImage;
    private Sprite startScreenImage;
    float questIdNumber =1;

    Questions activeQuestion;
    Color defaultColor;

    List<int> selectionQuests = new List<int>();

    string startValueInput = string.Empty;

    public Animator anim;

    public ProgressSlider progressSlider;
    private void Start()
    {
        startValueInput = adSoyadInputText.text;
        startScreenImage = backgroundPanel.GetComponent<Image>().sprite;
        defaultColor = a.GetComponent<SVGImage>().color;
    }
    public void StartGame()
    {
        if (adSoyadInputText.text == startValueInput)
        {
            anim.SetTrigger("hover");
            //input alaný boþ ise birþeyler yapýlacak
        }
        else
        {
            //value sýfýrlama
            //adSoyadInputText.text = startValueInput;
            GamePersonName.text = adSoyadInputText.text;
            StartEkrani.SetActive(false);
            SoruEkrani.SetActive(true);

            startGameButton.SetActive(false);
            //Select Question
            SelectQuestion();
            progressSlider.SetScore(1);
            backgroundPanel.GetComponent<Image>().sprite = inGameImage;
        }
    }
    private void EndGame()
    {
        adSoyadInputText.text = startValueInput;
        GamePersonName.text = startValueInput;
        backgroundPanel.GetComponent<Image>().sprite = startScreenImage;
        SoruEkrani.SetActive(false);
        StartEkrani.SetActive(true);
        startGameButton.SetActive(true);

        selectionQuests.Clear();

    }

    private void SelectQuestion()
    {
        while (true)
        {
            int randomQuestionId = Random.Range(0, _questions.Count);
            if (!selectionQuests.Contains(randomQuestionId))
            {
                activeQuestion = _questions[randomQuestionId];
                questionPanel.GetComponentInChildren<TextMeshProUGUI>().text = _questions[randomQuestionId].quest;
                //seçili sorular
                selectionQuests.Add(randomQuestionId);
                a.GetComponentInChildren<TextMeshProUGUI>().text = _questions[randomQuestionId].answerOne;
                b.GetComponentInChildren<TextMeshProUGUI>().text = _questions[randomQuestionId].answerTwo;
                c.GetComponentInChildren<TextMeshProUGUI>().text = _questions[randomQuestionId].answerThree;
                d.GetComponentInChildren<TextMeshProUGUI>().text = _questions[randomQuestionId].answerFour;
                break;
            }
            else if (selectionQuests.Count == _questions.Count)
            {
                EndGame();
                break;
            }
        }
        
    }

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
            Invoke("NextQuestion", 2f);
        }
        else
        {
            a.GetComponent<SVGImage>().color = new Color(0.8f, 0f, 0f);
            Invoke("WrongAnswer", 2f);
        }
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
            Invoke("NextQuestion", 2f);
        }
        else
        {
            b.GetComponent<SVGImage>().color = new Color(0.8f, 0f, 0f);
            Invoke("WrongAnswer", 2f);
        }
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
            Invoke("NextQuestion", 2f);
        }
        else
        {
            c.GetComponent<SVGImage>().color = new Color(0.8f, 0f, 0f);
            Invoke("WrongAnswer", 2f);
        }
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
            Invoke("NextQuestion", 2f);

        }
        else
        {
            d.GetComponent<SVGImage>().color = new Color(0.8f, 0f, 0f);
            Invoke("WrongAnswer", 2f);
        }
    }

    private void NextQuestion()
    {
        progressSlider.SetScore(1);
        SelectQuestion();
        ButtonsActiveted();
        questIdNumber++;
        questId.text = questIdNumber.ToString();
    }
    private void WrongAnswer()
    { 
        ButtonsActiveted();
        EndGame();
        questIdNumber = 1;
    }

    private void ButtonsActiveted()
    {
        a.GetComponent<Button>().enabled = true;
        a.GetComponent<SVGImage>().color = defaultColor;
        b.GetComponent<Button>().enabled = true;
        b.GetComponent<SVGImage>().color = defaultColor;
        c.GetComponent<Button>().enabled = true;
        c.GetComponent<SVGImage>().color = defaultColor;
        d.GetComponent<Button>().enabled = true;
        d.GetComponent<SVGImage>().color = defaultColor;
    }
    private void ButtonsDeactiveted()
    {
        a.GetComponent<Button>().enabled = false;
        b.GetComponent<Button>().enabled = false;
        c.GetComponent<Button>().enabled = false;
        d.GetComponent<Button>().enabled = false;
    }
}
