using UnityEngine;
using TMPro;
using System;

public class DamageText : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float lifetime = 1f;

    private TextMeshProUGUI textMesh;
    private Color originalColor;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        originalColor = textMesh.color;
        transform.position += new Vector3(
    UnityEngine.Random.Range(-20f, 20f),
    0,
    0
);
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // Fade out
        Color c = textMesh.color;
        c.a -= Time.deltaTime / lifetime;

        textMesh.color = c;
    }

    public void SetText(string txt)
    {
        if (textMesh == null)
            textMesh = GetComponent<TextMeshProUGUI>();

        textMesh.text = txt;
    }
}
