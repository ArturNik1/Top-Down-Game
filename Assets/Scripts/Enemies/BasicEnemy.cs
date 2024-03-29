using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{



    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (paused) return;
        base.Update();
        Move();


    }

    public override void Move()
    {
        if (player == null) return;

        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);

    }

    public override void Die()
    {
        playerStats.killedBasic++;
        base.Die();
    }

}

