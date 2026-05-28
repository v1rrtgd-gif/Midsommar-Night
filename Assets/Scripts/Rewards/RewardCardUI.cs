using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class RewardCardUI : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text descriptionText;

    public UnityEngine.UI.Image borderImage;

    private AbilityCard card;
    private RewardUIManager manager;

    public void Setup(AbilityCard newCard, RewardUIManager uiManager)
    {
        card = newCard;
        manager = uiManager;

        titleText.text = card.cardName;
        descriptionText.text = card.description;

        switch (card.rarity)
        {
            case RewardRarity.Common:
                borderImage.color = Color.gray;
                break;

            case RewardRarity.Rare:
                borderImage.color = Color.blue;
                break;

            case RewardRarity.Epic:
                borderImage.color = new Color(0.6f, 0f, 1f);
                break;

            case RewardRarity.Legendary:
                borderImage.color = new Color(1f, 0.5f, 0f);
                break;
        }
    }

    public void SelectCard()
    {
        manager.SelectCard(card);
    }
}
