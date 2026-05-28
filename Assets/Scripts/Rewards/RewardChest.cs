using System;
using UnityEngine;

public class RewardChest : MonoBehaviour
{
    public ChestType chestType;

    public RewardRarity RollRarity(int playerLevel)
    {
        float roll = UnityEngine.Random.Range(0f, 100f);

        float common = 0f;
        float rare = 0f;
        float epic = 0f;
        float legendary = 0f;

        switch (chestType)
        {
            case ChestType.Common:
                GetCommonChestRates(playerLevel,
                    out common,
                    out rare,
                    out epic,
                    out legendary);
                break;

            case ChestType.Epic:
                GetEpicChestRates(playerLevel,
                    out common,
                    out rare,
                    out epic,
                    out legendary);
                break;

            case ChestType.Legendary:
                GetLegendaryChestRates(playerLevel,
                    out common,
                    out rare,
                    out epic,
                    out legendary);
                break;
        }

        if (roll < legendary)
            return RewardRarity.Legendary;

        if (roll < legendary + epic)
            return RewardRarity.Epic;

        if (roll < legendary + epic + rare)
            return RewardRarity.Rare;

        return RewardRarity.Common;
    }

    void GetCommonChestRates(int level,
        out float common,
        out float rare,
        out float epic,
        out float legendary)
    {
        if (level >= 15)
        {
            common = 35;
            rare = 33;
            epic = 27;
            legendary = 5;
        }
        else if (level >= 10)
        {
            common = 40;
            rare = 33;
            epic = 22;
            legendary = 5;
        }
        else if (level >= 5)
        {
            common = 45;
            rare = 32;
            epic = 19;
            legendary = 4;
        }
        else
        {
            common = 50;
            rare = 32;
            epic = 15;
            legendary = 3;
        }
    }

    void GetEpicChestRates(int level,
        out float common,
        out float rare,
        out float epic,
        out float legendary)
    {
        if (level >= 15)
        {
            common = 25;
            rare = 35;
            epic = 30;
            legendary = 10;
        }
        else if (level >= 10)
        {
            common = 30;
            rare = 35;
            epic = 25;
            legendary = 10;
        }
        else if (level >= 5)
        {
            common = 35;
            rare = 35;
            epic = 25;
            legendary = 5;
        }
        else
        {
            common = 40;
            rare = 33;
            epic = 22;
            legendary = 5;
        }
    }

    void GetLegendaryChestRates(int level,
        out float common,
        out float rare,
        out float epic,
        out float legendary)
    {
        if (level >= 15)
        {
            common = 1;
            rare = 25;
            epic = 29;
            legendary = 45;
        }
        else if (level >= 10)
        {
            common = 1;
            rare = 29;
            epic = 35;
            legendary = 35;
        }
        else if (level >= 5)
        {
            common = 1;
            rare = 29;
            epic = 40;
            legendary = 30;
        }
        else
        {
            common = 1;
            rare = 34;
            epic = 35;
            legendary = 30;
        }
    }
}