using UnityEngine;

public class GasCloud : MonoBehaviour
{
    public float duration = 5f;

    public float tickDamage = 5f;

    public float tickRate = 1f;

    private float lastTick;

    void Start()
    {
        Destroy(gameObject, duration);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time > lastTick + tickRate)
            {
                PlayerHealth ph =
                    other.GetComponent<PlayerHealth>();

                if (ph != null)
                {
                    ph.TakeDamage(tickDamage);
                }

                lastTick = Time.time;
            }
        }
    }
}