using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainingPous : EnemyProjectile
{
    private void Start()
    {
        Destroy(transform.parent.gameObject, 3f);
    }
    private void Update()
    {
        transform.Translate(Vector3.down * stats.attackSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            PlayerHandler.Instance.TakeDamage(stats.attackPower);
        }
    }
}
