using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private Transform player;
    

    void Start()
    {
        player = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(player.position, player.up, rotationSpeed * Time.deltaTime);
    }
}
