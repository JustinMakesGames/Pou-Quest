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
    public Transform player;
    public float playerDistance;
    public float angle;
    public LayerMask playerLayer;
    public float rotationSpeed;

    public GameObject battleEnemy;

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
    



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerOverworld>().transform;
    }

    private void Start()
    {
        startPosition = transform.position;
        minPosition = new Vector3(startPosition.x - walkRange, transform.position.y - walkRange, startPosition.z - walkRange);
        maxPosition = new Vector3(startPosition.x + walkRange, transform.position.y + walkRange, startPosition.z + walkRange);
        print("Start");
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

    private bool IsNavMeshBaked()
    {
        return NavMesh.SamplePosition(nextDestination, out NavMeshHit hit, 1f, NavMesh.AllAreas);
    }

    private void MovingToDestination()
    {
        agent.SetDestination(nextDestination);
    }

    private void CheckingForPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < playerDistance && CanObjectBeSeen())
        {
            StopAllCoroutines();
            StartCoroutine(PreparingForChase());
        }
    }

    
    private bool CanObjectBeSeen()
    {
        Vector3 directionToPlayer = player.position - transform.position;

        float dotProduct = Vector3.Dot(directionToPlayer, transform.forward);

        if (dotProduct > 0.2f)
        {
            print("True yes xd");
            return true;
        }

        return false;
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

    private bool IsInPosition(Vector3 target, Vector3 min, Vector3 max)
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
    

    private void OnDisable()
    {
        agent.SetDestination(transform.position);
        StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BattleTransition.instance.StartBattle();
            CreateBattleArena.instance.SpawnEnemy(battleEnemy);
            EndBattle.instance.GetEnemy(gameObject);
        }
    }


}
