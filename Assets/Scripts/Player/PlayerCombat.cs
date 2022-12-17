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
    public bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<Player_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking) {
            MeleeAttack();
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

    void MeleeAttack() {
        Collider2D[] hitEnemies= Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(5);
        }
    }

    void OnDrawGizmosSelected() {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }

}

