using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : Attacking
{
    public GameObject projectile;
    public Transform spawnPlace;
    public BattlePlayerMovement playerMovement;
    public Transform player;
    public bool isReloading;
    public Transform cam;
    public float rotateSpeed;

    public override void StartAttack()
    {
        cam = Camera.main.transform;
        playerMovement = FindObjectOfType<BattlePlayerMovement>();
        player = playerMovement.transform;
        spawnPlace = transform.GetChild(0);

        playerMovement.isUsingRangeAttack = true;
    }

    public override void UpdateAttack()
    {
        KeepCameraRotation();
        if (Input.GetMouseButtonDown(0) & !isReloading)
        {

            PlayAnimation();
            Instantiate(projectile, spawnPlace.position, player.rotation);
            isReloading = true;
            StartCoroutine(Reload());
        }
    }

    private void KeepCameraRotation()
    {
        print("IS PLAYING");
        player.eulerAngles = Vector3.Lerp(player.eulerAngles,
            new Vector3(player.eulerAngles.x, cam.eulerAngles.y, player.eulerAngles.z), rotateSpeed * Time.deltaTime);
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(attackStats.attackInterval);
        isReloading = false;
    }

    private void PlayAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }
    public override void FinishAttack()
    {
        StopAllCoroutines();
        isReloading = false;
        playerMovement.isUsingRangeAttack = false;
    }
}
