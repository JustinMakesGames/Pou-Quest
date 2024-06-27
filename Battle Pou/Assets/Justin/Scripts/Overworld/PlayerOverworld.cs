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
    public float timer;
    public float endTimer;

    
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        InputCheck();
        OpeningInventory();
        PlayIdleAnimations();
        PlayWalkingAnimations();
        
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

    private void PlayIdleAnimations()
    {
        if (rb.velocity.magnitude < 0.1f)
        {
            timer += Time.deltaTime;

            if (timer > endTimer)
            {
                timer = 0;
                int randomIdleAnimation = Random.Range(1, 4);

                animator.SetTrigger("PlayIdle" + randomIdleAnimation.ToString());
            }
        }

        else
        {
            timer = 0;
        }
    }

    private void PlayWalkingAnimations()
    {
        animator.SetFloat("Walking", rb.velocity.magnitude);
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
