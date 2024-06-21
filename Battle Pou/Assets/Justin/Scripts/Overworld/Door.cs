using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && transform.GetChild(1).GetComponent<Collider>().isTrigger == true)
        {
            animator.SetTrigger("PlayDoorAnimation");
            GetComponent<AudioSource>().Play();
        }
    }
}
