using System.Diagnostics;
using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public EventData[] allEvents;

    public EventUI eventUI;

    private PlayerStatsHolder player;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player = FindFirstObjectByType<PlayerStatsHolder>();
    }

    public void StartRandomEvent()
    {
        EventData chosen =
            allEvents[
                UnityEngine.Random.Range(0, allEvents.Length)
            ];

        eventUI.ShowEvent(chosen);
    }

    public void ResolveChoice(EventChoice choice)
    {
        switch (choice.outcomeType)
        {
            case EventOutcomeType.GainHP:

                player.stats.currentHP += choice.value;

                if (player.stats.currentHP >
                    player.stats.maxHP)
                {
                    player.stats.currentHP =
                        player.stats.maxHP;
                }

                break;

            case EventOutcomeType.LoseHP:

                player.stats.TakeDamage(choice.value);

                break;

            case EventOutcomeType.GainAttack:

                player.stats.attack += choice.value;

                break;

            case EventOutcomeType.GainCoins:

                UnityEngine.Debug.Log("Coins +" + choice.value);

                break;

            case EventOutcomeType.Nothing:

                break;
        }

        eventUI.HideEvent();

        DungeonRunManager.Instance.NodeCleared();
    }
}