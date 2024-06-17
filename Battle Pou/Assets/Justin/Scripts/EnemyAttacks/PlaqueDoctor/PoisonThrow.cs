using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoisonThrow : EnemyMoveAround
{
    public GameObject rainProjectile;

    public override void StartAttack()
    {
        base.StartAttack();
        SpawnPlacements();
        StartCoroutine(Interval());
    }

    public override void UpdateAttack()
    {
        base.UpdateAttack();    
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
        StopAllCoroutines();
    }

    private void SpawnPlacements()
    {
        for (int i = 0; i < stats.secondAmountOfProjectiles; i++)
        {
            Instantiate(rainProjectile, GetPosition(), Quaternion.identity);
        }
    }

    private Vector3 GetPosition()
    {
        float xPosition = Random.Range(battleArena.min.x, battleArena.max.x);
        float zPosition = Random.Range(battleArena.min.z, battleArena.max.z);
        Vector3 positionToSpawn = new Vector3(xPosition, battleArena.max.y, zPosition);

        return positionToSpawn;
    }

    private IEnumerator Interval()
    {
        yield return new WaitForSeconds(stats.secondAttackInterval);
        SpawnPlacements();
        StartCoroutine(Interval());
    }
}
