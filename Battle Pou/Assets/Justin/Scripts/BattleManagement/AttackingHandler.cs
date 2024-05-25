using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackingHandler : StateHandler
{
    public override void HandleState()
    {
        StartCoroutine(Attacking());
    }

    private IEnumerator Attacking()
    {
        float time = 0f;
        player.GetComponent<BattlePlayerMovement>().enabled = true;
        enemyHandler.ChoosingAttack();
        yield return null;

        if (playerAttack != null)
        {
            player.GetComponent<MeshRenderer>().enabled = false;
            playerAttack.GetComponentInChildren<Renderer>().enabled = true;
            playerAttack.GetComponent<Attacking>().StartAttack();
            Poof.instance.UsePoof(player);

            player.GetComponent<BattlePlayerMovement>().animator = playerAttack.GetComponent<Attacking>().animator;
        }
        enemyAttack.GetComponent<Attacking>().StartAttack();
        while (time < attackingLength)
        {
            time += Time.deltaTime;
            if (playerAttack != null)
            {
                playerAttack.GetComponent<Attacking>().UpdateAttack();
            }

            if (enemy != null)
            {
                enemyAttack.GetComponent<Attacking>().UpdateAttack();
            }

            yield return null;
        }

        if (playerAttack != null)
        {
            playerAttack.GetComponent<Attacking>().FinishAttack();
        }
        enemyAttack.GetComponent<Attacking>().FinishAttack();
        HandlingEndOfTurn();
    }

    private void HandlingEndOfTurn()
    {
        TurnAttackRendererOff();
        TurnPlayerMovementOff();
        TurnEnemyMovementOff();
        RemovingAllProjectiles();
        playerHandler.StopImmunityFrames();
        BattleManager.instance.HandlingStates(BattleState.PlayerTurn);
    }

    private void TurnPlayerMovementOff()
    {
        player.GetComponent<BattlePlayerMovement>().enabled = false;
        playerAttack = null;
    }

    private void TurnEnemyMovementOff()
    {
        enemy.GetComponent<NavMeshAgent>().SetDestination(enemy.position);
    }

    private void RemovingAllProjectiles()
    {
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (GameObject projectile in projectiles)
        {
            Destroy(projectile);
        }
    }

    private void TurnAttackRendererOff()
    {
        if (playerAttack != null)
        {
            player.GetComponent<Renderer>().enabled = true;
            playerAttack.GetComponentInChildren<Renderer>().enabled = false;
            Poof.instance.UsePoof(player);
        }
    }
}
