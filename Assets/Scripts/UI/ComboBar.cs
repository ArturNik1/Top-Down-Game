using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ComboBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public TextMeshProUGUI currentMultiplierText;
    public TextMeshProUGUI highestMultiplierText;

    public void SetCombo(int combo)
    {
        slider.value = combo % 10;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void UpdateText(int multiplier)
    {
        currentMultiplierText.text = multiplier.ToString();
    }
    public void UpdateHighestText(int multiplier)
    {
        highestMultiplierText.text = "Multiplier: " + multiplier;
    }
}
