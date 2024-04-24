using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float hor, vert;
    public Vector3 dir;
    public bool onGround;
    public float speed, jumpForce;
    public float verticalLookRotation;

    private void Update()
    {
        //camera
        //movement
        hor = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
        dir.x = hor;
        dir.z = vert;
        transform.Translate(dir * speed * Time.deltaTime);

        if (Input.GetButton("Jump") && onGround)
        {
            GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
            onGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        onGround = true;
    }
}
