using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBall : EnemyProjectile
{
    public float speed;
    public Rigidbody rb;
    private Vector3 dir;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        dir = transform.forward;
    }

    private void FixedUpdate()
    {
        rb.velocity = dir * stats.attackSpeed * Time.deltaTime;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BattlePlayer"))
        {
            PlayerHandler.Instance.TakeDamage(stats.attackPower);
        }
    }
}
