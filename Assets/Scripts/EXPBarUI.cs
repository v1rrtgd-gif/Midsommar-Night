using UnityEngine;
using UnityEngine.UI;

public class EXPBarUI : MonoBehaviour
{
    public PlayerStatsHolder playerStatsHolder;

    public Slider slider;

    void Update()
    {
        slider.maxValue = playerStatsHolder.stats.requiredEXP;
        slider.value = playerStatsHolder.stats.currentEXP;
    }
}
