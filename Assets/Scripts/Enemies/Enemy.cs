using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    [HideInInspector]
    public GameObject player;
    public float knockbackSpeed = 5.0f;
    private new Rigidbody2D rigidbody;
    private Vector2 hitDirection;
    public int hitAmount = 5;
    public HealthBar healthBar;
    // Start is called before the first frame update
    protected void Start()
    {
        player = GameObject.Find("Player");
        healthBar.SetMaxHealth(health);
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void TakeDamage(int damage) {
        health -= damage;
        hitDirection = transform.position - player.transform.position;
        hitDirection.Normalize();
        rigidbody.velocity = hitDirection * knockbackSpeed;

        //play hurt animation
        if (health <= 0)
            Die();

           
    }

    void Die() {
        Destroy(gameObject);
    }

}
