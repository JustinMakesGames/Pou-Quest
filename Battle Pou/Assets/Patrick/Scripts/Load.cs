using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Load : MonoBehaviour
{
    public Item[] items;
    public Transform shopScreen;
    public bool mainMenu;
    public void Initialize(SaveData data)
    {
        if (data != null)
        {
            //if (data.inventoryIds != null && data.inventoryIds.Count > 0)
            //{
            //for (int i = 0; i < data.inventoryIds.Count; i++)
            //{
            //    InventoryManager.instance.AddItem(items[data.inventoryIds[i]]);
            //    InventoryManager.instance.items[i].GetComponentInChildren<ItemInfo>().count = data.inventoryCount[i];
            //    Debug.Log(data.inventoryIds[i]);
            //}


            //}

            if (shopScreen != null)
            {
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
            }
            

            if (PlayerHandler.Instance != null)
            {
                if (data.inventoryIds.Count > 0)
                {
                    for(int i = 0; i < data.inventoryIds.Count; i++)
                    {
                        InventoryManager.instance.AddItem(items[i]);
                        
                    }

                    int count = 0;
                    foreach (GameObject item in InventoryManager.instance.items)
                    {
                        
                        item.GetComponentInChildren<ItemInfo>().count = data.inventoryCount[count];
                        count++;
                    }
                    
                }
                
                PlayerHandler.Instance.level = data.level;
                PlayerHandler.Instance.attackPower = data.attackPower;
                PlayerHandler.Instance.hp = data.maxHp;
                PlayerHandler.Instance.sp = data.maxSp;
                PlayerHandler.Instance.exp = data.exp;
                PlayerHandler.Instance.maxHp = data.maxHp;
                PlayerHandler.Instance.maxSp = data.maxSp;
                PlayerHandler.Instance.coins = data.coins;
                PlayerHandler.Instance.maxExp = data.maxExp;
            }



        }
        StartCoroutine(Delay(data));
        print("Loaded this data: " + data);


    }

    IEnumerator Delay(SaveData data)
    {
        yield return null;
        ResManager.instance.FPSLimit(Convert.ToString(data.fpsLimit));
        ResManager.instance.currentResolutionIndex = data.resolution;
        ResManager.instance.fullscreen = data.fullScreen;
        ResManager.instance.SetResolution(data.resolution);
    }
    

}
