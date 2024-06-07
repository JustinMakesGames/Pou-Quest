using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public EnemyStats stats;
    public Transform player;
    public Transform enemy;
    protected virtual void Awake()
    {
        enemy = FindObjectOfType<EnemyHandler>().transform;
        stats = FindObjectOfType<EnemyHandler>().stats;
        player = FindObjectOfType<BattlePlayerMovement>().transform;
        
    }
}
