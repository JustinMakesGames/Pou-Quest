using System.Collections;
using UnityEngine;

public class LaserAttack : EnemyMoveAround
{
   
    public float radius;
    public GameObject laser;
    public GameObject sideLaser;
    public int amountOfLasers;
    public bool coroutineStarted;

    public float rotationSpeed;

    public float offsetAmount;
    private Vector3 directionToWatch;

    public Vector3 spawnPosition;
    public override void StartAttack()
    {
        base.StartAttack();
        StartCoroutine(SpawnSideLasers());
    }

    public override void UpdateAttack()
    {
        base.UpdateAttack();
        
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
        StopAllCoroutines();
        coroutineStarted = false;
    }

    protected override void PositionReached()
    {
        if (Vector3.Distance(transform.position, destinationPoint) < 0.2f)
        {
            if (!coroutineStarted)
            {
                StartCoroutine(StartFiringLasers());
                coroutineStarted = true;
            }
            WatchPlayer();          
        }
    }

    private void WatchPlayer()
    {
        directionToWatch = SetRandomPosition();
        Quaternion rotation = Quaternion.LookRotation(directionToWatch);

        enemy.rotation = Quaternion.Lerp(enemy.rotation, rotation, rotationSpeed * Time.deltaTime);

    }

    private IEnumerator StartFiringLasers()
    {
        for (int i = 0; i < stats.amountOfProjectiles; i++)
        {
            SetRandomOffset();
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(stats.attackInterval / 2);
            Instantiate(laser, enemy.position, enemy.rotation);
            AudioRef.instance.laser.Play();
            yield return new WaitForSeconds(stats.attackInterval / 2);
            
        }
        coroutineStarted = false;
        isMoving = false;

    }

    private void SetRandomOffset()
    {
        int chosenOne = Random.Range(0, 2);

        if (chosenOne == 0)
        {
            offsetAmount = 0f;
        }
        else
        {
            offsetAmount = Random.Range(-3f, 3f);
        }
        
    }
    private Vector3 SetRandomPosition()
    {
        Vector3 direction = player.position - enemy.position;

        direction.y = 0;
        Vector3 generalDirection = Vector3.Cross(direction, Vector3.up).normalized;

        Vector3 offset = generalDirection * offsetAmount;
        Vector3 directionWithOffset = direction + offset;

        Vector3 normalizedVersion = directionWithOffset.normalized;

        return normalizedVersion;

    }
    protected override Vector3 ReturnPosition()
    {
        Vector3 destination = new Vector3(Random.Range(transform.position.x - radius, transform.position.x + radius),
            transform.position.y,
            Random.Range(transform.position.z - radius, transform.position.z + radius));
        destination.x = Mathf.Clamp(destination.x, minPosition.x, maxPosition.x);
        destination.z = Mathf.Clamp(destination.z, minPosition.z, maxPosition.z);
        return destination;
    }

    private IEnumerator SpawnSideLasers()
    {
        yield return new WaitForSeconds(stats.thirdAttackInterval);
        CalculateSide();
        Instantiate(sideLaser, spawnPosition, Quaternion.identity);
        StartCoroutine(SpawnSideLasers());
    }
    private void CalculateSide()
    {
        
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
    }
}
