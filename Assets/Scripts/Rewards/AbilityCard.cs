using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Ability Card")]
public class AbilityCard : ScriptableObject
{
    public string cardName;

    [TextArea]
    public string description;

    public AbilityType type;

    public RewardRarity rarity;

    public float value;
}
