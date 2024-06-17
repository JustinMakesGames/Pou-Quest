using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public enum BattleState { Start, PlayerTurn, AttackingTurn, Win, Lose, Flee}
public class BattleManager : MonoBehaviour
{
    public PlayerturnHandler playerTurnHandler;
    public AttackingHandler attackHandler;
    public WinHandler winHandler;
    public LoseHandler loseHandler;
    public static BattleManager instance;

    public TMP_Text battleText;
    public GameObject chooseScreen;
    public Transform playerAttack;
    public Transform enemyAttack;

    private BattleState state;
    public PlayerHandler playerHandler;
    public EnemyHandler enemyHandler;
    public Transform player;
    public Transform enemy;

    public Vector3 playerStartPosition;
    public Vector3 enemyStartPosition;

    public float attackingLength;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this) 
        {
            Destroy(this);
        }

        playerTurnHandler = GetComponent<PlayerturnHandler>();
        attackHandler = GetComponent<AttackingHandler>();
        winHandler = GetComponent<WinHandler>();
        loseHandler = GetComponent<LoseHandler>();
       
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
                InitializeHandlers();
                playerTurnHandler.HandleState();
                break;
            case BattleState.AttackingTurn:
                InitializeHandlers();
                attackHandler.HandleState();
                break;
            case BattleState.Win:
                InitializeHandlers();
                FinishCoroutines();
                winHandler.HandleState();
                break;
            case BattleState.Lose:
                InitializeHandlers();
                FinishCoroutines();
                loseHandler.HandleState();
                break;
            case BattleState.Flee:
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

        InitializeHandlers();
    }

    public void InitializeHandlers()
    {
        playerTurnHandler.Initialize(this);
        attackHandler.Initialize(this);
        winHandler.Initialize(this);
        loseHandler.Initialize(this);

    }

    public void FinishCoroutines()
    {
        playerTurnHandler.FinishBattle();
        attackHandler.FinishBattle();
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
    
    
    
    
}
