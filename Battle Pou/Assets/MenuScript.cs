using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject menu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ReturnToGame()
    {
        menu.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
