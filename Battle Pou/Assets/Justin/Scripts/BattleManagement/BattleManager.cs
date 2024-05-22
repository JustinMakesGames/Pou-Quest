using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public enum BattleState { Start, PlayerTurn, AttackingTurn, Win, Lose, Flee}
public class BattleManager : MonoBehaviour
{

    public static BattleManager instance;

    public TMP_Text battleText;
    public GameObject chooseScreen;
    public Transform playerAttack;
    public Transform enemyAttack;

    private BattleState state;
    private PlayerHandler playerHandler;
    private EnemyHandler enemyHandler;
    private Transform player;
    private Transform enemy;

    private Vector3 playerStartPosition;
    private Vector3 enemyStartPosition;

    public float attackingLength;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {       
        HandlingStates(BattleState.Start);
    }
    
    public void HandlingStates(BattleState handlingState)
    {
        state = handlingState;
        switch (state)
        {
            case BattleState.Start:
                StartCoroutine(SettingUpBattle());
                StartCoroutine(BattleTextAppears());
                break;
            case BattleState.PlayerTurn:
                PlayerTurn();
                break;
            case BattleState.AttackingTurn:
                StartCoroutine(Attacking());
                break;
            case BattleState.Win:
                HandleWinning();
                break;
            case BattleState.Lose:
                StopAllCoroutines();
                
                LoseManager.instance.LoseManagement(player.gameObject);
                break;
            case BattleState.Flee:
                StopAllCoroutines();
                BattleTransition.instance.FleeingBattle();
                break;
        }
    }
    

    private IEnumerator SettingUpBattle()
    {
        yield return null;
        
        player = FindAnyObjectByType<BattlePlayerMovement>().transform;
        enemy = FindAnyObjectByType<EnemyHandler>().transform;
        playerHandler = PlayerHandler.Instance;
        enemyHandler = FindAnyObjectByType<EnemyHandler>();
        playerStartPosition = player.position;
        enemyStartPosition = enemy.position;
        playerHandler.BattlePlayerSet(player);
        SettingUpAttacks();
        yield return new WaitForSeconds(2);
        player.GetComponent<BattleCamera>().enabled = true;
    }

    private void SettingUpAttacks()
    {
        foreach (var attack in PlayerHandler.Instance.attacks)
        {
            Instantiate(attack, player);
        }
    }

    private IEnumerator BattleTextAppears()
    {
        yield return new WaitForSeconds(2);
        battleText.text = "A Wild " + enemyHandler.enemyName + " appears!";
        yield return new WaitForSeconds(2);
        HandlingStates(BattleState.PlayerTurn);

    }

    private void PlayerTurn()
    {
        battleText.text = "What will you do?";
        chooseScreen.SetActive(true);
    }

    private IEnumerator Attacking()
    {
        float time = 0f;
        player.GetComponent<BattlePlayerMovement>().enabled = true;
        enemyHandler.ChoosingAttack(); 
        
        if (playerAttack != null)
        {
            player.GetComponent<MeshRenderer>().enabled = false;
            playerAttack.GetComponent<MeshRenderer>().enabled = true;
            playerAttack.GetComponent<Attacking>().StartAttack();

            player.GetComponent<BattlePlayerMovement>().animator = playerAttack.GetComponent<Attacking>().animator;
        }      
        enemyAttack.GetComponent<Attacking>().StartAttack();
        while (time < attackingLength)
        {
            time += Time.deltaTime;
            if (playerAttack != null)
            {
                playerAttack.GetComponent<Attacking>().UpdateAttack();
            }

            if (enemy != null)
            {
                enemyAttack.GetComponent<Attacking>().UpdateAttack();
            }
            
            yield return null;
        }

        if (playerAttack != null)
        {
            playerAttack.GetComponent<Attacking>().FinishAttack();
        }
        enemyAttack.GetComponent<Attacking>().FinishAttack();
        StartCoroutine(HandlingEndOfTurn());
    }

    private IEnumerator HandlingEndOfTurn()
    {
        TurnAttackRendererOff();
        TurnPlayerMovementOff();  
        PuttingObjectsAtPosition();
        RemovingAllProjectiles();
        bool areObjectsBack = false;
        while (!areObjectsBack)
        {
            areObjectsBack = AreObjectsAtStartPosition();
            yield return null;
        }
        PuttingStartRotation();
        HandlingStates(BattleState.PlayerTurn);
    }
    
    private void TurnPlayerMovementOff()
    {
        player.GetComponent<BattlePlayerMovement>().enabled = false;
        playerAttack = null;
    }
    private void PuttingObjectsAtPosition()
    {
        player.GetComponent<NavMeshAgent>().enabled = true;
        player.GetComponent<NavMeshAgent>().SetDestination(playerStartPosition);
        enemy.GetComponent<NavMeshAgent>().SetDestination(enemyStartPosition);
    }

    private void RemovingAllProjectiles()
    {
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (GameObject projectile in projectiles)
        {
            Destroy(projectile);
        }
    }

    private void TurnAttackRendererOff()
    {
        if (playerAttack != null)
        {
            player.GetComponent<MeshRenderer>().enabled = true;
            playerAttack.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void PuttingStartRotation()
    {
        player.rotation = Quaternion.identity;
        enemy.rotation = Quaternion.Euler(0, 180, 0);
        player.GetComponent<NavMeshAgent>().enabled = false;
    }

    private bool AreObjectsAtStartPosition()
    {
        float playerDistance = CalculatePlayerDistance();
        float enemyDistance = Vector3.Distance(enemy.position, enemyStartPosition);
        if (playerDistance < 0.05f && enemyDistance < 0.05f)
        {
            return true;
        }

        return false;
    }

    private float CalculatePlayerDistance()
    {
        Vector3 playerPosition = new Vector3(player.position.x,0,player.position.z);
        Vector3 playerStartPos = new Vector3(playerStartPosition.x,0,playerStartPosition.z);
        
        return Vector3.Distance(playerPosition, playerStartPos);
    }

    private void HandleWinning()
    {
        StopAllCoroutines();
        RemovingAllProjectiles();

        if (playerAttack != null)
        {
            playerAttack.GetComponent<Attacking>().FinishAttack();
        }
        enemyAttack.GetComponent<Attacking>().FinishAttack();
        StartCoroutine(WinManagement.instance.WinScreen(playerHandler.exp, playerHandler.maxExp, enemyHandler.exp));

    }
    
    
    
}
