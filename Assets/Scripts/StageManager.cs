using System.Diagnostics;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public RewardUIManager rewardUI;

    private bool rewardsSpawned;

    private bool encounterStarted;

    public void StartEncounter()
    {
        encounterStarted = true;

        rewardsSpawned = false;
    }

    void Update()
    {
        if (!encounterStarted)
            return;

        Enemy[] enemies =
            FindObjectsByType<Enemy>(
                FindObjectsSortMode.None
            );

        if (enemies.Length == 0 && !rewardsSpawned)
        {
            rewardsSpawned = true;

            encounterStarted = false;

            UnityEngine.Debug.Log("Stage cleared");

            DungeonNodeType type =
                DungeonRunManager.Instance.currentNode.type;

            if (type == DungeonNodeType.Event)
            {
                DungeonRunManager.Instance.NodeCleared();
            }
            else
            {
                rewardUI.ShowRewards();
            }


        }
    }
}