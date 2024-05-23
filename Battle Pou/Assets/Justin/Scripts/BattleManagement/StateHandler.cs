using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public  class StateHandler : MonoBehaviour
{
    public TMP_Text battleText;
    public GameObject chooseScreen;
    public Transform playerAttack;
    public Transform enemyAttack;

    protected PlayerHandler playerHandler;
    protected EnemyHandler enemyHandler;
    protected Transform player;
    protected Transform enemy;

    protected Vector3 playerStartPosition;
    protected Vector3 enemyStartPosition;

    protected float attackingLength;

    public void Initialize(BattleManager battleManager)
    {
        battleText = battleManager.battleText;
        chooseScreen = battleManager.chooseScreen;
        playerAttack = battleManager.playerAttack;
        enemyAttack = battleManager.enemyAttack;
        playerHandler = battleManager.playerHandler;
        enemyHandler = battleManager.enemyHandler;
        player = battleManager.player;
        enemy = battleManager.enemy;

        playerStartPosition = battleManager.playerStartPosition;
        enemyStartPosition = battleManager.enemyStartPosition;
        attackingLength = battleManager.attackingLength;
    }
    public virtual void HandleState()
    {

    }

    public void FinishBattle()
    {
        StopAllCoroutines();
    }
}
