using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PoisonMoveAround : EnemyProjectile
{
    public float moveSpeed = 1.0f; // Speed of the gas movement
    public float changeDirectionTime = 2.0f;
    public Bounds boundsPlacements;// Time interval to change direction // Bounds for the gas movement

    private Vector3 targetPosition;
    private float timer;

    protected override void Awake()
    {
        base.Awake();
        boundsPlacements = FindObjectOfType<BattleManager>().GetComponent<Collider>().bounds;
    }
    private void Start()
    {
        SetRandomTargetPosition();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Change direction at intervals
        if (timer > changeDirectionTime)
        {
            SetRandomTargetPosition();
            timer = 0;
        }

        // Move towards the target position smoothly
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void SetRandomTargetPosition()
    {
        float x = Random.Range(boundsPlacements.min.x, boundsPlacements.max.x);
        float y = Random.Range(boundsPlacements.min.y, boundsPlacements.max.y);
        float z = Random.Range(boundsPlacements.min.z, boundsPlacements.max.z);

        targetPosition = new Vector3(x, y, z);
    }
}
