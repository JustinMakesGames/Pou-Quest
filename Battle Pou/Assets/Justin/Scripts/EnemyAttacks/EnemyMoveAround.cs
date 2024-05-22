using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveAround : Attacking
{
    public Transform enemy;
    public Transform player;
    protected NavMeshAgent agent;
    protected Vector3 minPosition;
    protected Vector3 maxPosition;

    protected Vector3 destinationPoint;
    protected Bounds battleArena;

    protected EnemyHandler enemyHandler;
    protected bool isMoving;

    private void Awake()
    {
        enemy = transform.parent;
        player = FindObjectOfType<BattlePlayerMovement>().transform;
        agent = transform.parent.GetComponent<NavMeshAgent>();
        battleArena = FindObjectOfType<BattleManager>().GetComponent<Collider>().bounds;
        minPosition = battleArena.min;
        maxPosition = battleArena.max;
        enemyHandler = transform.parent.GetComponent<EnemyHandler>();
    }

    public override void StartAttack()
    {
        isMoving = false;
    }

    public override void UpdateAttack()
    {
        if (!isMoving)
        {
            destinationPoint = ReturnPosition();
            agent.SetDestination(destinationPoint);
            isMoving = true;
        }
        else
        {
            PositionReached();
        }

    }

    public override void FinishAttack()
    {

    }

    protected virtual void PositionReached()
    {
        if (Vector3.Distance(transform.position, destinationPoint) < 0.2f)
        {
            isMoving = false;
        }
    }

    protected virtual Vector3 ReturnPosition()
    {
        Vector3 destination = new Vector3(Random.Range(minPosition.x, maxPosition.x),
            transform.position.y,
            Random.Range(minPosition.z, maxPosition.z));
        return destination;
    }


}
