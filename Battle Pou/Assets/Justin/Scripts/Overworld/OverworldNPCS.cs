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
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        for (int i = 0; i < positionsStored.childCount; i++)
        {
            positions.Add(positionsStored.GetChild(i));
        }
    }

    private void Update()
    {
        PlayAnimations();
        if (!isMoving)
        {
            SetNewPosition();
        }
        else
        {
            CheckIfPositionReached();
        }
    }

    private void PlayAnimations()
    {
        if (animator != null)
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


}
