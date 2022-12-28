using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP_AttackRange : Item
{
    private PlayerCombat playerCombat;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
    }

    public override void PickUpItem()
    {
        playerCombat.attackRange += 0.1f;
    }

}
