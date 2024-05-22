using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCloneAttack : EnemyMoveAround
{
    public GameObject ghostClone;
    public int ghostAmount;
    public override void StartAttack()
    {
        base.StartAttack();
        for (int i = 0; i < ghostAmount; i++)
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
