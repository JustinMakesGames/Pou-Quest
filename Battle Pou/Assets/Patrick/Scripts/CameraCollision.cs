using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraCollision : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 0.3f))
        {
            if (hit.collider.CompareTag("Put"))
            {
                SceneManager.LoadScene("HUB");
            }

        }
    }
}
