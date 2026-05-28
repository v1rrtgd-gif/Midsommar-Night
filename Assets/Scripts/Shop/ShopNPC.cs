using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    private bool playerInside;

    void Update()
    {
        if (playerInside && Input.GetMouseButtonDown(0))
        {
            ShopManager.Instance.OpenShop();
        }
    }

    void OnMouseDown()
    {
        ShopManager.Instance.OpenShop();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}
