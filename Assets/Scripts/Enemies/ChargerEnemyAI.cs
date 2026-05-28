using UnityEngine;
using System.Collections;

public class ChargerEnemyAI : MonoBehaviour
{
    public Transform player;

    public float moveSpeed = 2f;

    public float chargeSpeed = 12f;

    public float chargeCooldown = 4f;

    public float chargeDuration = 0.5f;

    private Rigidbody2D rb;

    private bool charging;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(ChargeRoutine());
    }

    void FixedUpdate()
    {
        if (charging)
            return;

        Vector2 dir =
            (player.position - transform.position).normalized;

        rb.velocity = dir * moveSpeed;
    }

    IEnumerator ChargeRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(chargeCooldown);

            charging = true;

            Vector2 dir =
                (player.position - transform.position).normalized;

            rb.velocity = dir * chargeSpeed;

            yield return new WaitForSeconds(chargeDuration);

            charging = false;
        }
    }
}