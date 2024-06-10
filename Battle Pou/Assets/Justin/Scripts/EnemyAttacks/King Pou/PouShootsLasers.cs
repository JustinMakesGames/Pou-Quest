using System.Collections;
using UnityEngine;

public class PouShootsLasers : EnemyMoveAround
{
    public GameObject laserShootingPou;
    public GameObject miniPou;

    public float radius;
    public override void StartAttack()
    {
        base.StartAttack();
        SpawnLaserShootingPous();
        StartCoroutine(SpawnMiniPous());
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

    private IEnumerator SpawnMiniPous()
    {
        Instantiate(miniPou,
            new Vector3(Random.Range(player.position.x - radius, player.position.x + radius),
            battleArena.max.y,
            Random.Range(player.position.z - radius, player.position.z + radius)),
            Quaternion.identity);
        yield return new WaitForSeconds(stats.secondAttackInterval);
        StartCoroutine(SpawnMiniPous());
    }
}

