using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAttack : Attacking
{
    public GameObject cannonBall;
    public Transform spawnPlace;
    public Transform player;
    public bool isReloading;
    public override void StartAttack()
    {
        player = FindObjectOfType<BattlePlayerMovement>().transform;
        spawnPlace = transform.GetChild(0);
    }

    public override void UpdateAttack()
    {
        if (Input.GetMouseButtonDown(0) & !isReloading)
        {
            Instantiate(cannonBall, spawnPlace.position, player.rotation);
            isReloading = true;
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(2);
        isReloading = false;
    }

    public override void FinishAttack()
    {
        StopAllCoroutines();
        isReloading = false;
    }
}
