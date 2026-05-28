using System.Collections.Generic;
using UnityEngine;

public class DungeonNode
{
    public int row;
    public int column;

    public DungeonNodeType type;
    public DungeonNodeView view;

    public Vector2 position;

    public List<DungeonNode> connections =
        new List<DungeonNode>();

    public bool visited;
    public bool cleared;
    public bool unlocked;
}
