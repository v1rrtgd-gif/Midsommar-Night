using System.Diagnostics;
using UnityEngine;

public class RangedEnemyAI : MonoBehaviour
{
    public Transform player;

    public float moveSpeed = 2f;

    public float preferredDistance = 7f;

    public float shootCooldown = 2f;

    public GameObject projectilePrefab;

    public Transform firePoint;

    private float lastShot;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        float distance =
            Vector2.Distance(transform.position, player.position);

        Vector2 dir =
            (player.position - transform.position).normalized;

        // Move toward player until in range
        if (distance > preferredDistance)
        {
            rb.velocity = dir * moveSpeed;
        }
        else
        {
            // Stop moving when in attack range
            rb.velocity = Vector2.zero;

            // Shoot repeatedly
            if (Time.time > lastShot + shootCooldown)
            {
                Shoot();


                lastShot = Time.time;
            }
        }
    }

    void Shoot()
    {
        GameObject bullet =
            Instantiate(
                projectilePrefab,
                firePoint.position,
                Quaternion.identity
            );

        EnemyProjectile proj =
            bullet.GetComponent<EnemyProjectile>();

        proj.direction =
            (player.position - firePoint.position).normalized;
    }
}
