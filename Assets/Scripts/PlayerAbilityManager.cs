using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
    public PlayerStatsHolder playerStatsHolder;

    void Start()
    {
            playerStatsHolder = FindFirstObjectByType<PlayerStatsHolder>();
    }

    public void ApplyCard(AbilityCard card)
    {
        switch (card.type)
        {
            case AbilityType.AttackUp:
                playerStatsHolder.stats.attack += card.value;
                break;

            case AbilityType.MaxHPUp:
                playerStatsHolder.stats.maxHP += card.value;
                playerStatsHolder.stats.currentHP += card.value;
                break;

            case AbilityType.AttackSpeedUp:
                playerStatsHolder.stats.attackSpeed += card.value;
                break;

            case AbilityType.MoveSpeedUp:
                playerStatsHolder.stats.moveSpeed += card.value;
                break;

            case AbilityType.CritChanceUp:
                playerStatsHolder.stats.critChance += card.value;
                break;

            case AbilityType.CritDamageUp:
                playerStatsHolder.stats.critDamage += card.value;
                break;

            case AbilityType.DamageResistanceUp:
                playerStatsHolder.stats.damageResistance += card.value;
                break;

            case AbilityType.CooldownReductionUp:
                playerStatsHolder.stats.cooldownReduction += card.value;
                break;
        }
    }
}