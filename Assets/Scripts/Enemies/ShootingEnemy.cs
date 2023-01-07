using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    public GameObject bullet;
    public float defaultShootTime;
    private float shootTime;

    // Start is called before the first frame update
    new void Start()
    {
        shootTime = defaultShootTime;
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        Move();
        
        shootTime -= Time.deltaTime;
        if (shootTime <= 0)
        {
            shootTime = defaultShootTime;
            Vector3 bulletPos = transform.position;
            GameObject bullet_obj = Instantiate(bullet, bulletPos, Quaternion.identity);
            Physics2D.IgnoreCollision(bullet_obj.transform.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

    }

    private void Shoot()
    {
        if (player == null) return;
        Vector3 bulletPos = transform.position;
        GameObject bullet_obj = Instantiate(bullet, bulletPos, Quaternion.identity);
        Physics2D.IgnoreCollision(bullet_obj.transform.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    public override void Move()
    {
        if (player == null) return;

        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
    }

}
