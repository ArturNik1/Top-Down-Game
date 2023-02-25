using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP_HealthPack : Item
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
        playerInfo.health += Mathf.CeilToInt(playerInfo.maxHealth * 0.1f);
        if (playerInfo.health > playerInfo.maxHealth) playerInfo.health = (int)playerInfo.maxHealth;
        playerInfo.healthBar.SetHealth(playerInfo.health);
    }
}
