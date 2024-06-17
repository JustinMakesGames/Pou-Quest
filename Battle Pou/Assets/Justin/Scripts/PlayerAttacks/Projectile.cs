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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == enemy)
        {
            enemy.GetComponent<EnemyHandler>().TakeDamage(attackStats.attackPower);
            Destroy(gameObject);
        }
    }
}
