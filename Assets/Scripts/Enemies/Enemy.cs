using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    [HideInInspector]
    public GameObject player;
    public int hitAmount = 5;

    // Start is called before the first frame update
    protected void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
    
    }

}
