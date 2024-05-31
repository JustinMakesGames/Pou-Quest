using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnableRoom : MonoBehaviour
{
    public GameObject enableThisRoom;
    public void EnableThisRoom()
    {
        Tile[] tiles = FindObjectsOfType<Tile>();

        foreach (Tile tile in tiles)
        {
            if (tile.gameObject != enableThisRoom)
            {
                tile.gameObject.SetActive(false);
            }
        }

        enableThisRoom.SetActive(true);

        Camera.main.GetComponent<CamMovement>().ChangeCollider(enableThisRoom.GetComponentInChildren<Floor>().GetComponent<Collider>());
    }
}
