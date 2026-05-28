using System.Collections;
using UnityEngine;

public class BossBomb : MonoBehaviour
{
    public Vector3 target;

    public float speed = 8f;

    public float impactDamage = 20f;

    public GameObject gasPrefab;

    void Update()
    {
        transform.position =
            Vector3.MoveTowards(
                transform.position,
                target,
                speed * Time.deltaTime
            );

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            Explode();
        }
    }

    void Explode()
    {
        Collider2D hit =
            Physics2D.OverlapCircle(
                transform.position,
                1f,
                LayerMask.GetMask("Player")
            );

        if (hit != null)
        {
            PlayerHealth ph =
                hit.GetComponent<PlayerHealth>();

            if (ph != null)
            {
                ph.TakeDamage(impactDamage);
            }
        }

        Instantiate(
            gasPrefab,
            transform.position,
            Quaternion.identity
        );

        Destroy(gameObject);
    }
}