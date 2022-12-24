using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float defaultTimer;
    private float timer;
    public GameObject[] enemies;
    public int enemiesCount = 0;
    public int wave = 1;
    List<GameObject> enemiesList;
    public List<Vector2> spawnPointsList;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        timer -= Time.deltaTime;
        if (timer <= 0 )
        {
            spawn();
            timer = defaultTimer;
        }
    }

    void spawn()
    {
        enemiesList = new List<GameObject>();
        foreach (GameObject enemy in enemies) { 
            enemiesList.Add(enemy);    
        }
        int count = 0;
        int randomSpawnpointIndex = (int)Random.Range(0f, (float)spawnPointsList.Count);
        Vector2 spawnPoint = spawnPointsList[randomSpawnpointIndex];
        if (spawnPointsList.Count == 0) return;
        Instantiate(enemiesList[Random.Range(0, enemiesList.Count)], spawnPoint, Quaternion.identity);
        count++;
    }

    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < spawnPointsList.Count; ++i) {
            Gizmos.DrawWireSphere(spawnPointsList[i], 0.5f);
        }

    }
}
