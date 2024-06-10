using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    public string sceneName;
    public Image blackScreen;
    public void RestartGame()
    {
        sceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(Fading());
    }

    public void HUB()
    {
        sceneName = "HUB";
        StartCoroutine(Fading());
    }

    private IEnumerator Fading()
    {
        while (blackScreen.color.a < 1f)
        {
            blackScreen.color += new Color(0, 0, 0, Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }
}
