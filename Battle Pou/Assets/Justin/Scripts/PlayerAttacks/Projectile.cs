using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform enemy;
    public AttackStats attackStats;

    private void Start()
    {
        enemy = FindObjectOfType<EnemyHandler>().transform;
        Destroy(gameObject, 5f);
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * attackStats.projectileSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, enemy.position) < 1)
        {
            enemy.GetComponent<EnemyHandler>().TakeDamage(PlayerHandler.Instance.attackPower * attackStats.attackPower);
            Destroy(gameObject);
        }
    }
}
