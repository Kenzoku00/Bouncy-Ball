using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField]private GameObject UpperContainer;
    [SerializeField]private GameObject LowerContainerContent;
    [SerializeField]private GameObject card;
    private void Start()
    {
        foreach(skin information in skinManager.instance.skinInformation.skins)
        {
            card = Instantiate(card);
            card.GetComponent<ShopCardInformation>().setBallID(information.ID);
            card.transform.SetParent(UpperContainer.transform, false);
            Debug.Log(card);
        }
    }
}
