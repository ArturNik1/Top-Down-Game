using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int wave = 0;
    public float runTime = 0f;
    public int damageDealt = 0;
    public int enemiesKilled = 0;
    public int powerUpsPicked = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        runTime += Time.deltaTime;   
    }
}
