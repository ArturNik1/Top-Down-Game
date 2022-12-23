using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float defaultTimer;
    private float timer;
    public GameObject enemy;
    public int enemiesCount = 0;
    public int wave = 1;
    List<GameObject> enemiesList;
    public List<Vector2> spwanPointsList;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 )
        {
            spawn();
            timer = defaultTimer;
        }
    }

    void spawn()
    {
        List<GameObject> enemiesList = new List<GameObject>();
        enemiesList.Add(enemy);    
        int count = 0;
        int randomSpawnpointIndex = (int)Random.Range(0f, (float)spwanPointsList.Count);
        Vector2 spawnPoint = spwanPointsList[randomSpawnpointIndex];
        Instantiate(enemiesList[Random.Range(0, enemiesList.Count)], spawnPoint, Quaternion.identity);
        count++;
    }

    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < spwanPointsList.Count; ++i) {
            Gizmos.DrawWireSphere(spwanPointsList[i], 0.5f);

        }

    }
}
