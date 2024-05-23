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
        StartCoroutine(HandlingEndOfTurn());
    }

    private IEnumerator HandlingEndOfTurn()
    {
        TurnAttackRendererOff();
        TurnPlayerMovementOff();
        PuttingObjectsAtPosition();
        RemovingAllProjectiles();
        bool areObjectsBack = false;
        while (!areObjectsBack)
        {
            areObjectsBack = AreObjectsAtStartPosition();
            yield return null;
        }
        PuttingStartRotation();
        BattleManager.instance.HandlingStates(BattleState.PlayerTurn);
    }

    private void TurnPlayerMovementOff()
    {
        player.GetComponent<BattlePlayerMovement>().enabled = false;
        playerAttack = null;
    }
    private void PuttingObjectsAtPosition()
    {
        player.GetComponent<NavMeshAgent>().enabled = true;
        player.GetComponent<NavMeshAgent>().SetDestination(playerStartPosition);
        enemy.GetComponent<NavMeshAgent>().SetDestination(enemyStartPosition);
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
        }
    }

    private void PuttingStartRotation()
    {
        player.rotation = Quaternion.identity;
        enemy.rotation = Quaternion.Euler(0, 180, 0);
        player.GetComponent<NavMeshAgent>().enabled = false;
    }

    private bool AreObjectsAtStartPosition()
    {
        float playerDistance = CalculatePlayerDistance();
        float enemyDistance = Vector3.Distance(enemy.position, enemyStartPosition);
        if (playerDistance < 0.05f && enemyDistance < 0.05f)
        {
            return true;
        }

        return false;
    }

    private float CalculatePlayerDistance()
    {
        Vector3 playerPosition = new Vector3(player.position.x, 0, player.position.z);
        Vector3 playerStartPos = new Vector3(playerStartPosition.x, 0, playerStartPosition.z);

        return Vector3.Distance(playerPosition, playerStartPos);
    }
}
