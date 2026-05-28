using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class DungeonNodeUI : MonoBehaviour
{
    public UnityEngine.UI.Image image;

    public void Setup(DungeonNode node)
    {
        switch (node.type)
        {
            case DungeonNodeType.Start:
                image.color = Color.green;
                break;

            case DungeonNodeType.Normal:
                image.color = Color.white;
                break;

            case DungeonNodeType.Shop:
                image.color = Color.yellow;
                break;

            case DungeonNodeType.Event:
                image.color = Color.cyan;
                break;

            case DungeonNodeType.Elite:
                image.color = Color.red;
                break;

            case DungeonNodeType.Boss:
                image.color = new Color(0.5f, 0f, 0f);
                break;
        }
    }
}
