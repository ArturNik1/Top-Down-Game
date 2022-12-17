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
    void Update()
    {
        Move();
    }

    private void Move()
    {
        healthBar.SetHealth(health);
        if (player == null) return;

        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
    }


}

