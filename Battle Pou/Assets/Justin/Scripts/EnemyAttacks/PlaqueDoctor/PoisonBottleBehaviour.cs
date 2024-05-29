using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBottleBehaviour : EnemyProjectile
{
    public float gravity;
    private Vector3 dir;
    private Rigidbody rb;

    public Transform target;
    private ParticleSystem explosion;
    private bool hasLanded;

    private Vector3 rotation;

    public bool isSecondAttack;

    public GameObject poisonGas;
    
    
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        explosion = GetComponentInChildren<ParticleSystem>();
        rotation = new Vector3(Random.Range(-1,2), Random.Range(-1,2), Random.Range(-1,2));
    }
    private void FixedUpdate()
    {
        if (!hasLanded)
        {
            UseGravity();
        }
    }

    private void UseGravity()
    {
        dir = new Vector3(0, -stats.attackSpeed, 0);
        rb.velocity = dir * Time.deltaTime;
        transform.Rotate(rotation);
    }

    private void Explode()
    {
        if (!isSecondAttack)
        {
            Destroy(target.gameObject);
            rb.velocity = Vector3.zero;
            hasLanded = true;
            GetComponent<MeshRenderer>().enabled = false;
            explosion.Play();
            Destroy(transform.parent.gameObject, 5f);
        }

        else
        {
            Instantiate(poisonGas, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            Explode();          
        }

        if (other.CompareTag("BattlePlayer"))
        {
            PlayerHandler.Instance.TakeDamage(stats.attackPower);
        }
     }
}
