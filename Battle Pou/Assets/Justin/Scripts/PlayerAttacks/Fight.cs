using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : InheritAnimator, IAttacking
{
    public Transform player;
    public LayerMask enemy;

    public void StartAttack()
    {
        StartCoroutine(SwordAttack());
        player = FindObjectOfType<BattlePlayerMovement>().transform;
    }
    
    public void UpdateAttack()
    {
        
    }

    private IEnumerator SwordAttack()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        print("Played attack");
        animator.SetTrigger("Attack");

        if (Physics.Raycast(player.position, player.forward, out RaycastHit hit, 90, enemy))
        {
            hit.transform.GetComponent<EnemyHandler>().TakeDamage(PlayerHandler.Instance.attackPower);

        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SwordAttack());
       

    }
    public void FinishAttack()
    {
        StopAllCoroutines();
    }
}

