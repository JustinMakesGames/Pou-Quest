using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractingWithNPC : MonoBehaviour
{
    public LayerMask interactable;
    public int maxDistance;
    private void Update()
    {
        if (Input.GetButtonDown("NPCInteract") && Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance, interactable))
        {
            if (hit.transform.GetComponent<OverworldNPCS>() != null)
            {
                transform.GetComponent<PlayerOverworld>().enabled = false;
                FindObjectOfType<Interact>().isAlreadyInteracting = true;
                hit.transform.GetComponent<OverworldNPCS>().IsTalkingToNPC();
            }
            if (hit.collider.GetComponent<DialogManager>() != null)
            {
                transform.GetComponent<PlayerOverworld>().enabled = false;
                FindObjectOfType<Interact>().isAlreadyInteracting = true;
                hit.transform.GetComponent<DialogManager>().StartDialog();
            }
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                transform.GetComponent<PlayerOverworld>().enabled = false;
                FindObjectOfType<Interact>().isAlreadyInteracting = true;
                hit.collider.GetComponent<Interactable>().StartGambling();
            }
            
            
            
        } 
    }
}
