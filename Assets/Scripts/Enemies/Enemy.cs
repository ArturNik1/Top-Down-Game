using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [HideInInspector]
    public GameObject player;
    private new Rigidbody2D rigidbody;
    private Vector2 hitDirection;
    private PlayerStats playerStats;
#nullable enable
    private ItemManager? itemManager= null;
#nullable disable

    [Header("Health Settings")]
    public int maxHealth;
    public int health;
    public HealthBar healthBar;

    [Header("Speed Settings")]
    public float speed;
    public float knockbackSpeed = 5.0f;

    [Header("Attack Settings")]
    public int hitAmount = 5;

    [Header("Item Settings")]
    [Range(0.0f, 100f)]
    public float itemDropRatePercent = 10f;
    [Header("Distribution Between Weapons And PowerUps\n(Example: 70 is 70% power up, 30 % weapon)")]
    [Range(0.0f, 100f)]
    public float distributionRate = 50f;

    // Start is called before the first frame update
    protected void Start()
    {
        health = maxHealth;
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
        healthBar.SetMaxHealth(maxHealth);
        rigidbody = GetComponent<Rigidbody2D>();

        GameObject itemManagerGameobject = GameObject.Find("Item Spawn Manager");
        if (itemManagerGameobject != null) itemManager = itemManagerGameobject.GetComponent<ItemManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void TakeDamage(int damage) {
        health -= damage;
        playerStats.damageDealt += damage;
        hitDirection = transform.position - player.transform.position;
        hitDirection.Normalize();
        rigidbody.velocity = hitDirection * knockbackSpeed;

        //play hurt animation
        if (health <= 0) {
            DropItemByChance();
            Die();
        }

           
    }

    public virtual void DropItemByChance() {
        if (itemManager == null) return;

        float dropResult = UnityEngine.Random.Range(0f, 100f);
        if (dropResult <= itemDropRatePercent) {
            float typeResult = UnityEngine.Random.Range(0f, 100f);
            if (typeResult <= distributionRate) itemManager.SpawnWeapon(transform.position);
            else itemManager.SpawnPowerUp(transform.position);
        }
    }

    public virtual void Die() {
        playerStats.enemiesKilled++;
        Destroy(gameObject);
    }

}
