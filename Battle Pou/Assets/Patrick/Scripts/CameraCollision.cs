using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraCollision : MonoBehaviour
{
    public bool hasHit;
    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 0.3f))
        {
            if (hit.collider.CompareTag("Put") && !hasHit)
            {
                FindObjectOfType<SceneTransition>().StartSceneSwitch();
                hasHit = true;
            }

        }
    }
}
