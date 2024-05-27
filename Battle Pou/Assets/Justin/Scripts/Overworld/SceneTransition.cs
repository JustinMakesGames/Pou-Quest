using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Image blackScreen;
    public float blackScreenFadeSpeed;
    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SceneSwitch());
        }
    }

    private IEnumerator SceneSwitch()
    {
        while (blackScreen.color.a < 0.98f)
        {
            blackScreen.color += new Color(0, 0, 0, blackScreenFadeSpeed * Time.deltaTime);
            yield return null;
        }

        SceneManager.LoadScene(sceneName);

    }
}
