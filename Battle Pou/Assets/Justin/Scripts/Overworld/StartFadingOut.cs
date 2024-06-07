using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartFadingOut : MonoBehaviour
{
    public Image blackScreen;
    public float blackScreenFadeSpeed;
    private void Start()
    {
        StartCoroutine(StartingToFadeOut());
    }

    private IEnumerator StartingToFadeOut()
    {
        yield return new WaitForSeconds(1);
        while (blackScreen.color.a > 0)
        {
            blackScreen.color -= new Color(0, 0, 0, blackScreenFadeSpeed * Time.deltaTime);
            yield return null;
        }
    }


}
