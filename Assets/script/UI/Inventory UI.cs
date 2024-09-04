using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryCard;

    public void setBallInventoryUI()
    {
        if (gameObject.GetComponentsInChildren<Button>().Length > 0)
        {
            foreach (Button button in gameObject.GetComponentsInChildren<Button>())
            {
                Destroy(button.gameObject);
            }
        }
        foreach (int id in generalManager.instance.playerInformation.inventory.ownedSkinID)
        {
            GameObject card = generalManager.Instantiate(inventoryCard);
            card.transform.parent = gameObject.transform;
            card.GetComponent<InventoryCardInformation>().ballID = id;
        }
    }

    public void refresh()
    {
        Button[] button = gameObject.GetComponentsInChildren<Button>();
        foreach (Button b in button)
        {
            b.interactable = true;
        }
    }
}
