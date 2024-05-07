using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForDungeon : MonoBehaviour
{

    public bool doesDungeonExist;

    private void Start()
    {
        Destroy(gameObject, 2f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        doesDungeonExist = true;
    }
    private void OnCollisionStay(Collision collision)
    {
        doesDungeonExist = true;
    }
}
