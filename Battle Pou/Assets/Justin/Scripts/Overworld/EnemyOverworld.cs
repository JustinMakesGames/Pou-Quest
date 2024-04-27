using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyOverworld : MonoBehaviour
{
    [SerializeField] private Transform player;
    private NavMeshAgent agent;
    private Vector3 startPosition;
    private float walkRange = 10;

    private Vector3 nextDestination;

    private Vector3 minPosition;
    private Vector3 maxPosition;
    private bool validPosition;

    //Checking for player
    private bool isChasing;
    private bool isPreparing;
    [SerializeField] private float radius;
    [SerializeField] private float angle;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float rotationSpeed;



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<MovementG>().transform;
    }

    private void Start()
    {
        startPosition = transform.position;
        minPosition = new Vector3(startPosition.x - walkRange, transform.position.y - walkRange, startPosition.z - walkRange);
        maxPosition = new Vector3(startPosition.x + walkRange, transform.position.y + walkRange, startPosition.z + walkRange);        
    }

    private void OnEnable()
    {
        StartCoroutine(GettingNextDestination());
    }

    private void Update()
    {
        if (!isChasing && !isPreparing)
        {
            CheckingForPlayer();
        }
        else if (isChasing)
        {
            ChasePlayer();
            CheckingOutOfRadius();
        }
        
        
    }

    //Patrolling around
    private IEnumerator GettingNextDestination()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            do
            {
                nextDestination = new Vector3(
                 Random.Range(startPosition.x - walkRange, startPosition.x + walkRange),
                 transform.position.y,
                 Random.Range(startPosition.z - walkRange, startPosition.z + walkRange));
                validPosition = IsNavMeshBaked();
                yield return null;
            } while (!validPosition);
            validPosition = false;
            MovingToDestination();

            yield return new WaitUntil(() => Vector3.Distance(transform.position, nextDestination) < 1f);
        }
    }

    private void MovingToDestination()
    {
        agent.SetDestination(nextDestination);
    }

    private void CheckingForPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, playerLayer);
        
        for (int i = 0; i < colliders.Length; i++)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;

            if (Vector3.Angle(transform.position, dirToPlayer) < angle / 2)
            {
                float playerdistance = Vector3.Distance(transform.position, player.position);

                if (!Physics.Raycast(transform.position, dirToPlayer,playerdistance,~playerLayer) && IsInPosition(player.position, minPosition, maxPosition))
                {                  
                    StopAllCoroutines();
                    StartCoroutine(PreparingForChase());
                    break;
                }
            }
        }
    }

    //Chasing
    private IEnumerator PreparingForChase()
    {
        print("Seen");
        isPreparing = true;
        agent.SetDestination(transform.position);
        float time = 0f;

        while (time < 1)
        {
            time += Time.deltaTime;
            Quaternion lookRotation = Quaternion.LookRotation((player.position - transform.position).normalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            yield return null;

        }
        isChasing = true;

    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void CheckingOutOfRadius()
    {
        if (!IsInPosition(player.position, minPosition, maxPosition))
        {
            StartCoroutine(PreparingForPatrolling());
        }
    }

    bool IsInPosition(Vector3 target, Vector3 min, Vector3 max)
    {
        return target.x >= min.x && target.x <= max.x &&
        target.y >= min.y && target.y <= max.y &&
        target.z >= min.z && target.z <= max.z;
    } 
    

    private IEnumerator PreparingForPatrolling()
    {
        isChasing = false;
        isPreparing = false;
        yield return new WaitForSeconds(1);
        StartCoroutine(GettingNextDestination());
    }
    private bool IsNavMeshBaked()
    {
        return NavMesh.SamplePosition(nextDestination, out NavMeshHit hit, 1f, NavMesh.AllAreas);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartBattle.instance.TriggeredEnemy();
        }
    }


}
