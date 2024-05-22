using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float speed;
    public Transform enemy;

    private void Start()
    {
        enemy = FindObjectOfType<EnemyHandler>().transform;
        Destroy(gameObject, 5f);
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, enemy.position) < 1)
        {
            enemy.GetComponent<EnemyHandler>().TakeDamage(PlayerHandler.Instance.attackPower);
            Destroy(gameObject);
        }
    }
}
