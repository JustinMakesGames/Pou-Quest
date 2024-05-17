using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    public Item[] items;
    public void Initialize(SaveData data)
    {
        for (int i = 0; i < data.dungeonX.Length; i++) 
        {
            Generation.instance.GenerateDungeonAtPos(new Vector3(data.dungeonX[i], 0, data.dungeonZ[i]));
        }

        foreach (int i in data.inventoryIds)
        {
            InventoryManager.instance.AddItem(items[i]);
        }
        //health and shit
    }
}
