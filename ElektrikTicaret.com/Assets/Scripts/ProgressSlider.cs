using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UI;

public class ProgressSlider : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public SVGImage fill;
    public Text sayacText;
    int maxTime = 10;
    int sayac = 0;
    float timer = 0f;
    float countdownDuration = 1f; // Saniye
    public bool isGoing = true; // Eðer soru arasýnda ise false olacak ve sayaç duracak
    private void Start()
    {
        slider.maxValue = maxTime;
        sayac = Convert.ToInt32(slider.maxValue);
        sayacText.text = "00:" + (sayac < 10 ? "0" + sayac.ToString() : sayac.ToString()); // sayac 10 dan küçükse otomatik olarak 0 atacak önüne
        fill.color = gradient.Evaluate(1f);
    }
    public void SetScore(float second)
    {
        slider.value -= second;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    public void ResetScore()
    {
        slider.value = maxTime;
        sayac = maxTime;
    }
    public int questTime 
    { 
        set 
        {
            slider.value = value;
            maxTime = value;
            slider.maxValue = value;
            ResetScore();
        } 
    }
    private void Update()
    {
        if (sayac > 0 && isGoing)
        {
            SetScore(Time.deltaTime);
            timer += Time.deltaTime;
            if (timer >= countdownDuration)
            {
                sayac -= 1;
                sayacText.text = "00:" + (sayac < 10 ? "0" + sayac.ToString() : sayac.ToString()); // sayac 10 dan küçükse otomatik olarak 0 atacak önüne
                timer = 0f;
            }
        }
    }
}
