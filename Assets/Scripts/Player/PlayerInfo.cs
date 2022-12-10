using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField]
    private int health = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) Die();
    }

    public void reduceHealth(int amount)
    {
        health -= amount;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
