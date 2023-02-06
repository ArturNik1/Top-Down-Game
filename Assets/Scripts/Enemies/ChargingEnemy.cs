using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChargingEnemyState { Walking, ColorChanging, Charging, Recovering };

public class ChargingEnemy : Enemy
{
    [Header("Range should be somewhere between [0, inf]")]
    public Vector2 walkingTimeRange;
    private float walkingTime;
    private float currentWalkingTime = 0;

    public float chargeSpeed = 7.5f;
    public float chargeForwardTime;
    private float currentChargeForwardTime = 0;
    private Vector2 chargeDirection;

    private SpriteRenderer spriteRenderer;

    private ChargingEnemyState state = ChargingEnemyState.Walking;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        walkingTime = Random.Range(walkingTimeRange.x, walkingTimeRange.y);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        Move();
    }

    public override void Move()
    {
        // walk for random time between range [a,b]
        // change color for x seconds
        // after color is done, charge for y seconds in a straight line
        // stop in place when charge is done, change color back.
        // resume walking (1) when color is done.
        if (state == ChargingEnemyState.Walking) {
            if (currentWalkingTime >= walkingTime) {
                currentWalkingTime = 0;
                walkingTime = Random.Range(walkingTimeRange.x, walkingTimeRange.y);
                state = ChargingEnemyState.ColorChanging;
            }
            else {
                // walk
                if (player == null) return;
                currentWalkingTime += Time.deltaTime;
                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
            }
        }
        else if (state == ChargingEnemyState.ColorChanging) { 
            if (spriteRenderer.color.g <= 125/255f) {
                state = ChargingEnemyState.Charging;
                if (player == null) return;
                chargeDirection = -(transform.position - player.transform.position);
                chargeDirection.Normalize();
            }
            else {
                // change color and do not move
                // green 220 to 120
                float newGreen = Mathf.MoveTowards(spriteRenderer.color.g, 120/255f, 0.2f * Time.deltaTime);
                spriteRenderer.color = new Color(spriteRenderer.color.r, newGreen, spriteRenderer.color.b);
            }
        }
        else if (state == ChargingEnemyState.Charging) { 
            if (currentChargeForwardTime >= chargeForwardTime) {
                currentChargeForwardTime = 0;
                state = ChargingEnemyState.Recovering;
            }
            else {
                // charge forward
                currentChargeForwardTime += Time.deltaTime;
                rigidbody.velocity = chargeDirection * chargeSpeed;
            }
        }
        else if (state == ChargingEnemyState.Recovering) { 
            if (spriteRenderer.color.g >= 215/255f) {
                state = ChargingEnemyState.Walking;
            }
            else {
                // change color and do not move
                float newGreen = Mathf.MoveTowards(spriteRenderer.color.g, 220 / 255f, 0.2f * Time.deltaTime);
                spriteRenderer.color = new Color(spriteRenderer.color.r, newGreen, spriteRenderer.color.b);
            }
        }
    }

}
