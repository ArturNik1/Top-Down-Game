using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int health = 20;
    public HealthBar healthBar;
    [HideInInspector]
    public float maxHealth;
    public static event Action onDeath;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) Die();
    }

    public void reduceHealth(int amount)
    {
        health -= amount;
        healthBar.SetHealth(health);
    }

    private void Die()
    {
        onDeath?.Invoke();
        Destroy(gameObject);
    }
}
