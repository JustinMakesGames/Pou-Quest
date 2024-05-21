using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAttack : InheritAnimator, IAttacking
{
    public GameObject cannonBall;
    public Transform spawnPlace;
    public Transform player;
    public void StartAttack()
    {
        player = FindObjectOfType<BattlePlayerMovement>().transform;
        spawnPlace = transform.GetChild(0);
    }

    public void UpdateAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(cannonBall, spawnPlace.position, player.rotation);
        }
    }

    public void FinishAttack()
    {

    }
}
