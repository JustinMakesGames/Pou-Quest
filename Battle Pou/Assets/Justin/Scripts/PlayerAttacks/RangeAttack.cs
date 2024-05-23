using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : Attacking
{
    public GameObject projectile;
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

            PlayAnimation();
            Instantiate(projectile, spawnPlace.position, player.rotation, transform);
            isReloading = true;
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(attackStats.attackInterval);
        isReloading = false;
    }

    private void PlayAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }
    public override void FinishAttack()
    {
        StopAllCoroutines();
        isReloading = false;
    }
}
