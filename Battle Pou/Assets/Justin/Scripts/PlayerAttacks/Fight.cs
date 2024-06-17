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
        GameObject.FindGameObjectWithTag("Audio").GetComponentAtIndex<AudioSource>(6).Play();
        float timer = 0;

        
        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            if (Physics.Raycast(player.position, player.forward, out RaycastHit hit, 3, enemy) && !hasHit)
            {
                print("has hit");
                if (hit.transform.GetComponent<EnemyHandler>() != null)
                {
                    hit.transform.GetComponent<EnemyHandler>().TakeDamage(attackStats.attackPower);
                }

                hasHit = true;
  
            }
            yield return null;
        }

        yield return new WaitForSeconds(attackStats.attackInterval);
        hasHit = false;
        StartCoroutine(SwordAttack());
       

    }
    public override void FinishAttack()
    {
        StopAllCoroutines();
    }
}

