using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleFrames : MonoBehaviour
{
    public bool isInvincible;
    public void StartInvincibleFrames()
    {
        StartCoroutine(IsBeingInvincible());
    }

    private IEnumerator IsBeingInvincible()
    {
        isInvincible = true;
        Renderer renderer = GetComponent<Renderer>();
        int amountOfFrames = 10;

        for (int i = 0; i < amountOfFrames; i++)
        {
            renderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            renderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        isInvincible = false;
    }
}
