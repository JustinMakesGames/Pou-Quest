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
            TurnPlayerRenderersOn();
            playerAttack.GetComponent<Attacking>().StartAttack();
            Poof.instance.UsePoof(player);

            player.GetComponent<BattlePlayerMovement>().animator = playerAttack.GetComponent<Attacking>().animator;
        }
        enemyAttack.GetComponent<Attacking>().StartAttack();

        StartCoroutine(BattleUI.instance.ShowTurnLength(attackingLength));
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

    private void TurnPlayerRenderersOn()
    {
        Renderer[] playerRenderers = player.GetChild(0).GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in playerRenderers)
        {
            renderer.enabled = false;
        }
        Renderer[] renderers = playerAttack.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = true;
        }
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
        BattleManager.instance.playerAttack = null;
        player.GetComponent<BattlePlayerMovement>().animator = player.GetChild(0).GetComponent<Animator>();
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
            Renderer[] playerRenderers = player.GetChild(0).GetComponentsInChildren<Renderer>();
            
            foreach (Renderer renderer in playerRenderers)
            {
                renderer.enabled = true;
            }

            Renderer[] renderers = playerAttack.GetComponentsInChildren<Renderer>();

            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = false;
            }
            Poof.instance.UsePoof(player);
        }
    }
}
