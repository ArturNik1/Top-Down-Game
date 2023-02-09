using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;

public class AchievementHandler : MonoBehaviour
{
    string filePath;
    Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();

    public bool resetFile = false;
    [Header("if adding/changing, remember to do it in PlayerStats, function UpdateAchievementsStats() too")]
    public List<Achievement> achievementSetup;

    public GameObject achievementButton;
    public GameObject contentGameObject;


    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.dataPath + "/Resources/achievements.json";

        if (resetFile && File.Exists(filePath)) File.Delete(filePath);

        ReadDataFromInspector();
        ReadDataFromFile();
        UpdateButtons();
    }

    public void AddObject(Achievement achievement)
    {
        if (achievements.ContainsKey(achievement.achievementName))
        {
            achievements.Remove(achievement.achievementName);
        }
        achievements.Add(achievement.achievementName, achievement);
    }
    public void WriteDataToFile() {
        string json = JsonConvert.SerializeObject(achievements, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public void ReadDataFromFile() {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            Dictionary<string, Achievement> temp = JsonConvert.DeserializeObject<Dictionary<string, Achievement>>(json);
            foreach (Achievement ach in temp.Values) {
                if (achievements.ContainsKey(ach.achievementName)) {
                    achievements[ach.achievementName].currentValue = temp[ach.achievementName].currentValue;
                    achievements[ach.achievementName].currentStar = temp[ach.achievementName].currentStar;
                }
                else {
                    achievements.Add(ach.achievementName, ach);
                }
            }
        }
        WriteDataToFile();
    }

    public void ReadDataFromInspector() { 
        foreach (Achievement achievement in achievementSetup) {
            AddObject(achievement);
        }
    }

    public void UpdateButtons() {
        bool create = true;
        if (contentGameObject.transform.childCount > 0) create = false;
        int i = 0;
        foreach (Achievement achievement in achievements.Values) {
            GameObject buttonObject;
            if (create) buttonObject = Instantiate(achievementButton, contentGameObject.transform);
            else {
                buttonObject = contentGameObject.transform.GetChild(i).gameObject;
                i++;
            }
            AchievementButton achievementUI = buttonObject.GetComponent<AchievementButton>();
            achievementUI.achievement = achievement;
            achievementUI.UpdateButton(achievement);
        }
    }

}

[System.Serializable]
public class Achievement
{
    public string achievementName;
    public int currentValue;
    public int currentStar;
    public int[] targets;

    public Achievement(string name, int value, int star, int[] targets) {
        this.achievementName = name;
        this.currentValue = value;
        this.currentStar = star;
        this.targets = targets;
    }
}
