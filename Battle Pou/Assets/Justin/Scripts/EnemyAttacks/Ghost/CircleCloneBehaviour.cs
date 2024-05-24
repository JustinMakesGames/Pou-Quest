using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCloneBehaviour : EnemyProjectile
{
    private bool isMoving;
    private void Start()
    {
        Destroy(gameObject, stats.secondAttackInterval);
        StartCoroutine(PreparingToMove());
    }

    private IEnumerator PreparingToMove()
    {
        yield return new WaitForSeconds(stats.secondAttackInterval / 2);
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            Moving();
        } 
        
    }

    private void Moving()
    {
        transform.Translate(Vector3.forward * stats.secondAttackSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BattlePlayer"))
        {
            PlayerHandler.Instance.TakeDamage(stats.attackPower);
        }
    }
}
