using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class EventUI : MonoBehaviour
{
    public GameObject panel;

    public TMP_Text titleText;

    public TMP_Text descriptionText;

    public UnityEngine.UI.Image eventImage;

    public Button[] choiceButtons;

    public TMP_Text[] buttonTexts;

    private EventData currentEvent;

    public void ShowEvent(EventData data)
    {
        currentEvent = data;

        panel.SetActive(true);

        Time.timeScale = 0f;

        titleText.text = data.eventTitle;

        descriptionText.text = data.description;

        eventImage.sprite = data.image;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < data.choices.Length)
            {
                choiceButtons[i].gameObject.SetActive(true);

                buttonTexts[i].text =
                    data.choices[i].buttonText;

                int index = i;

                choiceButtons[i].onClick.RemoveAllListeners();

                choiceButtons[i].onClick.AddListener(() =>
                {
                    EventManager.Instance.ResolveChoice(
                        currentEvent.choices[index]
                    );
                });
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void HideEvent()
    {
        panel.SetActive(false);

        Time.timeScale = 1f;
    }
}
