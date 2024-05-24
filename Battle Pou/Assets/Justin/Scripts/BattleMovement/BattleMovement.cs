using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerMovement : MonoBehaviour
{
    private float hor;
    private float vert;
    private float speed = 800;
    private float rotateSpeed = 5f;
    private Vector3 dir;
    private Transform cam;
    private Rigidbody rb;
    public Animator animator;

    public bool isUsingRangeAttack;
    
    private void OnEnable()
    {
        cam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        InputCheck();
        Animating();
    }

    private void Animating()
    {
        if (animator != null)
        {
            animator.SetFloat("Walking", dir.magnitude);

        }
    }

    private void FixedUpdate()
    {
        MovingAround();

        if (!isUsingRangeAttack)
        {
            Rotation();
        }
        
    }

    private void InputCheck()
    {
        hor = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");
        
    }
    
    private void MovingAround()
    {
        dir = (cam.forward * vert + cam.right * hor).normalized;
        dir.y = 0f;

        rb.velocity = dir * speed * Time.deltaTime;
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

    private void OnDisable()
    {
        if (animator != null)
        {
            animator.SetFloat("Walking", 0);
        }
        
    }
}
