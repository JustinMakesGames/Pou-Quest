using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : EnemyProjectile
{
    public float speed;
    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.forward * stats.secondAttackSpeed * Time.deltaTime);
    }
}
