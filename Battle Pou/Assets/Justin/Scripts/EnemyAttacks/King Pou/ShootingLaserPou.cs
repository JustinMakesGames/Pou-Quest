using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingLaserPou : EnemyProjectile
{
    public GameObject laser;
    public float rotationSpeed;

    private bool isAttacking;
    private void Start()
    {
        StartCoroutine(ShootLaser());
    }

    private void Update()
    {
        if (!isAttacking)
        {
            Vector3 positionToSpawn = player.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(positionToSpawn);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
        
    }

    private IEnumerator ShootLaser()
    {
        yield return new WaitForSeconds(stats.thirdAttackInterval);
        Instantiate(laser, transform.position, transform.rotation);
        StartCoroutine(ShootLaser());
    }
}
