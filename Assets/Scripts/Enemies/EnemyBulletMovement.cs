using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{

    public Vector3 bulletDirection;
    private GameObject player;
    public float bulletSpeed;
    public int hitAmount;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        if (!player)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 rand = new Vector3(Random.value - 0.5f, Random.value - 0.5f, 0);
        bulletDirection = player.transform.position - transform.position + rand;
        float angle = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;
        bulletDirection.Normalize();

        transform.rotation = Quaternion.AngleAxis(angle + 45, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (bulletDirection * Time.deltaTime * bulletSpeed);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Enviroment" && other.gameObject.tag != "Player")
        {
            Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), other.transform.GetComponent<Collider2D>());
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
