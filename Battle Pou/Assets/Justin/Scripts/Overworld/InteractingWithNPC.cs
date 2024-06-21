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
                hit.transform.GetComponent<OverworldNPCS>().IsTalkingToNPC();
                transform.GetComponent<PlayerOverworld>().enabled = false;
                FindObjectOfType<EToInteract>().isAlreadyInteracting = true;
            }
            if (hit.collider.GetComponent<DialogManager>() != null)
            {
                hit.transform.GetComponent<DialogManager>().StartDialog();
                transform.GetComponent<PlayerOverworld>().enabled = false;
                FindObjectOfType<EToInteract>().isAlreadyInteracting = true;
            }
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                hit.collider.GetComponent<Interactable>().StartGambling();
                transform.GetComponent<PlayerOverworld>().enabled = false;
                FindObjectOfType<EToInteract>().isAlreadyInteracting = true;
            }
            
            
            
        } 
    }
}
