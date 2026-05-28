using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public PlayerStats stats;

    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    private SwordMotion swordMotion;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        swordMotion =
    GetComponentInChildren<SwordMotion>();
    }

    void Update()
    {
        movement = Vector2.zero;

        // WASD input
        if (Input.GetKey(KeyCode.W))
            movement.y = 1;
        if (Input.GetKey(KeyCode.S))
            movement.y = -1;
        if (Input.GetKey(KeyCode.A))
            movement.x = -1;
        if (Input.GetKey(KeyCode.D))
            movement.x = 1;

        movement = movement.normalized;

        // Flip sprite
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;

            swordMotion.SetFacing(false);
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true;

            swordMotion.SetFacing(true);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement * stats.moveSpeed;
    }
}