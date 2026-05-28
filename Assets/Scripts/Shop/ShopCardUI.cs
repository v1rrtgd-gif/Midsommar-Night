using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCardUI : MonoBehaviour
{
    public TMP_Text titleText;

    public TMP_Text descriptionText;

    public TMP_Text priceText;

    public Button button;

    private AbilityCard card;

    private ShopManager manager;

    public void Setup(
        AbilityCard newCard,
        int price,
        ShopManager shop
    )
    {
        card = newCard;

        manager = shop;

        titleText.text = card.cardName;

        descriptionText.text = card.description;

        priceText.text = price.ToString();

        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(Buy);
    }

    void Buy()
    {
        manager.BuyCard(card);
    }
}
