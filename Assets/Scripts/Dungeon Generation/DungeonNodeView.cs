using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class DungeonNodeView : MonoBehaviour
{
    public UnityEngine.UI.Image image;

    public Button button;

    public DungeonNode node;

    Color originalColor;

    public void Setup(DungeonNode newNode)
    {
        node = newNode;

        switch (node.type)
        {
            case DungeonNodeType.Start:
                originalColor = Color.green;
                break;

            case DungeonNodeType.Normal:
                originalColor = Color.white;
                break;

            case DungeonNodeType.Shop:
                originalColor = Color.yellow;
                break;

            case DungeonNodeType.Event:
                originalColor = Color.cyan;
                break;

            case DungeonNodeType.Elite:
                originalColor = Color.red;
                break;

            case DungeonNodeType.Boss:
                originalColor = new Color(0.5f, 0f, 0f);
                break;
        }

        image.color = originalColor;

        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(OnClicked);

        Refresh();
    }

    public void Refresh()
    {
        // COMPLETED NODES
        if (node.cleared)
        {
            image.color = Color.black;
        }
        else
        {
            image.color = originalColor;
        }

        // ONLY ALLOW UNLOCKED NODES
        button.interactable = node.unlocked;
    }

    void OnClicked()
    {
        if (!node.unlocked)
            return;

        DungeonRunManager.Instance.TravelToNode(node);
    }
}