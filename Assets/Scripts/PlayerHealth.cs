using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    private PlayerStatsHolder statsHolder;

    private SpriteRenderer sr;

    private Vector3 originalScale;

    private Coroutine flashRoutine;

    void Start()
    {
        statsHolder = GetComponent<PlayerStatsHolder>();

        sr = GetComponent<SpriteRenderer>();

        originalScale = transform.localScale;
    }

    public void TakeDamage(float damage)
    {
        statsHolder.stats.TakeDamage(damage);

        DamageNumberManager.Instance.ShowDamage(
            damage,
            transform.position
        );

        // Reset previous effect safely
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);

            sr.color = Color.white;
            transform.localScale = originalScale;
        }

        flashRoutine = StartCoroutine(DamageFlash());
    }

    IEnumerator DamageFlash()
    {
        float duration = 0.2f;

        Vector3 squishScale = new Vector3(
            originalScale.x,
            originalScale.y * 0.7f,
            originalScale.z
        );

        Color originalColor = Color.white;

        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;

            float p = t / duration;

            sr.color = Color.Lerp(
                originalColor,
                Color.red,
                Mathf.PingPong(p * 2f, 1f)
            );

            transform.localScale = Vector3.Lerp(
                originalScale,
                squishScale,
                Mathf.Sin(p * Mathf.PI)
            );

            yield return null;
        }

        // ALWAYS restore
        sr.color = originalColor;

        transform.localScale = originalScale;

        flashRoutine = null;
    }
}