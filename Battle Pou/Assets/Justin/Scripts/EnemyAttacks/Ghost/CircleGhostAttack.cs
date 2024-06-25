using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGhostAttack : EnemyMoveAround
{
    public GameObject ghostClone;
    public float radius;
    public List<GameObject> ghostClones;

    private bool hasSpawned;
    private bool hasMoven;

    private Vector3 spawnPosition;
    public override void StartAttack()
    {
        base.StartAttack();
    }

    public override void UpdateAttack()
    {
        base.UpdateAttack();

        if (!hasSpawned & !hasMoven)
        {
            SpawnClones();
            StartCoroutine(TimeInterval());
        }
        else if (!hasMoven && hasSpawned)
        {
            hasMoven = true;
        }

    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    private void SpawnClones()
    {
        ghostClones.Clear();
        for (int i = 0; i < stats.secondAmountOfProjectiles; i++)
        {
            GameObject spawnClone = Instantiate(ghostClone, CalculateAnglesToSpawn(i), CalculateRotation());
            ghostClones.Add(spawnClone);
        }
    }

    private Vector3 CalculateAnglesToSpawn(int i)
    {
        float angleStep = Random.Range(0,360);

        Vector3 centerPosition = player.position;
        float angle = i * angleStep;

        float angleRad = angle * Mathf.Deg2Rad;

        float x = centerPosition.x + Mathf.Cos(angleRad) * radius;
        float z = centerPosition.z + Mathf.Sin(angleRad) * radius;

        spawnPosition = new Vector3(x, centerPosition.y, z);
        return spawnPosition;
            
        

    }

    private void GetPositionAndRotation()
    {
        for (int i = 0; i < stats.secondAmountOfProjectiles; i++)
        {
            ghostClones[i].transform.position = CalculateAnglesToSpawn(i);
            ghostClones[i].transform.rotation = CalculateRotation();
        }
    }

    private Quaternion CalculateRotation()
    {
        Vector3 lookRotation = player.position - spawnPosition;
        Quaternion rotation = Quaternion.LookRotation(lookRotation);
        return rotation;
    }

    private IEnumerator TimeInterval()
    {
        hasSpawned = true;
        yield return new WaitForSeconds(stats.secondAttackInterval / 2);
        hasMoven = true;
        yield return new WaitForSeconds(stats.secondAttackInterval / 2 + 0.5f);
        RemovingAllProjectiles();
        hasSpawned = false;
        hasMoven = false;


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
