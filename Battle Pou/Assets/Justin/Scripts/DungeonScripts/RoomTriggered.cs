using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTriggered : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GetComponent<Cell1>().tiles.Count != 0)
            {
                FindObjectOfType<EnableRoom>().enableThisRoom = GetComponent<Cell1>().tiles[0].gameObject;
                FindObjectOfType<EnableRoom>().EnableThisRoom();
            }
            
        }
    }
}
