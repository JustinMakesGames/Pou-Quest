using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimations : MonoBehaviour
{
    public Animator animator;
    public float timer;
    public float endTimer;
    public Rigidbody rb;
    private void Update()
    {
        PlayIdleAnimations();
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
}
