using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCloneAttack : EnemyMoveAround
{
    public GameObject ghostClone;
    public override void StartAttack()
    {
        base.StartAttack();
        for (int i = 0; i < stats.amountOfProjectiles; i++)
        {
            Instantiate(ghostClone, transform.position, transform.rotation);
        }
    }

    public override void UpdateAttack()
    {
        base.UpdateAttack();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}
