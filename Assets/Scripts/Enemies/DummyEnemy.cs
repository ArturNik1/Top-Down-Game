using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : Enemy
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Move()
    {

    }

    public override void Die()
    {
        player.GetComponent<PlayerStats>().enemiesKilled++;
        GameObject.Find("Tutorial Manager").GetComponent<TutorialManager>().killedDummy = true;
        Destroy(gameObject);
    }
}
