using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [Header("UI")]
    public GameObject panel;

    public TMP_Text goldText;

    public ShopCardUI[] cardSlots;

    [Header("Cards")]
    public AbilityCard[] allCards;

    [Header("References")]
    public PlayerAbilityManager abilityManager;

    public PlayerStatsHolder player;

    [Header("Prices")]
    public int healPrice = 40;

    public int cardPrice = 60;

    private List<AbilityCard> currentCards =
        new List<AbilityCard>();

    void Awake()
    {
        Instance = this;
    }

    public void OpenShop()
    {
        panel.SetActive(true);

        Time.timeScale = 0f;

        goldText.text =
            "Gold: " + player.stats.coins;

        GenerateCards();
    }

    void GenerateCards()
    {
        currentCards.Clear();

        for (int i = 0; i < cardSlots.Length; i++)
        {
            AbilityCard card =
                allCards[
                    UnityEngine.Random.Range(0, allCards.Length)
                ];

            currentCards.Add(card);

            cardSlots[i].Setup(
                card,
                cardPrice,
                this
            );
        }
    }

    public void BuyCard(AbilityCard card)
    {
        if (player.stats.coins < cardPrice)
            return;

        player.stats.coins -= cardPrice;

        abilityManager.ApplyCard(card);

        goldText.text =
            "Gold: " + player.stats.coins;
    }

    public void BuyHeal()
    {
        if (player.stats.coins < healPrice)
            return;

        player.stats.coins -= healPrice;

        player.stats.currentHP =
            Mathf.Min(
                player.stats.maxHP,
                player.stats.currentHP + 40
            );

        goldText.text =
            "Gold: " + player.stats.coins;
    }

    public void SkipShop()
    {
        panel.SetActive(false);

        Time.timeScale = 1f;

        DungeonRunManager.Instance.NodeCleared();
    }
}