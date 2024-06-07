using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : Attacking
{
    public Transform player;
    public LayerMask enemy;
    public bool hasHit;

    public override void StartAttack()
    {
        hasHit = false;
        StartCoroutine(SwordAttack());
        player = FindObjectOfType<BattlePlayerMovement>().transform;
    }
    
    public override void UpdateAttack()
    {
        Debug.DrawRay(player.position, player.forward * 3, Color.red);
    }

    private IEnumerator SwordAttack()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        print("Played attack");
        animator.SetTrigger("Attack");

        float timer = 0;

        
        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            if (Physics.Raycast(player.position, player.forward, out RaycastHit hit, 3, enemy) && !hasHit)
            {
                print("has hit");
                hit.transform.GetComponent<EnemyHandler>().TakeDamage(PlayerHandler.Instance.attackPower);
                hasHit = true;
  
            }
            yield return null;
        }
        hasHit = false;
        StartCoroutine(SwordAttack());
       

    }
    public override void FinishAttack()
    {
        StopAllCoroutines();
    }
}

