using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCardInformation : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI price;
    [SerializeField]private Image icon;
    private int id;

    public void setBallID(int idb) 
    {
        id = idb;
        icon = skinManager.instance.get_information(id).ballImage;
        price.text = skinManager.instance.get_information(id).price.ToString();
        if (generalManager.instance.playerInformation.inventory.ownedSkinID.Contains(id))
        {
            gameObject.GetComponent<Button>().enabled = false;
            price.text = "owned";
        }
    }

    public void buy()
    {
        if (generalManager.currency.star.spend(skinManager.instance.get_information(id).price)) {
            generalManager.inventory.skin.addItem(id);
            gameObject.GetComponent<Button>().enabled = false;
            price.text = "owned";
        };
    }
}
