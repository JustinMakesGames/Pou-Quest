using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    public Item[] items;
    public void Initialize(SaveData data)
    {
        if (data != null)
        {
            //foreach (int i in data.inventoryIds)
            //{
            //    InventoryManager.instance.AddItem(items[i]);
            //}
            PlayerHandler.Instance.level = data.level;
            PlayerHandler.Instance.attackPower = data.attackPower;
            PlayerHandler.Instance.hp = data.health;
            PlayerHandler.Instance.sp = data.sp;
            PlayerHandler.Instance.exp = data.exp;
            PlayerHandler.Instance.maxHp = data.maxHp;
            PlayerHandler.Instance.maxSp = data.maxSp;
            PlayerHandler.Instance.coins = data.coins;
            PlayerHandler.Instance.maxExp = data.maxExp;
        }
    }
}
