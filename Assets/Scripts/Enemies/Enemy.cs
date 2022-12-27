using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public float speed;
    [HideInInspector]
    public GameObject player;
    public float knockbackSpeed = 5.0f;
    private new Rigidbody2D rigidbody;
    private Vector2 hitDirection;
    public int hitAmount = 5;
    public HealthBar healthBar;

    private PlayerStats playerStats;

    // Start is called before the first frame update
    protected void Start()
    {
        health = maxHealth;
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
        healthBar.SetMaxHealth(maxHealth);
        rigidbody = GetComponent<Rigidbody2D>();
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
        if (health <= 0)
            Die();

           
    }

    public virtual void Die() {
        playerStats.enemiesKilled++;
        Destroy(gameObject);
    }

}
