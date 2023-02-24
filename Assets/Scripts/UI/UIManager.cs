using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{
    public GameObject wavePanel;
    
    public GameObject gameOverPanel;

    public GameObject wavePrizePanel;
    private bool dontShowWavePanel = false;

    public GameObject escapePanel;
    private Animator uiAnimator;
    private Slider musicSlider;
    private Slider soundSlider;
    private Sound backgroundSound;

    private PlayerStats playerStats;
    private LevelManager levelManager;

    public TextMeshProUGUI itemText;
    private int showItemText = 0;

    public RuntimeAnimatorController transitionController;
    public RuntimeAnimatorController gameOverPanelController;
    public RuntimeAnimatorController pausePanelController;
    public RuntimeAnimatorController wavePrizeController;

    public Button[] prizeButtons;
    public Button randomPrizeButton;

    private ItemManager itemManager;

    public static event Action onPause;
    public static event Action onUnpause;

    // Start is called before the first frame update
    void Start()
    {
        uiAnimator = GetComponent<Animator>();
        musicSlider = uiAnimator.transform.GetChild(2).GetChild(2).GetComponent<Slider>();
        soundSlider = uiAnimator.transform.GetChild(2).GetChild(4).GetComponent<Slider>();
        backgroundSound = AudioManager.instance.GetSound("background");

        musicSlider.value = backgroundSound.volume;
        soundSlider.value = AudioListener.volume;

        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        if (GameObject.Find("Item Spawn Manager") != null) itemManager = GameObject.Find("Item Spawn Manager").GetComponent<ItemManager>();
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

        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "SampleScene" && !gameOverPanel.activeSelf && !wavePrizePanel.activeSelf) {
            uiAnimator.runtimeAnimatorController = pausePanelController;
            
            if (escapePanel.activeSelf) {
                uiAnimator.SetTrigger("close");
            }
            else {
                uiAnimator.SetTrigger("open");
            }
        }

    }

    public void ChangeEscapePanelState() {
        escapePanel.SetActive(!escapePanel.activeSelf);
        if (escapePanel.activeSelf) onPause.Invoke();
        else onUnpause.Invoke();
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
        uiAnimator.runtimeAnimatorController = gameOverPanelController;
        uiAnimator.SetTrigger("open");
        StartCoroutine(WriteText());
    }

    IEnumerator WriteText() {
        TextMeshProUGUI[] children = gameOverPanel.transform.GetComponentsInChildren<TextMeshProUGUI>(true);
        playerStats.CalculateScore();
        playerStats.UpdateAchievementsStats();

        // Update text according to stats...
        // Score
        children[1].text += (playerStats.score / playerStats.highestMultiplier) + " x " + playerStats.highestMultiplier + " = " + playerStats.score;
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

    public void ChangeGameOverPanelState() {
        gameOverPanel.SetActive(!gameOverPanel.activeSelf);
    }

    #endregion

    #region Wave Prize Panel Stuff

    public void OpenWavePrizePanel() {
        if (dontShowWavePanel) {
            StartCoroutine(GetRandomPrize());
            return;
        }
        PopulateButtons();
        onPause.Invoke();
        uiAnimator.runtimeAnimatorController = wavePrizeController;
        uiAnimator.SetTrigger("open");
    }

    public void CloseWavePrizePanel() {
        onUnpause.Invoke();
        uiAnimator.runtimeAnimatorController = wavePrizeController;
        uiAnimator.SetTrigger("close");
    }


    IEnumerator GetRandomPrize() {
        Item[] items = itemManager.GetThreeItemsForPrizePanel();

        yield return new WaitForEndOfFrame();

        items[0].PickUpItem();
        ShowOnScreenItemText(items[0].pickUPText);
        foreach (Item curItem in items) {
            Destroy(curItem.gameObject);
        }
    }

    

    public void ChangeWavePrizePanelState() {
        wavePrizePanel.SetActive(!wavePrizePanel.activeSelf);
    }

    public void PopulateButtons() {

        foreach (Button button in prizeButtons) {
            button.interactable = true;
        }

        randomPrizeButton.onClick.RemoveAllListeners();

        // Choose 3 random power ups and add them
        Item[] items = itemManager.GetThreeItemsForPrizePanel();
        for (int i = 0; i < items.Length; i++) {
            Item item = items[i];
            prizeButtons[i].onClick.RemoveAllListeners();
            prizeButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.pickUPText;
            prizeButtons[i].onClick.AddListener(() =>
            {
                foreach (Button button in prizeButtons) {
                    button.interactable = false;
                }

                item.PickUpItem();
                ShowOnScreenItemText(item.pickUPText);
                foreach (Item curItem in items) {
                    Destroy(curItem.gameObject);
                }

                CloseWavePrizePanel();
            });
        }
        randomPrizeButton.onClick.AddListener(() =>
        {
            dontShowWavePanel = true;
            int r = UnityEngine.Random.Range(0, 2);
            items[r].PickUpItem();
            ShowOnScreenItemText(items[r].pickUPText);
            foreach (Item curItem in items)
            {
                Destroy(curItem.gameObject);
            }
            CloseWavePrizePanel();
        });
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
        uiAnimator.runtimeAnimatorController = transitionController;
        uiAnimator.SetTrigger("close"); 
        levelManager.LoadLevelWrapper("SampleScene");
    }

    public void ReturnToMenu() {
        uiAnimator.runtimeAnimatorController = transitionController;
        uiAnimator.SetTrigger("close");
        levelManager.LoadLevelWrapper("Menu");
    }

}
