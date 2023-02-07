using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject wavePanel;
    
    public GameObject gameOverPanel;

    public GameObject escapePanel;
    private Animator closePanel;
    private Slider musicSlider;
    private Slider soundSlider;
    private Sound backgroundSound;

    private PlayerStats playerStats;
    private LevelManager levelManager;

    public TextMeshProUGUI itemText;
    private int showItemText = 0;

    // Start is called before the first frame update
    void Start()
    {
        closePanel = GetComponent<Animator>();
        musicSlider = closePanel.transform.GetChild(2).GetChild(2).GetComponent<Slider>();
        soundSlider = closePanel.transform.GetChild(2).GetChild(4).GetComponent<Slider>();
        backgroundSound = AudioManager.instance.GetSound("background");

        musicSlider.value = backgroundSound.volume;
        soundSlider.value = AudioListener.volume;

        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }

    private void Update()
    {
        if (showItemText == 1) {
            itemText.alpha = Mathf.MoveTowards(itemText.alpha, 1.0f, 0.75f * Time.deltaTime);
            if (itemText.alpha == 1) showItemText = 2;
        }
        else if (showItemText == 2) {
            itemText.alpha = Mathf.MoveTowards(itemText.alpha, 0f, 0.5f * Time.deltaTime);
            if (itemText.alpha == 0) showItemText = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "SampleScene") {
            escapePanel.SetActive(!escapePanel.activeSelf);
        }

    }

    public void ChangeMusicSlider() {
        backgroundSound.volume = musicSlider.value;
        backgroundSound.source.volume = backgroundSound.volume;
    }

    public void ChangeSoundSlider() {
        AudioListener.volume = soundSlider.value;
    }

    #region Item Text
    public void ShowOnScreenItemText(string text) {
        showItemText = 1;
        itemText.text = text;
        itemText.alpha = 0f;
    }
    #endregion

    #region Game Over Stuff
    private void OpenGameOverPanel()
    {
        wavePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        StartCoroutine(WriteText());
    }

    IEnumerator WriteText() {
        TextMeshProUGUI[] children = gameOverPanel.transform.GetComponentsInChildren<TextMeshProUGUI>(true);
        playerStats.CalculateScore();

        // Update text according to stats...
        // Score
        children[1].text += playerStats.score + " x " + playerStats.highestMultiplier + " = " + (playerStats.score * playerStats.highestMultiplier);
        // Wave
        children[2].text += playerStats.wave;
        // Run Time
        int minutes = Mathf.FloorToInt(playerStats.runTime / 60f);
        children[3].text += string.Format("{0:00}:{1:00}", minutes, Mathf.FloorToInt(playerStats.runTime - minutes * 60));
        // Damage Dealt
        children[4].text += playerStats.damageDealt;
        // Enemies Killed
        children[5].text += playerStats.enemiesKilled;
        // Power Ups
        children[6].text += playerStats.powerUpsPicked;

        // Write text on screen...
        for (int i = 0; i < children.Length - 2; i++) {
            TextMeshProUGUI text = children[i];
            string targetText = text.text;
            text.text = "";
            children[i].gameObject.SetActive(true);
            for (int j = 0; j < targetText.Length; j++) {
                text.text = text.text + targetText[j];
                yield return new WaitForSeconds(0.05f);
            }
        }

        yield return null;
    }
    #endregion

    private void OnEnable()
    {
        PlayerInfo.onDeath += OpenGameOverPanel;
    }

    private void OnDisable()
    {
        PlayerInfo.onDeath -= OpenGameOverPanel;   
    }

    public void RestartRun() {
        closePanel.SetTrigger("close"); 
        levelManager.LoadLevelWrapper("SampleScene");
    }

    public void ReturnToMenu() {
        closePanel.SetTrigger("close");
        levelManager.LoadLevelWrapper("Menu");
    }

}
