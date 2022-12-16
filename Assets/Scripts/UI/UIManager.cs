using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Animator closePanel;

    private PlayerStats playerStats;
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OpenGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        StartCoroutine(WriteText());
    }

    IEnumerator WriteText() {
        TextMeshProUGUI[] children = gameOverPanel.transform.GetComponentsInChildren<TextMeshProUGUI>(true);

        // Update text according to stats...
        // Wave
        children[1].text += playerStats.wave;
        // Run Time
        int minutes = Mathf.FloorToInt(playerStats.runTime / 60f);
        children[2].text += string.Format("{0:00}:{1:00}", minutes, Mathf.FloorToInt(playerStats.runTime - minutes * 60));
        // Damage Dealt
        children[3].text += playerStats.damageDealt;
        // Enemies Killed
        children[4].text += playerStats.enemiesKilled;
        // Power Ups
        children[5].text += playerStats.powerUpsPicked;

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
