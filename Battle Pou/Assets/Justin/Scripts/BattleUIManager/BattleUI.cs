using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    private PlayerHandler playerHandler;

    private void Awake()
    {
        playerHandler = PlayerHandler.Instance;
    }
    public void Attack1()
    {
        BattleManager.instance.playerAttack = playerHandler.attacks[0];
        BattleManager.instance.HandlingStates(BattleState.AttackingTurn);
    }
    
    public void Flee()
    {
        int chanceToEscape = 1;

        if (chanceToEscape == 1)
        {
            BattleManager.instance.HandlingStates(BattleState.Flee);

        }
    }
}
