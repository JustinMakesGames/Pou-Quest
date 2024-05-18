using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleUI : MonoBehaviour
{
    private PlayerHandler playerHandler;
    public List<TMP_Text> attackTexts = new List<TMP_Text>(3);

    private void Awake()
    {
        playerHandler = PlayerHandler.Instance;
    }

    private void Start()
    {
        for (int i = 0; i < attackTexts.Count; i++)
        {
            attackTexts[i].text = PlayerHandler.Instance.attacks[i].name;
        }  
    }
    public void Attack1()
    {
        BattleManager.instance.playerAttack = playerHandler.attacks[0];
        BattleManager.instance.HandlingStates(BattleState.AttackingTurn);
    }

    public void Attack2()
    {
        BattleManager.instance.playerAttack = playerHandler.attacks[1];
        BattleManager.instance.HandlingStates(BattleState.AttackingTurn);
    }

    public void Attack3()
    {
        BattleManager.instance.playerAttack = playerHandler.attacks[2];
        BattleManager.instance.HandlingStates(BattleState.AttackingTurn);
    }
    
    public void Flee()
    {
        int chanceToEscape = Random.Range(0,3);

        if (chanceToEscape == 0)
        {
            BattleManager.instance.HandlingStates(BattleState.Flee);

        }
        else
        {
            BattleManager.instance.HandlingStates(BattleState.AttackingTurn);
        }
    }
}
