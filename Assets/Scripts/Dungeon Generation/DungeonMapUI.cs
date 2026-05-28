using UnityEngine;

public class DungeonMapUI : MonoBehaviour
{
    public GameObject panel;

    private bool open;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap();
        }
    }

    void ToggleMap()
    {
        open = !open;

        panel.SetActive(open);

        // optional pause
        Time.timeScale = open ? 0f : 1f;
    }
}