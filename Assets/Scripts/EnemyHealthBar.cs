using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public Transform target;

    public Vector3 offset = new Vector3(0, 1.5f, 0);

    void Update()
    {
        if (target != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(
                target.position + offset
            );

            transform.position = screenPos;
        }
    }

    public void SetHealth(float current, float max)
    {
        slider.maxValue = max;
        slider.value = current;
    }
}
