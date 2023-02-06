using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemy : Enemy
{
    public float explodeTime = 3f;
    private float currentExplodeTime;

    public float explodeRadius = 1;

    private bool exploded = false;

    public LayerMask layerMask;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (exploded) return;

        currentExplodeTime += Time.deltaTime;

        if (currentExplodeTime >= explodeTime) {

            // explode in a radius...
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explodeRadius, layerMask);
            // deal damage if in radius
            foreach (Collider2D col in colliders) {
                if (col.name != "Player") return;
                PlayerCollision playerCollision = col.GetComponent<PlayerCollision>();
                playerCollision.HitByExplosion(this);
            }
            // do effect
            exploded = true;
            healthBar.gameObject.SetActive(false);
            GetComponent<ParticleSystem>().Play();
            
            // dies when particle effect is done.
        }

        Move();
    }

    public override void Move()
    {
        if (player == null) return;

        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }

    private void OnParticleSystemStopped()
    {
        DropItemByChance();
        Die();
    }

}
