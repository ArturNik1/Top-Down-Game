using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP_Speed : Item
{
    private Player_Movement playerMovement;

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<Player_Movement>();
    }

    public override void PickUpItem()
    {
        playerMovement.speed *= 1.1f;
    }
}
