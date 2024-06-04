using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GroundPound : EnemyMoveAround
{
    public float maxScale;
    public float growSpeed;

    private bool hasGrown;

    private float originalYPosition;

    public float upSpeed;

    private bool preparingToGoDown;

    private bool isGoingDown;

    public override void StartAttack()
    {
        originalYPosition = player.position.y;
        StartCoroutine(GrowPou());   
    }

    private IEnumerator GrowPou()
    {
        while (enemy.localScale.magnitude < maxScale)
        {
            enemy.localScale += new Vector3(growSpeed * Time.deltaTime, growSpeed * Time.deltaTime, growSpeed * Time.deltaTime);
            yield return null;
        }

        hasGrown = true;
    }

    public override void UpdateAttack()
    {
        if (hasGrown && !preparingToGoDown)
        {
            JumpUp();
        }

        else if (preparingToGoDown && !isGoingDown)
        {
            GetPositionOfPlayer();
        }

        else
        {
            GoingDown();
        }
 
    }

    public override void FinishAttack()
    {
        
    }

    private void JumpUp()
    {
        enemy.Translate(Vector3.up * upSpeed * Time.deltaTime);
        
        if (enemy.position.y > originalYPosition + 10)
        {
            preparingToGoDown = true;
            StartCoroutine(DownInterval());

        }
    }

    private IEnumerator DownInterval()
    {
        yield return new WaitForSeconds(stats.attackInterval);
        isGoingDown = true;
    }

    private void GetPositionOfPlayer()
    {
        enemy.position = new Vector3(player.position.x, enemy.position.y, player.position.z);
    }

    private void GoingDown()
    {
        enemy.Translate(Vector3.down * upSpeed * Time.deltaTime);

        if (enemy.position.y <= originalYPosition)
        {
            StartCoroutine(IntervalUp());
        }
    }

    private IEnumerator IntervalUp()
    {
        yield return new WaitForSeconds(stats.attackInterval);
        preparingToGoDown = false;
        isGoingDown = false;

    }
}
