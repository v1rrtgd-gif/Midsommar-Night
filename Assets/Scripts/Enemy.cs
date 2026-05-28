using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Enemy : MonoBehaviour
{
    public float maxHP = 50f;
    private float currentHP;

    public float damage = 10f;

    private SpriteRenderer sr;
    private Vector3 originalScale;

    private bool isDead;
    public Canvas uiCanvas;
    public GameObject healthBarPrefab;

    private EnemyHealthBar healthBar;
    private bool healthBarShown;

    public int expReward = 10;
    private PlayerStatsHolder player;

    void Start()
    {
        currentHP = maxHP;
        if (player == null)
            player = FindFirstObjectByType<PlayerStatsHolder>();

        if (uiCanvas == null)
            uiCanvas = FindFirstObjectByType<Canvas>();
        sr = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
    }

    public void TakeDamage(float dmg)
    {
        if (isDead) return;
        if (!healthBarShown)
        {
            UnityEngine.Debug.Log("showed");
            GameObject hb = Instantiate(
                healthBarPrefab,
                uiCanvas.transform
            );

            healthBar = hb.GetComponent<EnemyHealthBar>();
            healthBar.target = transform;

            healthBarShown = true;
        }


        currentHP -= dmg;


        DamageNumberManager.Instance.ShowDamage( dmg,transform.position );


        UnityEngine.Debug.Log("Enemy took damage: " + dmg);

        StopAllCoroutines();
        StartCoroutine(HitEffect());


        if (currentHP <= 0)
        {

            Die();
        }

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHP, maxHP);
        }
    }

    IEnumerator HitEffect()
    {
        float duration = 0.15f;

        Color originalColor = sr.color;
        Color redColor = Color.red;

        Vector3 squishScale = new Vector3(
            originalScale.x * 0.85f,
            originalScale.y * 0.85f,
            originalScale.z
        );

        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float p = t / duration;

            // flash red
            sr.color = Color.Lerp(originalColor, redColor, Mathf.PingPong(p * 2f, 1f));

            // squish
            transform.localScale = Vector3.Lerp(originalScale, squishScale, Mathf.Sin(p * Mathf.PI));

            yield return null;
        }

        sr.color = originalColor;
        transform.localScale = originalScale;
    }

    void Die()
    {
        isDead = true;
        if (healthBar != null)
        {
            Destroy(healthBar.gameObject);
        }

        if (player != null)
        {
            UnityEngine.Debug.Log("got exp ");
            player.stats.GainEXP(expReward);
        }
        Destroy(gameObject);
    }
}