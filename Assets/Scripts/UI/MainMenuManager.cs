using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public Animator settingsAnimator;

    public Slider musicSlider;
    public Slider soundSlider;
    private Sound backgroundSound;

    private void Start()
    {
        backgroundSound = AudioManager.instance.GetSound("background");
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
