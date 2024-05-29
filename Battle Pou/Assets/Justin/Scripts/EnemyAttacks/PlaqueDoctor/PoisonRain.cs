using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonRain : EnemyMoveAround
{
    public GameObject projectile;
    public float range;
    private bool hasAttacked;
    
    public override void StartAttack()
    {
        base.StartAttack();
    }

    public override void UpdateAttack()
    {
        base.UpdateAttack();

        if (!hasAttacked)
        {
            SpawnAttack();
        }

    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    private void SpawnAttack()
    {
        Vector3 spawnPos = new Vector3(Random.Range(player.position.x - range, player.position.x + range), 
            battleArena.max.y, 
            Random.Range(player.position.z - range, player.position.z + range));
        Instantiate(projectile, spawnPos, Quaternion.identity);
        hasAttacked = true;

        StartCoroutine(AttackInterval());

    }
    private IEnumerator AttackInterval()
    {
        yield return new WaitForSeconds(stats.attackInterval);
        hasAttacked = false;
    }
}
