using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour, IAttacking
{
    public Transform player;
    public LayerMask enemy;

    public void Attack()
    {
        player = FindObjectOfType<BattlePlayerMovement>().transform;
        if (Physics.Raycast(player.position, player.forward, out RaycastHit hit, 3f, enemy) && Input.GetMouseButtonDown(0))
        {
            hit.transform.GetComponent<EnemyHandler>().TakeDamage(3);
        }
        
    }
}
