using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform left_attack_point;
    public Transform right_attack_point;
    private Transform attack_point;
    public float attackRange = 0.3f;
    public LayerMask enemyLayers;
    private Player_Movement playerMovement;
    public float maxAttackTime = 0.5f;
    [HideInInspector]
    public float currentAttackTime = 0;
    [HideInInspector]
    public bool isAttacking = false;
    public int attackDamage = 5;
    public Animator animator;
    private PlayerStats playerStats;
    private int attack_animation = 1;
    private int total_attack_animations = 3;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<Player_Movement>();
        playerStats = GetComponent<PlayerStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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

        TriggerAttackAnimation();
        if (spriteRenderer.flipX)
        {
            attack_point = left_attack_point;
        }
        else{
            attack_point = right_attack_point;
        }

        Collider2D[] hitEnemies= Physics2D.OverlapCircleAll(attack_point.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
        return hitEnemies.Length > 0;
    }

    void TriggerAttackAnimation() {
        if (attack_animation == 1) {
            animator.ResetTrigger("MeleeAttack1");
            animator.SetTrigger("MeleeAttack1");
        }
        else if (attack_animation == 2)
        {
            animator.ResetTrigger("MeleeAttack2");
            animator.SetTrigger("MeleeAttack2");
        }
        else if (attack_animation == 3)
        {
            animator.ResetTrigger("MeleeAttack3");
            animator.SetTrigger("MeleeAttack3");
        }
        
        attack_animation++;

        if (attack_animation > total_attack_animations) {
            attack_animation = 1;
        }

    }

    void OnDrawGizmosSelected() {
        if (attack_point == null)
            return;
        Gizmos.DrawWireSphere(attack_point.position, attackRange);
    }

    IEnumerator Delay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        // Code to execute after the delay
    }

}

