using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseManager : MonoBehaviour
{
    public static LoseManager instance;

    public GameObject loseUI;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void LoseManagement(GameObject player)
    {
        Destroy(player);

        Cursor.lockState = CursorLockMode.None;
        loseUI.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
