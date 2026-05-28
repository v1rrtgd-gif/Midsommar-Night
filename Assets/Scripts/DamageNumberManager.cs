using UnityEngine;

public class DamageNumberManager : MonoBehaviour
{
    public static DamageNumberManager Instance;

    public GameObject damageTextPrefab;
    public Canvas canvas;

    void Awake()
    {
        Instance = this;
    }

    public void ShowDamage(float damage, Vector3 worldPosition)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);

        GameObject obj = Instantiate(
            damageTextPrefab,
            screenPos,
            Quaternion.identity,
            canvas.transform
        );

        DamageText dt = obj.GetComponentInChildren<DamageText>();

        if (dt != null)
        {
            dt.SetText(Mathf.RoundToInt(damage).ToString());
        }
    }
}
