using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public Transform canvas;
    public bool isAlreadyInteracting;
    public LayerMask interacting;
    public bool doorInteraction;
    public Transform door;
    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2, interacting) && !isAlreadyInteracting)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(hit.collider.transform.position);
            canvas.gameObject.SetActive(true);
            canvas.position = screenPosition;
        }
        else if (!doorInteraction)
        {
            canvas.gameObject.SetActive(false);
        }

        if (doorInteraction)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(door.position);
            canvas.gameObject.SetActive(true);
            canvas.position = screenPosition;
        }
    }

    
}
