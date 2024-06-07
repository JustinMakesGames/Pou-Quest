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
    private bool preparingToGoUp;

    public GameObject indicator;
    public GameObject cloneIndicator;

    private float originalSize;
    public GameObject miniPou;

    public GameObject getDamageObject;

    public GameObject cloneDamageObject;
    public float radius;

    private void Start()
    {
        print(gameObject.name);
    }
    public override void StartAttack()
    {
        
        originalSize = enemy.localScale.magnitude;
        originalYPosition = player.position.y;
        StartCoroutine(SpawnMiniPous());
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

        else if (!preparingToGoUp && isGoingDown)
        {
            GoingDown();
        }
 
    }

    public override void FinishAttack()
    {
        Destroy(cloneDamageObject);
        hasGrown = false;
        StopAllCoroutines();
        StartCoroutine(SetSizeBack());
        StartCoroutine(SetPositionOnGround());
    }

    private void JumpUp()
    {
        enemy.Translate(Vector3.up * upSpeed * Time.deltaTime);
        
        if (enemy.position.y > originalYPosition + 10)
        {
            cloneDamageObject = Instantiate(getDamageObject, enemy.position, Quaternion.identity, enemy);
            cloneIndicator = Instantiate(indicator, new Vector3(enemy.position.x, battleArena.max.y, enemy.position.z), Quaternion.identity);
            preparingToGoDown = true;

            StartCoroutine(DownInterval());

        }
    }

    private IEnumerator DownInterval()
    {
        yield return new WaitForSeconds(stats.attackInterval);
        Destroy(cloneIndicator); 
        isGoingDown = true;
    }

    private void GetPositionOfPlayer()
    {
        enemy.position = new Vector3(player.position.x, enemy.position.y, player.position.z);
        cloneIndicator.transform.position = new Vector3(enemy.position.x, battleArena.max.y, enemy.position.z);
    }

    private void GoingDown()
    {
        
        enemy.Translate(Vector3.down * upSpeed * Time.deltaTime);

        if (enemy.position.y <= originalYPosition && !preparingToGoUp)
        {
            StartCoroutine(IntervalUp());
            Destroy(cloneDamageObject);
        }
    }

    private IEnumerator IntervalUp()
    {
        preparingToGoUp = true;
        yield return new WaitForSeconds(stats.attackInterval);
        isGoingDown = false;
        preparingToGoDown = false;
        preparingToGoUp = false;
        

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

    private IEnumerator SetSizeBack()
    {
        while (enemy.localScale.magnitude > originalSize)
        {
            enemy.localScale -= new Vector3(growSpeed * Time.deltaTime, growSpeed * Time.deltaTime, growSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator SetPositionOnGround()
    {
        while (enemy.position.y > originalYPosition)
        {
            enemy.Translate(Vector3.down * upSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
