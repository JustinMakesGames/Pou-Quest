using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPouDamage : EnemyProjectile
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            PlayerHandler.Instance.TakeDamage(stats.attackPower);
        }
    }
}
