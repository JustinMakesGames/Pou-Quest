using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class HasKey : MonoBehaviour
{
    public bool hasKey;
    public LayerMask door;

    private void Update()
    {
        if (hasKey)
        {
            UseKey();
        }
    }

    private void UseKey()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2, door)) 
        {

            print(hit.transform.name);
            if (hit.collider.gameObject == hit.transform.parent.GetComponent<Tile1>().inDoor)
            {
                FindObjectOfType<EToInteract>().doorInteraction = true;
                FindObjectOfType<EToInteract>().door = hit.transform;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hasKey = false;
                    StartCoroutine(LockAnimation(hit.transform));
                    FindObjectOfType<EToInteract>().doorInteraction = false;
                    
                }
            }
        }
        else
        {
            FindObjectOfType<EToInteract>().doorInteraction = false;
        }
    }

    private IEnumerator LockAnimation(Transform obj)
    {
        obj.GetChild(2).GetComponent<Animator>().SetTrigger("OpenLock");
        yield return new WaitForSeconds(2);
        obj.GetComponent<Collider>().isTrigger = true;
    }
}
