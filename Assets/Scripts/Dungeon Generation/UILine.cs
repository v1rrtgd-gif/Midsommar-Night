using UnityEngine;
using UnityEngine.UI;

public class UILine : MonoBehaviour
{
    public RectTransform rect;

    public void Setup(Vector2 start, Vector2 end)
    {
        Vector2 dir = end - start;

        rect.sizeDelta =
            new Vector2(dir.magnitude, 6f);

        rect.anchoredPosition =
            start + dir / 2f;

        float angle =
            Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        rect.rotation =
            Quaternion.Euler(0, 0, angle);
    }
}
