using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Load : MonoBehaviour
{
    public Item[] items;
    public Transform shopScreen;
    public void Initialize(SaveData data)
    {
        if (data != null)
        {
            if (data.inventoryIds != null && data.inventoryIds.Count > 0)
            {
                //for (int i = 0; i < data.inventoryIds.Count; i++)
                //{
                //    InventoryManager.instance.AddItem(items[data.inventoryIds[i]]);
                //    InventoryManager.instance.items[i].GetComponentInChildren<ItemInfo>().count = data.inventoryCount[i];
                //    Debug.Log(data.inventoryIds[i]);
                //}

                
            }

            foreach (Transform attack in shopScreen)
            {
                for (int i = 0; i < data.attacks.Count; i++)
                {
                    if (attack.GetComponent<AttackShop>().attack.GetComponent<Attacking>().attackStats.id == data.attacks[i])
                    {
                        attack.GetComponent<AttackShop>().SetAttackInInventory();
                    }
                }

            }
            foreach (int i in data.inventoryIds)
            {
                InventoryManager.instance.AddItem(items[i]);
                InventoryManager.instance.items[i].GetComponentInChildren<ItemInfo>().count = data.inventoryCount[i];
            }
        }
        PlayerHandler.Instance.level = data.level;
        PlayerHandler.Instance.attackPower = data.attackPower;
        PlayerHandler.Instance.hp = data.health;
        PlayerHandler.Instance.sp = data.sp;
        PlayerHandler.Instance.exp = data.exp;
        PlayerHandler.Instance.maxHp = data.maxHp;
        PlayerHandler.Instance.maxSp = data.maxSp;
        PlayerHandler.Instance.coins = data.coins;
        PlayerHandler.Instance.maxExp = data.maxExp;
        StartCoroutine(Delay(data));



    }

    IEnumerator Delay(SaveData data)
    {
        yield return null;
        ResManager.instance.FPSLimit(Convert.ToString(data.fpsLimit));
        ResManager.instance.currentResolutionIndex = data.resolution;
        ResManager.instance.SetResolution(data.resolution);
    }
    

}
