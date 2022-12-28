using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP_ItemDropRate : Item
{
    private EnemySpawn enemySpawn;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        enemySpawn = GameObject.Find("Enemy Spawner").GetComponent<EnemySpawn>();
    }

    public override void PickUpItem()
    {
        enemySpawn.dropRateIncreasePercent += 1f;
    }

}
