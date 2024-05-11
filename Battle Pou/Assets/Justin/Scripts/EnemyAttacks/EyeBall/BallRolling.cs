using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BallRolling : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private EnemyHandler enemyHandler;
    private Vector3 dir;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        enemyHandler = FindObjectOfType<EnemyHandler>();
        dir = transform.forward;
    }

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void FixedUpdate()
    {
        rb.velocity = dir * speed * Time.deltaTime;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHandler.Instance.TakeDamage(enemyHandler.attackPower);
        }
    }
}
