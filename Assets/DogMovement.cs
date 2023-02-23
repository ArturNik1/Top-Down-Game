using UnityEngine;

public class DogMovement : MonoBehaviour
{
    public float speed = 2f; // The speed at which the dog moves
    public float wanderDistance = 20f; // The maximum distance the dog will wander
    public float wanderTime = 100f; // The time the dog will spend wandering before changing direction

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

        // Move the dog in the current direction
        transform.Translate(new Vector2(direction * speed * Time.deltaTime, 0f));

        // Check if the dog has wandered too far and needs to turn around
        if (transform.position.x > wanderDistance || transform.position.x < -wanderDistance)
        {
            direction *= -1; // Reverse the direction
        }

        // Flip the sprite to face the direction of movement
        if (direction < 0)
        {
            transform.localScale = new Vector3(0.7f, 0.7f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-0.7f, 0.7f, 1f);
        }
    }
}
