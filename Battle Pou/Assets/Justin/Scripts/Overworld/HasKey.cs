using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class HasKey : MonoBehaviour
{
    public bool hasKey;
    public LayerMask door;
    public GameObject keyPanel;

    private void Start()
    {
        keyPanel = GameObject.FindGameObjectWithTag("Key");
    }
    private void Update()
    {
        if (hasKey)
        {
            keyPanel.transform.GetChild(0).gameObject.SetActive(true);
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
                FindObjectOfType<Interact>().doorInteraction = true;
                FindObjectOfType<Interact>().door = hit.transform;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hasKey = false;
                    keyPanel.transform.GetChild(0).gameObject.SetActive(false);
                    StartCoroutine(LockAnimation(hit.transform));
                    FindObjectOfType<Interact>().doorInteraction = false;
                    
                }
            }
        }
        else
        {
            FindObjectOfType<Interact>().doorInteraction = false;
        }
    }

    private IEnumerator LockAnimation(Transform obj)
    {
        obj.GetChild(2).GetComponent<Animator>().SetTrigger("OpenLock");
        yield return new WaitForSeconds(2);
        obj.GetComponent<Collider>().isTrigger = true;
    }
}
