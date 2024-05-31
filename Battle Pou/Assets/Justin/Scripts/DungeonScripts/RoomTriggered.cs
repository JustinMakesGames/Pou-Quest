using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTriggered : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GetComponent<Cell>().tiles.Count != 0)
            {
                FindObjectOfType<EnableRoom>().enableThisRoom = GetComponent<Cell>().tiles[0].gameObject;
                FindObjectOfType<EnableRoom>().EnableThisRoom();
            }
            
        }
    }
}
