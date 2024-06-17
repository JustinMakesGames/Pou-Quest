using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoins : MonoBehaviour
{
    public float rotateSpeed;

    private void Start()
    {
        Destroy(gameObject, 10);
    }
    private void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !FindObjectOfType<InvincibleFrames>().isInvincible)
        {
            PlayerHandler.Instance.coins++;
            PlayerHandler.Instance.StatsOverworldChange();
            Destroy(gameObject);
        }
    }
}
