using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    private PlayerHandler playerHandler;

    private void Awake()
    {
        playerHandler = FindObjectOfType<PlayerHandler>();
    }
    public void Attack1()
    {
        BattleManager.instance.playerAttack = playerHandler.attacks[0];
        BattleManager.instance.HandlingStates(BattleState.AttackingTurn);
    }
}
