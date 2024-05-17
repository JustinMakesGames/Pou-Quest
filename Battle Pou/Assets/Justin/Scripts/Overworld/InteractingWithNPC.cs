using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractingWithNPC : MonoBehaviour
{
    public LayerMask npc;
    public int maxDistance;
    private void Update()
    {
        if (Input.GetButtonDown("NPCInteract") & Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance, npc))
        {
            hit.transform.GetComponent<DialogManager>().StartDialog();
            transform.GetComponent<PlayerOverworld>().enabled = false;
        } 
    }
}
