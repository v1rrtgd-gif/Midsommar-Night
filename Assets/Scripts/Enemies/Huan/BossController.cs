using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player;

    [Header("Projectile Attack")]
    public GameObject projectilePrefab;

    public Transform firePoint;

    public float shootCooldown = 1.5f;

    private float lastShot;

    [Header("Big Attack")]
    public GameObject rectangleWarningPrefab;

    public float bigAttackCooldown = 10f;

    private float lastBigAttack;

    [Header("Bomb")]
    public GameObject bombPrefab;

    public float bombCooldown = 6f;

    private float lastBomb;

    void Start()
    {
        if (player == null)
        {
            player =
                FindFirstObjectByType<PlayerStatsHolder>()
                .transform;
        }
    }

    void Update()
    {
        if (Time.time > lastShot + shootCooldown)
        {
            Shoot();

            lastShot = Time.time;
        }

        if (Time.time > lastBigAttack + bigAttackCooldown)
        {
            BigAttack();

            lastBigAttack = Time.time;
        }

        if (Time.time > lastBomb + bombCooldown)
        {
            ThrowBomb();

            lastBomb = Time.time;
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
            (player.position - firePoint.position)
            .normalized;
    }

    void BigAttack()
    {
        Vector3 center =
            player.position;

        GameObject warning =
            Instantiate(
                rectangleWarningPrefab,
                center,
                Quaternion.identity
            );

        BossRectangleAttack attack =
            warning.GetComponent<BossRectangleAttack>();

        attack.targetPlayer = player;
    }

    void ThrowBomb()
    {
        GameObject bomb =
            Instantiate(
                bombPrefab,
                transform.position,
                Quaternion.identity
            );

        BossBomb b =
            bomb.GetComponent<BossBomb>();

        b.target =
            player.position;
    }
}