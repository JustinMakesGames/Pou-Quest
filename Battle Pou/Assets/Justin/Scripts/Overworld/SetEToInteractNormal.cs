using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEToInteractNormal : MonoBehaviour
{
    public void SetEToNormal()
    {
        FindObjectOfType<Interact>().isAlreadyInteracting = false;
    }
}
