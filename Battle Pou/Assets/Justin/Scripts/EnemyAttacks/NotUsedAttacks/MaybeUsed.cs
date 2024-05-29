using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class MaybeUsed : EnemyMoveAround
{
    private bool hasAttacked;
    public GameObject rainProjectile;
    public GameObject throwProjectile;

    public List<GameObject> cloneRainProjectiles;
    public List<GameObject> cloneThrowProjectiles;
    public float radius;
    
    public override void StartAttack()
    {
        
    }

    public override void UpdateAttack()
    {
        
    }

    public override void FinishAttack()
    {
        
    }
    protected override void PositionReached()
    {
        if (Vector3.Distance(transform.position, destinationPoint) < 0.2f)
        {
            SetRotationToPlayer();
            if (!hasAttacked)
            {
                StartCoroutine(PrepareAttack());
                hasAttacked = true;
            }

        }
    }

    private void SetRotationToPlayer()
    {
        float rotationSpeed = 20f;
        Vector3 lookRotation = player.position - enemy.position;
        Quaternion rotation = Quaternion.LookRotation(lookRotation);

        enemy.rotation = Quaternion.Lerp(enemy.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
    private IEnumerator PrepareAttack()
    {
        yield return new WaitForSeconds(0.5f);
        SpawnRainProjectiles();
        SpawnThrowProjectiles();
        yield return new WaitForSeconds(0.5f);
        isMoving = false;
        hasAttacked = false;
    }

    private void SpawnRainProjectiles()
    {
        cloneRainProjectiles.Clear();
        for (int i = 0; i < stats.secondAmountOfProjectiles; i++)
        {
            GameObject cloneRainProjectile = Instantiate(rainProjectile, SetRainPositionCorrectly(i), Quaternion.identity);
            cloneRainProjectiles.Add(cloneRainProjectile);
        }

    }

    private void SpawnThrowProjectiles()
    {
        cloneThrowProjectiles.Clear();

        for (int i = 0; i < stats.amountOfProjectiles; i++)
        {
            GameObject cloneThrowProjectile = Instantiate(throwProjectile, SetThrowPositionCorrectly(i) + Vector3.up * 0.3f, Quaternion.identity);
            cloneThrowProjectiles.Add(cloneThrowProjectile);
        }
    }

    private Vector3 SetRainPositionCorrectly(int i)
    {
        float angleStep = 360 / stats.secondAmountOfProjectiles;

        Vector3 centerPosition = enemy.position;
        float angle = i * angleStep;

        float angleRad = angle * Mathf.Deg2Rad;

        float x = centerPosition.x + Mathf.Cos(angleRad) * radius;
        float z = centerPosition.z + Mathf.Sin(angleRad) * radius;

        Vector3 spawnPosition = new Vector3(x, centerPosition.y, z);
        return spawnPosition;
    }

    private Quaternion SetRotation(int i)
    {
        Vector3 position = SetThrowPositionCorrectly(i);

        Vector3 lookRotation = position - enemy.position;

        Quaternion rotation = Quaternion.Euler(lookRotation);

        return rotation;
    }

    private Vector3 SetThrowPositionCorrectly(int i)
    {
        float angleStep = 90f / stats.amountOfProjectiles;
        Vector3 centerPosition = enemy.position;
        float angle = i * angleStep;

        // Calculate rotation around the Y-axis
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);

        // Get the forward direction of the enemy
        Vector3 forwardDirection = enemy.forward;

        // Calculate the offset position in the direction of the forward vector, rotated by the angle
        Vector3 offset = rotation * forwardDirection * radius;

        // Calculate the spawn position based on the center position and the offset
        Vector3 spawnPosition = centerPosition + offset;

        // Adjust the Y position to match the enemy's Y position
        spawnPosition.y = centerPosition.y;

        return spawnPosition;
    }
}
