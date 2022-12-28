using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP_MaxHealth : Item
{
    private PlayerInfo playerInfo;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
    }
    public override void PickUpItem()
    {
        playerInfo.health += 2;
        playerInfo.maxHealth += 2;
        playerInfo.healthBar.SetMaxHealth(playerInfo.maxHealth);
        playerInfo.healthBar.SetHealth(playerInfo.health);
    }
}
