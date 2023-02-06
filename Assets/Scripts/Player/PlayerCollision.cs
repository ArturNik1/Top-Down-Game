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
    private PlayerStats playerStats;
    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
        playerStats = GetComponent<PlayerStats>();
        uiManager = GameObject.Find("UI").GetComponent<UIManager>();
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

        if (collision.transform.tag == "EnemyBullet")
        {
            // push back
            isHit = true;
            AudioManager.instance.Play("hit1");
            hitDirection = transform.position - collision.transform.position;
            hitDirection.Normalize();
            currentHitTime = 0;
            playerInfo.reduceHealth(collision.transform.GetComponent<EnemyBulletMovement>().hitAmount);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Item")
        {
            Item item = collision.transform.GetComponent<Item>();
            item.PickUpItem();
            if (item.type == ItemType.PowerUp) {
                playerStats.powerUpsPicked++;
                uiManager.ShowOnScreenItemText(item.pickUPText);
            }
            Destroy(collision.gameObject);
        }
        else if (collision.transform.tag == "Goal") {
            GameObject.Find("Tutorial Manager").GetComponent<TutorialManager>().reachedGoal = true;
        }
        else if (collision.transform.tag == "Exit") {
            GameObject.Find("Tutorial Manager").GetComponent<TutorialManager>().canExit = true;
        }
    }

    public Vector2 getHitDirection()
    {
        return hitDirection * hitSpeed;
    }

    public void HitByExplosion(ExplodingEnemy explodingEnemy) {
        isHit = true;
        AudioManager.instance.Play("hit1");
        currentHitTime = 0;
        playerInfo.reduceHealth(explodingEnemy.hitAmount);

    }

}
