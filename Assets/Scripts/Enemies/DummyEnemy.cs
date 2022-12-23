using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(health);
    }

    public override void Die()
    {
        GameObject.Find("Tutorial Manager").GetComponent<TutorialManager>().killedDummy = true;
        base.Die();
    }
}
