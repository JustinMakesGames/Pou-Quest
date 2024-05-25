using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poof : MonoBehaviour
{
    public static Poof instance;
    public GameObject poofObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void UsePoof(Transform objectPos)
    {
        GameObject poofClone = Instantiate(poofObject, objectPos.position, Quaternion.identity);
        poofClone.transform.localScale = objectPos.lossyScale;
        Destroy(poofClone, 1f);
        StartCoroutine(KeepParticlesAtPosition(poofClone.transform, objectPos));
    }

    private IEnumerator KeepParticlesAtPosition(Transform particles, Transform objectPos)
    {
        while (particles != null)
        {
            if (objectPos != null)
            {
                particles.position = objectPos.position;
            }          
            yield return null;
        }
    }
}
