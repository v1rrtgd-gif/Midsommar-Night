using System.Diagnostics;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    [Header("Level")]
    public int level = 1;

    public int currentEXP = 0;
    public int requiredEXP = 50;

    [Header("Base Stats")]
    public float moveSpeed = 5f;

    public float maxHP = 100f;
    public float currentHP = 100f;

    public float attack = 10f;

    public float critChance = 0.1f;
    public float critDamage = 2f;

    public float attackSpeed = 1f;

    public float cooldownReduction = 0f;

    public float damageResistance = 0f;

    [Header("Currency")]
    public int coins = 0;

    public void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            UnityEngine.Debug.Log("Player died");
        }
    }

    public void GainEXP(int amount)
    {
        currentEXP += amount;
        UnityEngine.Debug.Log(
    "EXP gained: " + amount +
    " Current EXP: " + currentEXP
);
        while (currentEXP >= requiredEXP)
        {
            currentEXP -= requiredEXP;
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;

        requiredEXP = GetRequiredEXP(level);

        maxHP += 20f;
        currentHP = maxHP;

        attack += 3f;

        damageResistance += 0.01f;

        UnityEngine.Debug.Log("LEVEL UP -> " + level);
    }

    int GetRequiredEXP(int lvl)
    {
        switch (lvl)
        {
            case 2:
                return 50;

            case 3:
                return 200;

            default:
                return Mathf.RoundToInt(
                    200 * Mathf.Pow(1.35f, lvl - 3)
                );
        }
    }
}

