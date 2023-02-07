using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int wave = 0;
    public float runTime = 0f;
    public int damageDealt = 0;
    public int enemiesKilled = 0;
    public int powerUpsPicked = 0;

    public int combo = 0;
    public int highestMultiplier = 1;
    public int multiplier = 1;
    public int score = 0;

    public ComboBar comboBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        runTime += Time.deltaTime;   
    }

    public void CalculateScore()
    {
        score = Mathf.FloorToInt((runTime / 2f) + ((wave - 1) * 10) + powerUpsPicked);
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0) + score);
        if (PlayerPrefs.GetInt("score", -1) < score) PlayerPrefs.SetInt("score", score);
    }

    public void ResetComboBar()
    {
        combo = 0;
        highestMultiplier = highestMultiplier < multiplier ? multiplier : highestMultiplier;
        multiplier = 1;
        comboBar.UpdateText(1);
        comboBar.SetCombo(0);
    }

    public void UpdateCombo()
    {
        combo++;
        comboBar.SetCombo(combo);
        // combo % 10 == 0 -> 0%, == 1 -> 10%, ... == 9 -> 90%
        if (combo % 10 == 0)
        {
            multiplier++;
            comboBar.UpdateText(multiplier);
            if (multiplier > highestMultiplier) comboBar.UpdateHighestText(multiplier);
        }
    }
}
