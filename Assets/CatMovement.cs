using UnityEngine;

public class CatMovement : MonoBehaviour
{
    public float speed = 2f; // The speed at which the dog moves
    public float wanderDistance = 20f; // The maximum distance the dog will wander
    public float wanderTime = 10f; // The time the dog will spend wandering before changing direction
    public Animator animator;
    private float timer; // Timer to keep track of how long the dog has been wandering
    private int direction = 1; // The direction the dog is currently moving in

    void Update()
    {
        timer += Time.deltaTime;

        // If the dog has been wandering for longer than the wander time, change direction
        if (timer > wanderTime)
        {
            timer = 0f;
            direction *= -1; // Reverse the direction
        }
        animator.SetFloat("Ydir", direction);

        // Move the dog in the current direction
        transform.Translate(new Vector2(0f, direction * speed * Time.deltaTime));

        // Check if the dog has wandered too far and needs to turn around
        if (transform.position.y > wanderDistance || transform.position.y < -wanderDistance)
        {
            direction *= -1; // Reverse the direction
        }

    }
}
