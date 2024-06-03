using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventory;
    public GameObject inventoryMenu;
    public GameObject gObject;
    public List<GameObject> items = new List<GameObject>();
    public int maxStackSize;
    public static InventoryManager instance;

    void Start()
    {
        instance = this;
    }

    public void AddItem(Item item)
    {
        GameObject existingItem = null;
        restart:
        foreach (var v in items)
        {
            if (v != null)
            {
                if (v.GetComponentInChildren<ItemInfo>().id == item.id)
                {
                    if (v.GetComponentInChildren<ItemInfo>().count != maxStackSize)
                    {
                        existingItem = v;
                        break;
                    }
                }
            }
            else
            {
                items.Remove(v);
                goto restart;
            }
        }
        if (existingItem != null)
        {
            existingItem.GetComponentInChildren<ItemInfo>().count++;
            existingItem.GetComponentInChildren<TMP_Text>().text = existingItem.GetComponentInChildren<ItemInfo>().count.ToString();
        }
        else
        {
            GameObject newItem = Instantiate(gObject);
            newItem.GetComponentInChildren<Image>().sprite = item.itemSprite;
            newItem.transform.SetParent(inventory.transform, false);
            ItemInfo itemInfo = newItem.GetComponentInChildren<ItemInfo>();
            itemInfo.item = item;
            itemInfo.itemSprite = item.itemSprite;
            itemInfo.id = item.id;
            itemInfo.itemName = item.itemName;
            itemInfo.itemDescription = item.itemDescription;
            itemInfo.hpPlus = item.hp;
            itemInfo.spPlus = item.sp;
            newItem.name = item.itemName;
            
            items.Add(newItem);
            itemInfo.count = 1;
        }
    }

    public void UseItem(GameObject item)
    {
        
        item.GetComponentInChildren<ItemInfo>().count--;
        item.GetComponentInChildren<TMP_Text>().text = item.GetComponentInChildren<ItemInfo>().count.ToString();
        if (item.GetComponentInChildren<ItemInfo>().count <= 0)
        {
            items.Remove(item);
            Destroy(item);
        }
    }
}
