using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressSlider : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public int MaxScore = 10;
    private void Start()
    {
        slider.maxValue = MaxScore;
        fill.color = gradient.Evaluate(1f);
    }
    public void SetScore(int score)
    {
        slider.value += score;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
