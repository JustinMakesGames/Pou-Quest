using EZhex1991.EZSoftBone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBones : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(EnableSoftBones());
    }

    private IEnumerator EnableSoftBones()
    {
        yield return null;
        GetComponent<EZSoftBone>().enabled = true;
    }
}
