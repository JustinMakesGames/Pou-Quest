using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningPouBehaviour : EnemyProjectile
{
    private Vector3 direction;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        direction = transform.forward;
    }

    private void Update()
    {
        rb.velocity = direction * stats.secondAttackSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;

        direction = Vector3.Reflect(direction, normal);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            PlayerHandler.Instance.TakeDamage(stats.attackPower);
        }
    }
}
