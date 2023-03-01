using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour
{
    public Achievement achievement;

    private Button button;
    private AchievementHandler achievementHandler;
    
    private Image image;
    private Color readyColor;
    private Color inProgressColor;
    private Color doneColor;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        readyColor = new Color(0, 1, 0, 60f / 255f);
        inProgressColor = new Color(1, 1, 1, 60f / 255f);
        doneColor = new Color(0, 0.5f, 1, 60f / 255f);

        achievementHandler = GameObject.Find("Achievement Manager").GetComponent<AchievementHandler>();
        button.onClick.AddListener(() =>
        {
            achievement.currentStar++;
            UpdateButton(achievement);
            achievementHandler.WriteDataToFile();

            // add to coins
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0) + (10 * achievement.currentStar));
            GameObject.Find("Canvas").GetComponent<MainMenuManager>().coinsText.text = "Coins: " + PlayerPrefs.GetInt("coins", 0);

        });
    }

    // Update is called once per frame
    void Update()
    {
        // check for flag.
        // if true -> set interactable to true
        // otherwise -> set to false
        // if clicked then -> give reward, change to not interactable and update text
        int index = achievement.currentStar < achievement.targets.Length ? achievement.currentStar : achievement.targets.Length - 1;
        if (achievement.currentValue < achievement.targets[index]) {
            button.interactable = false;
            image.color = inProgressColor;
        }
        else if (achievement.currentStar >= achievement.targets.Length) {
            button.interactable = false;
            image.color = doneColor;
        }
        else if (achievement.currentValue >= achievement.targets[index]) {
            button.interactable = true;
            image.color = readyColor;
        }
    }

    public void UpdateButton(Achievement achievement) {
        this.achievement = achievement;

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = achievement.achievementName;

        if (achievement.currentStar < achievement.targets.Length) { 
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = achievement.currentValue + "/" + achievement.targets[achievement.currentStar];
        }
        else {
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = achievement.currentValue.ToString();
        }

        int stars = achievement.currentStar <= achievement.targets.Length ? achievement.currentStar : achievement.targets.Length;
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Star: " + stars + "/" + achievement.targets.Length;

    }

}
