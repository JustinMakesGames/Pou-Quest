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
        Renderer[] renderers = transform.GetChild(0).GetComponentsInChildren<Renderer>();
        int amountOfFrames = 10;


        for (int i = 0; i < amountOfFrames; i++)
        {
            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = false;
            }
            
            yield return new WaitForSeconds(0.1f);
            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = true;
            }
            yield return new WaitForSeconds(0.1f);
        }

        isInvincible = false;
    }
}
