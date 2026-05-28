using System.Collections;
using UnityEngine;

public class BossRectangleAttack : MonoBehaviour
{
    public float delay = 2f;

    public float damage = 30f;

    public Vector2 size =
        new Vector2(8f, 3f);

    public Transform targetPlayer;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        Color c = sr.color;
        c.a = 0.4f;

        sr.color = c;

        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(delay);

        Collider2D hit =
            Physics2D.OverlapBox(
                transform.position,
                size,
                0,
                LayerMask.GetMask("Player")
            );

        if (hit != null)
        {
            PlayerHealth ph =
                hit.GetComponent<PlayerHealth>();

            if (ph != null)
            {
                ph.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }

    void Update()
    {
        if (targetPlayer != null)
        {
            transform.position =
                targetPlayer.position;
        }
    }
}