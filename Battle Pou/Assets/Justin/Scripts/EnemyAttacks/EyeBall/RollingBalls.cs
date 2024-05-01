using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class RollingBalls : EnemyMoveAround
{
    public GameObject ball;
    private bool hasChosen;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    public float attackLength;
    public override void Attack()
    {
        base.Attack();
        PreparingToSpawn();
    }

    private void PreparingToSpawn()
    {
        if (!hasChosen)
        {
            int randomIndex = Random.Range(0, 4);
            SetPosition(randomIndex);
            SetRotation(randomIndex);
            StartCoroutine(SpawningProjectile());
        }
    }

    private void SetPosition(int index)
    {
        switch (index)
        {
            case 0:
                //Right side
                spawnPosition = new Vector3(
                battleArena.max.x,
                transform.position.y,
                Random.Range(battleArena.min.z, battleArena.max.z
                ));
                spawnRotation = Quaternion.Euler(0, -90, 0);
                break;

            case 1:
                //Left Side
                spawnPosition = new Vector3(
                battleArena.min.x,
                transform.position.y,
                Random.Range(battleArena.min.z, battleArena.max.z));
                break;

            case 2:
                //Down side
                spawnPosition = new Vector3(
                Random.Range(battleArena.min.x, battleArena.max.x),
                transform.position.y,
                battleArena.min.z);
                break;

            case 3:
                //Up side
                spawnPosition = new Vector3(
                Random.Range(battleArena.min.x, battleArena.max.x),
                transform.position.y,
                battleArena.max.z);
                break;
        }
    }

    private void SetRotation(int index)
    {
        switch (index)
        {
            case 0:
                spawnRotation = Quaternion.Euler(0, -90, 0);
                break;
            case 1:
                spawnRotation = Quaternion.Euler(0, 90, 0);
                break;
            case 2:
                spawnRotation = Quaternion.identity;
                break;
            case 3:
                spawnRotation = Quaternion.Euler(0, 180, 0);
                break;
        }
    }

    private IEnumerator SpawningProjectile()
    {
        Instantiate(ball, spawnPosition, spawnRotation);
        yield return new WaitForSeconds(attackLength);
        hasChosen = false;
    }


}
