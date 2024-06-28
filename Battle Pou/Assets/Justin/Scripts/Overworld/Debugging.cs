using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Debugging : MonoBehaviour
{
    public GameObject eyeBall;
    public GameObject ghosty;
    public GameObject plaqueDoctor;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            GameObject eyeBallClone = Instantiate(eyeBall, transform.position + transform.forward * 2, Quaternion.identity);
            Destroy(eyeBallClone.GetComponent<NavMeshAgent>());
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            GameObject ghostyClone = Instantiate(ghosty, transform.position + transform.forward * 2, Quaternion.identity);
            Destroy(ghostyClone.GetComponent<NavMeshAgent>());
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            GameObject plaqueDoctorClone = Instantiate(plaqueDoctor, transform.position + transform.forward * 2, Quaternion.identity);
            Destroy(plaqueDoctorClone.GetComponent<NavMeshAgent>());
        }
    }


}
