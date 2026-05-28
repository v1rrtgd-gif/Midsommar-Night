using UnityEngine;

public class GatewayButton : MonoBehaviour
{
    private DungeonNode targetNode;

    public void Setup(DungeonNode node)
    {
        targetNode = node;
    }

    public void SelectNode()
    {
        DungeonRunManager.Instance
            .TravelToNode(targetNode);
    }
}