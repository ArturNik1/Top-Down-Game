using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public Animator settingsAnimator;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI coinsText;

    public Slider musicSlider;
    public Slider soundSlider;
    private Sound backgroundSound;

    private void Start()
    {
        backgroundSound = AudioManager.instance.GetSound("background");
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("score", 0);
        coinsText.text = "Coins: " + PlayerPrefs.GetInt("coins", 0);
    }

    public void ChangeMusicSlider()
    {
        backgroundSound.volume = musicSlider.value;
        backgroundSound.source.volume = backgroundSound.volume;
    }

    public void ChangeSoundSlider()
    {
        AudioListener.volume = soundSlider.value;
    }

    public void EnableTriggerOpen()
    {
        settingsAnimator.SetTrigger("open");
    }


    public void EnableTriggerClose() {
        settingsAnimator.SetTrigger("close");
    }

    public void ChangeSettingsPanelState() {
        settingsPanel.SetActive(!settingsPanel.activeSelf);   
    }
}
