using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    // for achievmenets...
    public int killedBasic = 0;
    public int killedShooting = 0;
    public int killedCharging = 0;
    public int killedExploding = 0;

    public ComboBar comboBar;

    private bool paused = false;

    void Update()
    {
        if (paused) return;
        runTime += Time.deltaTime;   
    }

    public void CalculateScore()
    {
        score = Mathf.FloorToInt((runTime / 2f) + ((wave - 1) * 10) + powerUpsPicked);
        score *= highestMultiplier;
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


    public void UpdateAchievementsStats()
    {
        // do this and then do saving of stats.
        string filePath = Application.dataPath + "/Resources/achievements.json";
        string json = File.ReadAllText(filePath);
        Dictionary<string, Achievement> achievements = JsonConvert.DeserializeObject<Dictionary<string, Achievement>>(json);

        // update stats here...

        achievements["Kill Basic Enemies"].currentValue += killedBasic;
        achievements["Kill Shooting Enemies"].currentValue += killedShooting;
        achievements["Kill Charging Enemies"].currentValue += killedCharging;
        achievements["Kill Exploding Enemies"].currentValue += killedExploding;
        achievements["Kill Enemies"].currentValue += enemiesKilled;
        achievements["Survive"].currentValue += Mathf.FloorToInt(runTime);
        achievements["Pick Up Items"].currentValue += powerUpsPicked;
        achievements["Reach Score"].currentValue += score;


        ///

        string jsonNew = JsonConvert.SerializeObject(achievements, Formatting.Indented);
        File.WriteAllText(filePath, jsonNew);

    }


    private void Pause()
    {
        paused = true;
    }

    private void Unpause()
    {
        paused = false;
    }

    private void OnEnable()
    {
        UIManager.onPause += Pause;
        UIManager.onUnpause += Unpause;
    }

    private void OnDisable()
    {
        UIManager.onPause -= Pause;
        UIManager.onUnpause -= Unpause;
    }

}
