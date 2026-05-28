using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public PlayerStatsHolder playerStatsHolder;
    public Slider slider;

    void Update()
    {
        slider.maxValue = playerStatsHolder.stats.maxHP;
        slider.value = playerStatsHolder.stats.currentHP;
    }
}