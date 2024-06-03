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
        if (Input.GetKeyDown(KeyCode.E) & Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2f, door))
        {
            print(hit.transform.name);
            if (hit.collider.gameObject == hit.transform.parent.GetComponent<Tile1>().inDoor)
            {
                hasKey = false;
                StartCoroutine(LockAnimation(hit.transform));               
            }
        }
    }

    private IEnumerator LockAnimation(Transform obj)
    {
        obj.GetChild(2).GetComponent<Animator>().SetTrigger("OpenLock");
        yield return new WaitForSeconds(2);
        obj.GetComponent<Collider>().isTrigger = true;
    }
}
