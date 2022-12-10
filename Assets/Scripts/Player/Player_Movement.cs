using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed;
    public float dashSpeed;

    private float dashing = 1.0f;
    private new Rigidbody2D rigidbody;
    private PlayerCollision playerCollision;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerCollision = GetComponent<PlayerCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCollision.isHit) {
            rigidbody.velocity = playerCollision.getHitDirection();
            return;
        }


        Vector2 dir;
        dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            dir.x = -1;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            dir.x = 1;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            dir.y = 1;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            dir.y = -1;
        }

        dir.Normalize();
        rigidbody.velocity = speed * dir * dashing;

    }

    public void setDashing(bool flag)
    {
        if (flag) dashing = dashSpeed;
        else dashing = 1.0f;
    }
}
