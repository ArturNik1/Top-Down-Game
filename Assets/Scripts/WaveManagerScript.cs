using UnityEngine;
using System.Collections;

public class WaveManagerScript : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // array of enemy prefabs
    public int startingWave = 1; // starting wave
    public float waveInterval = 10f; // time between waves
    public float spawnInterval = 1.5f; // time between enemy spawns
    private int currentWave;
    private int enemiesRemaining;
    private int spawnEnemiesRemaining;
    private float timer;
    private bool waveActive;
    private float spawnTimer;
    public GameObject enemySpawner;
    private EnemySpawn enemySpawn;
    public GameObject player;
    private PlayerStats playerStats;
    void Start()
    {
        enemySpawn = enemySpawner.GetComponent<EnemySpawn>();
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
        currentWave = startingWave;
        currentWave = startingWave;
        timer = 0f;
        waveActive = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (!waveActive && timer >= waveInterval)
        {
            waveActive = true;
            timer = 0f;
            enemiesRemaining = currentWave * 2; // number of enemies per wave
            spawnEnemiesRemaining = currentWave * 2;
            spawnTimer = 0f;
        }
        if (waveActive)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval && spawnEnemiesRemaining > 0)
            {
                spawnTimer = 0f;
                enemySpawn.Spawn();
                spawnEnemiesRemaining--;
            }
        }
    }

    public void EnemyDied()
    {
        enemiesRemaining--;
        if (enemiesRemaining <= 0)
        {
            currentWave++; // increase wave number
            playerStats.wave = currentWave;
            waveActive = false;
            timer = 0f;
        }
    }
}