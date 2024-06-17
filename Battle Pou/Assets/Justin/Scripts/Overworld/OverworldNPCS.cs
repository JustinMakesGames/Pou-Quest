using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OverworldNPCS : MonoBehaviour
{
    private NavMeshAgent agent;
    public List<Transform> positions;
    public int index;
    private bool isMoving;

    public Transform positionsStored;

    private Animator animator;

    public bool isTalking;

    private Transform player;

    public Quaternion startRotation;
    public float rotationSpeed = 20f;
    private void Awake()
    {
        if (GetComponent<NavMeshAgent>() != null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        
        animator = GetComponentInChildren<Animator>();
        player = FindObjectOfType<PlayerOverworld>().transform;

        for (int i = 0; i < positionsStored.childCount; i++)
        {
            positions.Add(positionsStored.GetChild(i));
        }

        startRotation = transform.rotation;
    }

    private void Update()
    {
        PlayAnimations();

        if (!isTalking && positions.Count > 0)
        {
            if (!isMoving)
            {
                SetNewPosition();
            }
            else
            {
                CheckIfPositionReached();
            }
        }

        else if (isTalking && positions.Count <= 0)
        {
            TalkingRotation();
        }
        
    }

    private void PlayAnimations()
    {
        if (animator != null && agent != null)
        {
            animator.SetFloat("Walking", agent.velocity.magnitude);
        }
    }

    private void SetNewPosition()
    {
        agent.SetDestination(positions[index].position);
        isMoving = true;
    }

    private void CheckIfPositionReached()
    {
        if (Vector3.Distance(transform.position, positions[index].position) < 0.5f)
        {
            index++;
            
            if (index >= positions.Count)
            {
                index = 0;
            }

            isMoving = false;
        }
    }

    private void TalkingRotation()
    {
        
        Vector3 lookRotation = player.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(lookRotation);

        if (transform.rotation != rotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
        
    }

    public void IsTalkingToNPC()
    {
        if (!isTalking)
        {
            isTalking = true;
            GetComponent<DialogManager>().StartDialog();
            if (positions.Count > 0)
            {
                agent.SetDestination(transform.position);
            }
            
        }
        
    }

    public void FinishDialog()
    {
        if (positions.Count > 0)
        {
            agent.SetDestination(positions[index].position);
        }
        else
        {
            StartCoroutine(SetRotationRight());
        }

        isTalking = false;

        player.GetComponent<PlayerOverworld>().enabled = true;
    }

    private IEnumerator SetRotationRight()
    {
        while (transform.rotation != startRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, startRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }


}
