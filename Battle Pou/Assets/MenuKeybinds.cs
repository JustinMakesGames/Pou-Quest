using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuKeybinds : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            InventoryManager.instance.inventoryMenu.SetActive(true);
        }
        if (Input.GetButtonDown("Menu"))
        {

        }
    }
}
