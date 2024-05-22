using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostCloneBehaviour : MonoBehaviour
{
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
        player = FindObjectOfType<BattlePlayerMovement>().transform;
        agent = GetComponent<NavMeshAgent>();
        battleArena = FindObjectOfType<BattleManager>().GetComponent<Collider>().bounds;
        minPosition = battleArena.min;
        maxPosition = battleArena.max;
        enemyHandler = FindObjectOfType<EnemyHandler>();
    }

    private void Start()
    {
        isMoving = false;
    }

    private void Update()
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

    private void PositionReached()
    {
        if (Vector3.Distance(transform.position, destinationPoint) < 0.2f)
        {
            isMoving = false;
        }
    }

   private Vector3 ReturnPosition()
   {
        Vector3 destination = new Vector3(Random.Range(minPosition.x, maxPosition.x),
            transform.position.y,
            Random.Range(minPosition.z, maxPosition.z));
        return destination;
   }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            PlayerHandler.Instance.TakeDamage(PlayerHandler.Instance.attackPower);
        }
    }
}
