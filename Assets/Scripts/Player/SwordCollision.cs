using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    public PlayerCombat playerCombat;

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.tag == "Enemy") {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (!enemy.beingAttacked) enemy.TakeDamage(playerCombat.attackDamage);
        }
    }
}
