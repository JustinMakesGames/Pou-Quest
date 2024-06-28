using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerWin : MonoBehaviour
{
    public GameObject winScreen;
    public Image blackScreen;
    public bool hasTriggeredWin;

    private void Start()
    {
        winScreen = GameObject.FindGameObjectWithTag("WinScreen").transform.GetChild(0).gameObject;
        blackScreen = GameObject.Find("BlackScreen").GetComponent<Image>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggeredWin)
        {
            hasTriggeredWin = true;
            FindObjectOfType<PlayerOverworld>().GetComponent<PlayerOverworld>().enabled = false;
            StartCoroutine(Fading());
        }
        
    }

    private IEnumerator Fading()
    {
        while (blackScreen.color.a < 1f)
        {
            blackScreen.color += new Color(0, 0, 0, Time.deltaTime);
            yield return null;
        }

        SetWinScreenActive();
        while (blackScreen.color.a > 0)
        {
            blackScreen.color -= new Color(0, 0, 0, Time.deltaTime);
            yield return null;
        }
    }

    private void SetWinScreenActive()
    {
        winScreen.SetActive(true);

    }
}
