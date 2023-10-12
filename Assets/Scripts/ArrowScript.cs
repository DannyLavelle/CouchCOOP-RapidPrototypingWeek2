using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f; // Speed at which the arrow flies
    public float maxDistance = 10f; // Maximum distance the arrow can travel
    public float dmg = 3.34f;
   // Define the enemy layer to detect collisions

    private Vector3 initialPosition; // The starting position of the arrow

    void Start()
    {
        initialPosition = transform.position;
        // Set the initial velocity to move the arrow forward
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        // Calculate the current distance the arrow has traveled
        float currentDistance = Vector3.Distance(initialPosition, transform.position);

        // If the arrow has traveled the maximum distance, destroy it
        if (currentDistance >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {
            HitManager hit = collision.gameObject.GetComponent<HitManager>();
            hit.Hit(dmg);
        }
    }

}
