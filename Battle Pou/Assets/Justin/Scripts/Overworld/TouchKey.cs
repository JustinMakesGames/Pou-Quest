using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchKey : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("Touched key");
            FindObjectOfType<HasKey>().hasKey = true;
            Destroy(gameObject);
        }
    }
}
