using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float defaultTimer;
    private float timer;
    public GameObject[] enemies;
    public int enemiesCount = 0;
    [HideInInspector]
    public List<GameObject> enemiesList;
    public List<Vector2> spawnPointsList;

    [HideInInspector]
    public float dropRateIncreasePercent = 0f;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemiesList = new List<GameObject>();
        foreach (GameObject enemy in enemies)
        {
            enemiesList.Add(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        timer -= Time.deltaTime;
        if (timer <= 0 )
        {
           // Spawn();
            timer = defaultTimer;
        }
    }

    public void Spawn(int currentWave)
    {
        int randomSpawnpointIndex = (int)Random.Range(0f, (float)spawnPointsList.Count);
        Vector2 spawnPoint = spawnPointsList[randomSpawnpointIndex];
        while (Vector2.Distance(spawnPoint, player.transform.position) < 3f) {
            randomSpawnpointIndex = (int)Random.Range(0f, (float)spawnPointsList.Count);
            spawnPoint = spawnPointsList[randomSpawnpointIndex];
        }
        if (spawnPointsList.Count == 0) return;
        int upTo = (currentWave == 1) ? 1 : (currentWave <= 3) ? 2 : (currentWave <= 6) ? 3 : enemiesList.Count;  
        GameObject enemyObject = Instantiate(enemiesList[Random.Range(0, upTo)], spawnPoint, Quaternion.identity);
        enemyObject.GetComponent<Enemy>().UpdateStats(currentWave);
    }

    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < spawnPointsList.Count; ++i) {
            Gizmos.DrawWireSphere(spawnPointsList[i], 0.5f);
        }

    }
}
