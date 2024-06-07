using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.XR;

public class PouShootsLasers : EnemyMoveAround
{
    public GameObject laserShootingPou; 
    public override void StartAttack()
    {
        base.StartAttack();
        SpawnLaserShootingPous();
    }

    public override void UpdateAttack()
    {
        base.UpdateAttack();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    private void SpawnLaserShootingPous()
    {
        for (int i = 0; i < stats.thirdAmountOfProjectiles; i++)
        {
            Vector3 spawnPosition = Vector3.zero;
            int index = Random.Range(0, 4);

            switch (index)
            {
                case 0:
                    //Right side
                    spawnPosition = new Vector3(
                    battleArena.max.x,
                    transform.position.y,
                    Random.Range(battleArena.min.z, battleArena.max.z
                    ));
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

            Instantiate(laserShootingPou, spawnPosition, Quaternion.identity);
        }
    }
}

