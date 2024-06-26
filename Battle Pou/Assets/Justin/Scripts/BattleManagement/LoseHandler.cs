using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseHandler : StateHandler
{
    public GameObject loseUI;

    private void Start()
    {
        loseUI = GameObject.FindGameObjectWithTag("WinScreen").transform.GetChild(1).gameObject;
    }
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
        enemyAttack.GetComponent<Attacking>().FinishAttack();

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
}
