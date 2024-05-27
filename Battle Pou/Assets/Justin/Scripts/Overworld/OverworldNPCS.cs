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
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        player = FindObjectOfType<PlayerOverworld>().transform;

        for (int i = 0; i < positionsStored.childCount; i++)
        {
            positions.Add(positionsStored.GetChild(i));
        }
    }

    private void Update()
    {
        PlayAnimations();

        if (!isTalking)
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

        else
        {
            TalkingRotation();
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

    private void TalkingRotation()
    {
        float rotationSpeed = 20f;
        Vector3 lookRotation = player.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(lookRotation);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    public void IsTalkingToNPC()
    {
        if (!isTalking)
        {
            isTalking = true;
            GetComponent<DialogManager>().StartDialog();
            agent.SetDestination(transform.position);
        }
        
    }

    public void FinishDialog()
    {
        agent.SetDestination(positions[index].position);
        isTalking = false;

        player.GetComponent<PlayerOverworld>().enabled = true;
    }


}
