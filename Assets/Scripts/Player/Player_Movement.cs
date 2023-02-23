using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed;
    public float dashSpeed;
    public Animator animator;
    private Vector2 attackDirection;
    private float dashing = 1.0f;
    private new Rigidbody2D rigidbody;
    private PlayerCollision playerCollision;
    private PlayerCombat playerCombat;
    private SpriteRenderer spriteRenderer;

    [HideInInspector]
    public bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerCollision = GetComponent<PlayerCollision>();
        playerCombat = GetComponent<PlayerCombat>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (paused) return;
        if (playerCollision.isHit) {
            rigidbody.velocity = playerCollision.getHitDirection();
            return;
        }

        //animations
        animator.SetFloat("Speed", rigidbody.velocity.magnitude);

        float moveDirection = Input.GetAxisRaw("Horizontal");

        if (moveDirection > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveDirection < 0)
        {
            spriteRenderer.flipX = true;
        }
        /*
        if (playerCombat.isAttacking)
        {
            rigidbody.velocity = MeleeAttackMovement();
            return;
        }
        */
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

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) {
            dir.x = 0;
        }

        dir.Normalize();
        rigidbody.velocity = speed * dir * dashing;

    }

    public void setDashing(bool flag)
    {
        if (flag) dashing = dashSpeed;
        else dashing = 1.0f;
    }
    //not in use- i wanted that when the player attacks he will move a little
    public Vector3 MeleeAttackMovement() {
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        attackDirection = mousePos3D - transform.position;
        attackDirection.Normalize();
        return attackDirection;
    }


    private void Pause()
    {
        paused = true;
    }

    private void Unpause()
    {
        paused = false;
    }

    private void OnEnable()
    {
        UIManager.onPause += Pause;
        UIManager.onUnpause += Unpause;
    }

    private void OnDisable()
    {
        UIManager.onPause -= Pause;
        UIManager.onUnpause -= Unpause;
    }
}
