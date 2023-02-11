using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class WaveManagerScript : MonoBehaviour
{
    [Header("index is degree (degree = 2 is best)")]
    public float[] coeffSpawnAmount;
    public GameObject waveProgressBar;
    private Slider waveSlider;
    private TextMeshProUGUI enemiesText;
    private int amountForText = 0;

    public int startingWave = 0; // starting wave
    public float waveInterval = 10f; // time between waves
    public float spawnInterval = 0.5f; // time between enemy spawns
    private int currentWave;
    private int enemiesRemaining;
    private int spawnEnemiesRemaining;
    private float timer;
    private bool waveActive;
    private float spawnTimer;
    public GameObject enemySpawner;
    private EnemySpawn enemySpawn;
    private GameObject player;
    private PlayerStats playerStats;
    private UIManager uiManager;

    private bool paused = false;
    void Start()
    {
        waveSlider = waveProgressBar.GetComponent<Slider>();
        enemiesText = waveProgressBar.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        uiManager = GameObject.Find("UI").GetComponent<UIManager>();

        enemySpawn = enemySpawner.GetComponent<EnemySpawn>();
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
        currentWave = startingWave;
        timer = 0f;
        waveActive = false;
    }

    void Update()
    {
        if (paused) return;
        if (player == null) return;
        timer += Time.deltaTime;
        waveSlider.value = timer / waveInterval;
        if (!waveActive || timer >= waveInterval)
        {
            currentWave++; // increase wave number
            playerStats.wave = currentWave;

            waveActive = true;
            timer = 0f;

            int oldRemaining = enemiesRemaining;
            int newAmount = SpawnFunctionTransformation();
            enemiesRemaining += newAmount;
            spawnEnemiesRemaining += newAmount;
            waveInterval = WaveTimeFunctionTransformation(oldRemaining);
            waveSlider.value = 0f;

            spawnTimer = 0f;

            // pause game and open wave panel.
            if (currentWave > 1) uiManager.OpenWavePrizePanel();
        }
        if (waveActive)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval && spawnEnemiesRemaining > 0)
            {
                spawnTimer = 0f;
                enemySpawn.Spawn(currentWave);
                spawnEnemiesRemaining--;
                amountForText++;
                enemiesText.text = "Enemies: " + amountForText;
            }
        }
    }

    public void EnemyDied()
    {
        enemiesRemaining--;
        enemiesText.text = "Enemies: " + (--amountForText);
        if (enemiesRemaining <= 0)
        {
            waveActive = false;
            timer = 0;
        }
    }

    private int SpawnFunctionTransformation()
    {
        float sum = 0;
        float degree = 1;
        for (int i = 0; i < coeffSpawnAmount.Length; i++) {
            sum += coeffSpawnAmount[i] * degree;
            degree *= currentWave;
        }
        return Mathf.CeilToInt(sum);
    }

    private float WaveTimeFunctionTransformation(int amountOfEnemies)
    {
        return (amountOfEnemies * spawnInterval) + 10f;
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
