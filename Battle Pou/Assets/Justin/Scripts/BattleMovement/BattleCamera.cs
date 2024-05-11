using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{
    private float hor;
    public float rotationSpeed;
    private Transform cam;

    private float height;
    private float distance;
    private float currentRotationAngle;
    private bool rightMouseClick;

    private void OnEnable()
    {
        cam = Camera.main.transform;
        distance = Vector3.Distance(cam.position, transform.position);
        height = cam.position.y - transform.position.y;
    }

    private void Update()
    {
        InputCheck();
        CursorLocked();
    }

    private void InputCheck()
    {
        hor = Input.GetAxis("Mouse X");
        rightMouseClick = Input.GetMouseButton(1);
    }

    private void CursorLocked()
    {
        if (rightMouseClick)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (rightMouseClick)
        {
            currentRotationAngle += hor * rotationSpeed * Time.deltaTime;
        }
        
        Quaternion rotation = Quaternion.Euler(0, currentRotationAngle, 0);
        Vector3 offset = rotation * new Vector3(0, height, -distance);
        cam.position = transform.position + offset;

        // Make the camera look at the target
        cam.LookAt(transform.position);
    }
}
