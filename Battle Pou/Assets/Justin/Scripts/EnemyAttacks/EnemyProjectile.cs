using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public EnemyStats stats;
    public Transform player;

    protected virtual void Awake()
    {
        stats = FindObjectOfType<EnemyHandler>().stats;
        player = FindObjectOfType<BattlePlayerMovement>().transform;
    }
}
