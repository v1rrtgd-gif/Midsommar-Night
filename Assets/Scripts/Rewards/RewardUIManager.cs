using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RewardUIManager : MonoBehaviour
{
    public GameObject panel;

    public RewardCardUI[] cardSlots;

    public AbilityCard[] allCards;

    public PlayerAbilityManager playerAbilities;

    public PlayerStatsHolder playerStats;

    [Header("Chest Chances")]
    public float commonChestChance = 50f;
    public float epicChestChance = 40f;
    public float legendaryChestChance = 10f;

    void Start()
    {
        panel.SetActive(false);
    }

    public void ShowRewards()
    {
        panel.SetActive(true);

        Time.timeScale = 0f;

        ChestType chestType = RollChestType();

        List<AbilityCard> selectedCards = GenerateCards(chestType);

        for (int i = 0; i < cardSlots.Length; i++)
        {
            cardSlots[i].gameObject.SetActive(true);

            cardSlots[i].Setup(selectedCards[i], this);
            UnityEngine.Debug.Log(selectedCards[i]);
        }

        UnityEngine.Debug.Log("Opened " + chestType + " chest");
    }

    ChestType RollChestType()
    {
        float roll = UnityEngine.Random.Range(0f, 100f);

        if (roll < legendaryChestChance)
            return ChestType.Legendary;

        if (roll < legendaryChestChance + epicChestChance)
            return ChestType.Epic;

        return ChestType.Common;
    }

    List<AbilityCard> GenerateCards(ChestType chestType)
    {
        List<AbilityCard> result = new List<AbilityCard>();

        for (int i = 0; i < 3; i++)
        {
            RewardRarity rarity =
                RollCardRarity(chestType, playerStats.stats.level);

            List<AbilityCard> possibleCards =
                new List<AbilityCard>();

            foreach (AbilityCard card in allCards)
            {
                if (card.rarity == rarity)
                {
                    possibleCards.Add(card);
                }
            }

            if (possibleCards.Count == 0)
                continue;

            AbilityCard selected =
                possibleCards[
                    UnityEngine.Random.Range(0, possibleCards.Count)
                ];

            result.Add(selected);
        }

        return result;
    }

    RewardRarity RollCardRarity(
        ChestType chestType,
        int level)
    {
        float roll = UnityEngine.Random.Range(0f, 100f);

        float common = 0;
        float rare = 0;
        float epic = 0;
        float legendary = 0;

        switch (chestType)
        {
            case ChestType.Common:

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

                break;

            case ChestType.Epic:

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

                break;

            case ChestType.Legendary:

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

    public void SelectCard(AbilityCard card)
    {
        playerAbilities.ApplyCard(card);

        panel.SetActive(false);

        Time.timeScale = 1f;

        foreach (RewardCardUI cardUI in cardSlots)
        {
            cardUI.gameObject.SetActive(false);
        }
        DungeonRunManager.Instance.NodeCleared();
        UnityEngine.Debug.Log("Selected: " + card.cardName);
    }
}