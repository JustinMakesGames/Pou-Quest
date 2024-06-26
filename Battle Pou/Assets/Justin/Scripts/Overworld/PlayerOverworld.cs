using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Rigidbody))]
public class PlayerOverworld : MonoBehaviour
{
   
    public float speed;
    public float rotateSpeed;
    private float hor, vert;
    private Vector3 dir;
    private Rigidbody rb;
    public GameObject inventory;
    public Animator animator;

    
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        InputCheck();
        OpeningInventory();
    }

    private void FixedUpdate()
    {
        Movement();
        Rotation();
    }

    private void InputCheck()
    {
        hor = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");
    }

    private void Movement()
    {
        dir = new Vector3(hor, 0, vert);
        rb.velocity = dir.normalized * speed * Time.deltaTime;
    }

    private void Rotation()
    {
        if (dir.magnitude > 0.02f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
        }
        else
        {
            Quaternion lookRotation = Quaternion.LookRotation(transform.forward);
            transform.rotation = lookRotation;
        }

    }

    private void OpeningInventory()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventory.SetActive(!inventory.activeInHierarchy);

            if (inventory.activeInHierarchy)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
        }
    }

}
