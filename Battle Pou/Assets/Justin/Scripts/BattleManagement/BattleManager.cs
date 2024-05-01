using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public enum BattleState { Start, PlayerTurn, AttackingTurn, Win, Lose}
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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void OnEnable()
    {       
        HandlingStates(BattleState.Start);
    }
    
    public void HandlingStates(BattleState handlingState)
    {
        state = handlingState;
        switch (state)
        {
            case BattleState.Start:
                StartCoroutine(SettingUpStats());
                StartCoroutine(BattleTextAppears());
                break;
            case BattleState.PlayerTurn:
                PlayerTurn();
                break;
            case BattleState.AttackingTurn:
                StartCoroutine(Attacking());
                break;
            case BattleState.Win:
                //Win
                break;
            case BattleState.Lose:
                //Lose
                break;
        }
    }
    
    private IEnumerator SettingUpStats()
    {
        yield return new WaitForEndOfFrame();
        player = FindObjectOfType<BattlePlayerMovement>().transform;
        enemy = FindObjectOfType<EnemyHandler>().transform;
        playerHandler = PlayerHandler.Instance;
        enemyHandler = FindObjectOfType<EnemyHandler>();
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
        const float attackingLength = 99999999f;

        player.GetComponent<BattleCamera>().enabled = true;
        player.GetComponent<BattlePlayerMovement>().enabled = true;
        enemyHandler.ChoosingAttack();
        while (time < attackingLength)
        {
            time += Time.deltaTime;
            if (playerAttack != null)
            {
                playerAttack.GetComponent<IAttacking>().Attack();
            }

            if (enemy != null)
            {
                enemyAttack.GetComponent<IAttacking>().Attack();
            }
            
            yield return null;
        }

        while (player.position != playerStartPosition && enemy.position != enemyStartPosition)
        {
            PuttingObjectsAtPosition();
            yield return null;
        }
        


    }
    
    private void PuttingObjectsAtPosition()
    {

    }
}
