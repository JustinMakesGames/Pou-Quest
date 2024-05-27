using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingCameraPlacement : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CamMovement.instance.ChangeCollider();
        }
    }
}
