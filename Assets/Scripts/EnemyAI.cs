using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float followDistance = 10f;
    public float attackRange = 4f;
    public float damageCooldown = 2f;

    private float lastHitTime;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        // Only follow when close (simulate "camera sees them")
        if (distance > 3f)
        {
            Vector2 dir = (player.position - transform.position).normalized;

            if (distance > attackRange)
            {
                rb.velocity = dir * speed;
            }
            else
            {
                UnityEngine.Debug.Log("Started hitting");
                rb.velocity = Vector2.zero;

                if (Time.time > lastHitTime + damageCooldown)
                {
                    
                    PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(GetComponent<Enemy>().damage);
                    }
                }
            }
        }
        else
        {
            rb.velocity = Vector2.zero;

            if (Time.time > lastHitTime + damageCooldown)
            {

                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(GetComponent<Enemy>().damage);
                }
                lastHitTime = Time.time;
            }
        }
    }
}
