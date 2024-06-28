using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerBackToMoving : MonoBehaviour
{
    public void SetPlayerMovingOn()
    {
        FindObjectOfType<PlayerOverworld>().GetComponent<PlayerOverworld>().enabled = true;
    }
}
