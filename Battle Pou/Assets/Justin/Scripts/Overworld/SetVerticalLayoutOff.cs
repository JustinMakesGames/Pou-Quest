using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVerticalLayoutOff : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(SettingOff());
    }

    private IEnumerator SettingOff()
    {
        yield return null;
        yield return null;

        GetComponent<VerticalLayoutGroup>().enabled = false;
    }
}
