using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpinningPous : EnemyMoveAround
{
    public GameObject spinningPou;
    public GameObject indicator;

    public GameObject cloneIndicator;
    public List<GameObject> amountList;

    public override void StartAttack()
    {
        base.StartAttack();
        for (int i = 0; i < stats.secondAmountOfProjectiles; i++)
        {
            cloneIndicator = Instantiate(indicator, new Vector3
                (Random.Range(battleArena.min.x, battleArena.max.x),
                battleArena.max.y,
                Random.Range(battleArena.min.z, battleArena.max.z)), 
                Quaternion.identity);
            amountList.Add(cloneIndicator);
        }

        StartCoroutine(WaitTime());
    }

    public override void UpdateAttack()
    {
        base.UpdateAttack();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(1);

        for (int i = 0; i < amountList.Count;i++)
        {
            Instantiate(spinningPou, amountList[i].transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0));
            Destroy(amountList[i]);
        }
        amountList.Clear();
    }
}
