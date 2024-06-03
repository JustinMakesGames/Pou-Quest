using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPouDialog : MonoBehaviour
{
    public DialogManager pouDialog;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerOverworld>().enabled = false;
            pouDialog.StartDialog();
        }
    }
}
