using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : EnemyMoveAround
{
   
    public float radius;
    public GameObject laser;
    public int amountOfLasers;
    public bool coroutineStarted;

    public float rotationSpeed;
    public override void StartAttack()
    {
        base.StartAttack();
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
        Vector3 directionToWatch = player.position - enemy.position;
        Quaternion rotation = Quaternion.LookRotation(directionToWatch);

        enemy.rotation = Quaternion.Lerp(enemy.rotation, rotation, rotationSpeed * Time.deltaTime);

    }

    private IEnumerator StartFiringLasers()
    {
        for (int i = 0; i < amountOfLasers; i++)
        {
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(0.6f);
            Instantiate(laser, enemy.position, enemy.rotation);
            yield return new WaitForSeconds(0.6f);
        }
        coroutineStarted = false;
        isMoving = false;

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
}
