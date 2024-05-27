using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuy : MonoBehaviour
{
    public int price;
    public Item item;
    public void BuyItem()
    {
        if (PlayerHandler.Instance.coins < price)
        {
            return;
        }

        PlayerHandler.Instance.coins -= price;
        InventoryManager.instance.AddItem(item);
    }
}
