using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    private Player_Movement playerMovement;
    public float maxAttackTime = 0.5f;
    [HideInInspector]
    public float currentAttackTime = 0;
    [HideInInspector]
    public bool isAttacking = false;
    public int attackDamage = 5;

    private PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<Player_Movement>();
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.paused) return;
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking) {
            if (MeleeAttack())
            {
                // if hit something, update combo
                playerStats.UpdateCombo();
            }
            //playerMovement.MeleeAttackMovement();
            isAttacking = true;
        }

        currentAttackTime += Time.deltaTime;
        if (currentAttackTime >= maxAttackTime)
        {
            isAttacking = false;
            currentAttackTime = 0;
        }
    }

    bool MeleeAttack() {
        Collider2D[] hitEnemies= Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
        return hitEnemies.Length > 0;
    }

    void OnDrawGizmosSelected() {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }

}

