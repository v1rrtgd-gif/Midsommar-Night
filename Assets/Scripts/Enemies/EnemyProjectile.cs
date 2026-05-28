using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 10f;

    public float damage = 10f;

    public Vector2 direction;

    void Update()
    {
        transform.position +=
            (Vector3)(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore enemy collisions
        if (other.CompareTag("Enemy"))
            return;

        PlayerHealth player =
            other.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.TakeDamage(damage);

            Destroy(gameObject);
            return;
        }

        // Destroy on walls/solid objects
        if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}
