using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [HideInInspector]
    public bool isHit = false;
    public float maxHitTime = 0.5f;
    [HideInInspector]
    public float currentHitTime = 0;
    public float hitSpeed = 3;

    private Vector2 hitDirection;
    private PlayerInfo playerInfo;

    // Start is called before the first frame update
    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHitTime += Time.deltaTime;
        if (currentHitTime >= maxHitTime) {
            isHit = false;
            currentHitTime = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isHit) return;

        if (collision.transform.tag == "Enemy") {
            // push back
            isHit = true;
            AudioManager.instance.Play("hit1");
            hitDirection = transform.position - collision.transform.position;
            hitDirection.Normalize();
            currentHitTime = 0;
            playerInfo.reduceHealth(collision.transform.GetComponent<Enemy>().hitAmount);
        }
    }

    public Vector2 getHitDirection()
    {
        return hitDirection * hitSpeed;
    }

}
