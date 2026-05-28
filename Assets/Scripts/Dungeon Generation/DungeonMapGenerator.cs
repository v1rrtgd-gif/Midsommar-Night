using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMapGenerator : MonoBehaviour
{
    [Header("Layout")]
    public int columns = 8;

    public int rows = 3;

    public float horizontalSpacing = 250f;

    public float verticalSpacing = 150f;


    [Header("Paths")]
    public int extraConnections = 8;
    public GameObject linePrefab;
    public float nodeRadius = 30f;
    

        [Header("References")]
    public GameObject nodePrefab;

    public Transform nodeParent;

    private DungeonNode[,] grid;

    private List<DungeonNode> allNodes =
        new List<DungeonNode>();

    private DungeonNode startNode;

    private DungeonNode bossNode;

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        CreateGrid(); 

        AssignSpecialNodes();

        GenerateMainPaths();

        SpawnVisuals();

        DungeonRunManager.Instance.StartRun(startNode);
    }
    // =====================================
    // GRID
    // =====================================

    void CreateGrid()
    {
        grid = new DungeonNode[rows, columns];

        int middleRow = rows / 2;

        for (int col = 0; col < columns; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                // START NODE
                if (col == 0 && row != middleRow)
                    continue;

                // BOSS COLUMN
                if (col == columns - 1 && row != middleRow)
                    continue;

                DungeonNode node = new DungeonNode();

                node.row = row;
                node.column = col;

                node.position = new Vector2(
                    col * horizontalSpacing,
                    -row * verticalSpacing
                );

                // START
                if (col == 0 && row == middleRow)
                {
                    node.type = DungeonNodeType.Start;

                    startNode = node;
                }
                // BOSS
                else if (col == columns - 1 && row == middleRow)
                {
                    node.type = DungeonNodeType.Boss;

                    bossNode = node;
                }
                else
                {
                    node.type = DungeonNodeType.Normal;
                }

                grid[row, col] = node;

                allNodes.Add(node);
            }
        }
    }
    // =====================================
    // SPECIAL NODES
    // =====================================

    void AssignSpecialNodes()
    {
        AssignRandomNode(DungeonNodeType.Shop);

        AssignRandomNode(DungeonNodeType.Event);

        AssignRandomNode(DungeonNodeType.Elite);
    }

    void AssignRandomNode(DungeonNodeType type)
    {
        bool assigned = false;

        while (!assigned)
        {
            int row = UnityEngine.Random.Range(0, rows);

            int col = UnityEngine.Random.Range(1, columns - 1);

            DungeonNode node = grid[row, col];

            if (node == null)
                continue;

            if (node.type == DungeonNodeType.Normal)
            {
                node.type = type;

                assigned = true;
            }
        }
    }
    // =====================================
    // MAIN PATHS
    // =====================================

    void GenerateMainPaths()
    {
        // START -> COLUMN 1

        for (int row = 0; row < rows; row++)
        {
            DungeonNode first =
                grid[row, 1];

            if (first != null)
            {
                startNode.connections.Add(first);
            }
        }

        // CONNECT EVERY COLUMN
        for (int col = 1; col < columns - 2; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                DungeonNode current =
                    grid[row, col];

                if (current == null)
                    continue;

                ConnectNodeForward(current);
            }
        }

        // LAST COLUMN -> BOSS

        int lastPlayableColumn = columns - 2;

        for (int row = 0; row < rows; row++)
        {
            DungeonNode current =
                grid[row, lastPlayableColumn];

            if (current != null)
            {
                if (!current.connections.Contains(bossNode))
                {
                    current.connections.Add(bossNode);
                }
            }
        }
    }

    void ConnectNodeForward(DungeonNode current)
    {
        int nextColumn = current.column + 1;

        List<DungeonNode> possible =
            new List<DungeonNode>();

        // SAME ROW
        DungeonNode same =
            grid[current.row, nextColumn];

        if (same != null)
            possible.Add(same);

        // UP
        if (current.row > 0)
        {
            DungeonNode up =
                grid[current.row - 1, nextColumn];

            if (up != null)
                possible.Add(up);
        }

        // DOWN
        if (current.row < rows - 1)
        {
            DungeonNode down =
                grid[current.row + 1, nextColumn];

            if (down != null)
                possible.Add(down);
        }

        // GUARANTEE AT LEAST ONE CONNECTION
        DungeonNode guaranteed =
            possible[
                UnityEngine.Random.Range(0, possible.Count)
            ];

        current.connections.Add(guaranteed);

        // RANDOM EXTRA CONNECTIONS
        foreach (DungeonNode node in possible)
        {
            if (node == guaranteed)
                continue;

            if (UnityEngine.Random.value < 0.4f)
            {
                if (!current.connections.Contains(node))
                {
                    current.connections.Add(node);
                }
            }
        }
    }


    // =====================================
    // VISUALS
    // =====================================

    void SpawnVisuals()
    {
        foreach (DungeonNode node in allNodes)
        {
            GameObject obj =
                Instantiate(
                    nodePrefab,
                    nodeParent
                );

            RectTransform rt =
                obj.GetComponent<RectTransform>();

            rt.anchoredPosition = node.position;

            DungeonNodeView view =
                obj.GetComponent<DungeonNodeView>();

            view.Setup(node);
            node.view = view;
        }

        foreach (DungeonNode node in allNodes)
        {
            DrawConnections(node);
        }
    }

    // =====================================
    // LINES
    // =====================================

    void DrawConnections(DungeonNode node)
    {
        foreach (DungeonNode connected in node.connections)
        {
            GameObject obj =
                Instantiate(linePrefab, nodeParent);

            UILine line =
                obj.GetComponent<UILine>();

            Vector2 start = node.position;
            Vector2 end = connected.position;

            Vector2 dir = (end - start).normalized;

            // offset both ends away from node center
            start += dir * nodeRadius;
            end -= dir * nodeRadius;

            line.Setup(start, end);
        }
    }
}
