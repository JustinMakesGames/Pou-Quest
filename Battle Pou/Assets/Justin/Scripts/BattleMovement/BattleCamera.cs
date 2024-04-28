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

    private void Update()
    {
        InputCheck();
    }

    private void InputCheck()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.RotateAround(player.position, player.up, rotationSpeed * Time.deltaTime);
    }
}
