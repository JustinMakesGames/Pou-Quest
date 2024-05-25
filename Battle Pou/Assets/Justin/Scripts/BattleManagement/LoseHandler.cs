using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseHandler : StateHandler
{
    public GameObject loseUI;

    public override void HandleState()
    {
        RemovingAllProjectiles();
        LoseManagement();
    }
    public void LoseManagement()
    {
        if (playerAttack!= null)
        {
            playerAttack.GetComponentInChildren<Renderer>().enabled = false;
        }
        else
        {
            player.GetComponent<Renderer>().enabled = false;
        }
        player.GetComponent<BattlePlayerMovement>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        loseUI.SetActive(true);
    }

    private void RemovingAllProjectiles()
    {
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (GameObject projectile in projectiles)
        {
            Destroy(projectile);
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
