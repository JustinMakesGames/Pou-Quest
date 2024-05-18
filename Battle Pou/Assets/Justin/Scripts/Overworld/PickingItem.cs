using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingItem : MonoBehaviour
{
    public Item item;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.instance.AddItem(item);
            Destroy(gameObject);
        }
    }
}
