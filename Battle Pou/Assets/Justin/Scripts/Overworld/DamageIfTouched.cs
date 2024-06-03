using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIfTouched : EnemyProjectile
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BattlePlayer"))
        {
            PlayerHandler.Instance.TakeDamage(stats.attackPower);
        }
    }
}
