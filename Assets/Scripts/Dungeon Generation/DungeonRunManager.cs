using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DungeonRunManager : MonoBehaviour
{
    public static DungeonRunManager Instance;

    public DungeonNode currentNode;

    public GatewayButton[] gatewayButtons;

    public EnemySpawner enemySpawner;

    public Transform player;

    public Transform playerSpawnPoint;
    public GameObject mapPanel;

    void Awake()
    {
        Instance = this;
    }

    void StartEncounter(DungeonNode node)
    {
        UnityEngine.Debug.Log("started");

        // MOVE PLAYER
        player.position =
            playerSpawnPoint.position;

        // CLEAR OLD ENEMIES
        Enemy[] enemies =
            FindObjectsByType<Enemy>(
                FindObjectsSortMode.None
            );

        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

        // CLEAR PROJECTILES
        EnemyProjectile[] projectiles =
            FindObjectsByType<EnemyProjectile>(
                FindObjectsSortMode.None
            );

        foreach (EnemyProjectile p in projectiles)
        {
            Destroy(p.gameObject);
        }

        // LOAD NODE ENCOUNTER
        switch (node.type)
        {
            case DungeonNodeType.Normal:

                enemySpawner.SpawnLevel();

                break;
            case DungeonNodeType.Start:

                enemySpawner.SpawnLevel();

                break;

            case DungeonNodeType.Elite:

                enemySpawner.currentLevel += 2;

                enemySpawner.SpawnLevel();

                break;

            case DungeonNodeType.Shop:

                UnityEngine.Debug.Log("SHOP");

                break;

            case DungeonNodeType.Event:

                EventManager.Instance.StartRandomEvent();

                break;

            case DungeonNodeType.Boss:

                UnityEngine.Debug.Log("BOSS");

                break;
        }
        FindFirstObjectByType<StageManager>()
    .StartEncounter();
    }

    public void StartRun(DungeonNode startNode)
    {
        currentNode = startNode;

        currentNode.unlocked = true;

        currentNode.visited = true;

        currentNode.view.Refresh();

        mapPanel.SetActive(false);

        StartEncounter(startNode);
    }

    public void NodeCleared()
    {
        currentNode.cleared = true;

        currentNode.view.Refresh();

        UnlockConnectedNodes();

        mapPanel.SetActive(true);

        Time.timeScale = 0f;
    }
    void UnlockConnectedNodes()
    {
        foreach (DungeonNode node in currentNode.connections)
        {
            node.unlocked = true;

            node.view.Refresh();
        }
    }

    void ShowGateways()
    {
        HideAllGateways();

        List<DungeonNode> nextNodes =
            currentNode.connections;
        

        for (int i = 0; i < nextNodes.Count; i++)
        {
            gatewayButtons[i].gameObject.SetActive(true);

            gatewayButtons[i].Setup(nextNodes[i]);
            UnityEngine.Debug.Log(i);
        }
    }

    void HideAllGateways()
    {
        foreach (GatewayButton button in gatewayButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void TravelToNode(DungeonNode node)
    {
        // ONLY FORWARD CONNECTED NODES
        if (!currentNode.connections.Contains(node))
            return;

        currentNode = node;

        mapPanel.SetActive(false);

        Time.timeScale = 1f;

        StartEncounter(node);
    }
}
