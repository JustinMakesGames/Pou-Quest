using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutismMovement : MonoBehaviour
{
    public Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Jump());
    }

    IEnumerator Jump()
    {
        rb.AddForce(new Vector3(0, 11, 0), ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Jump());
    }
}
