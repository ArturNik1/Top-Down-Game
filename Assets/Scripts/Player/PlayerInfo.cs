using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField]
    private int health = 20;
    public HealthBar healthBar;
    public static event Action onDeath;

    // Start is called before the first frame update
    void Start()
    {
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
