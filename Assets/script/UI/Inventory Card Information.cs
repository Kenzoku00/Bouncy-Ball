using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCardInformation : MonoBehaviour
{
    [SerializeField]public int ballID;

    private void Start()
    {
        if (ballID == generalManager.instance.playerInformation.inventory.equippedSkinID)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void OnClick()
    {
        generalManager.inventory.skin.equipitem(ballID);
        gameObject.GetComponentInParent<InventoryUI>().refresh();
        gameObject.GetComponent<Button>().interactable = false;
    }
}
