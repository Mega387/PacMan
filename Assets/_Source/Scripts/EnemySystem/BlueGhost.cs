using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGhost : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction = Vector2.right;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }


    private void UpdateAnimation()
    {
        animator.SetBool("isMoving", rb.velocity.magnitude > 0);

        if (rb.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("level"))
        {
            ChangeDirectionRandom();
        }
    }

    private void ChangeDirectionRandom()
    {
        Vector2[] possibleDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        Vector2 newDirection;

        do
        {
            newDirection = possibleDirections[Random.Range(0, possibleDirections.Length)];
        }
        while (newDirection == direction);

        direction = newDirection;
    }
}