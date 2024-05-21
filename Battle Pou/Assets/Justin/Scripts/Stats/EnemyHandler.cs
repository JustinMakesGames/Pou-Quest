using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public string enemyName;
    public int hp;
    public int maxHp;
    public int attackPower;
    public int exp;
    public EnemyStats stats;
    public List<Transform> enemyAttacks;

    private void Awake()
    {
        enemyName = stats.enemyName;
        hp = stats.hp;
        maxHp = stats.maxHp;
        attackPower = stats.attackPower;
        exp = stats.exp;

        for (int i = 0; i < transform.childCount; i++)
        {
            enemyAttacks.Add(transform.GetChild(i));
        }
    }
    public void ChoosingAttack()
    {
        int randomAttack = Random.Range(0, transform.childCount);
        Transform attack = enemyAttacks[randomAttack];
        BattleManager.instance.enemyAttack = attack;
    }

    public void TakeDamage(int damage)
    {
        print("YOUCH");
        hp -= damage;

       
        if (hp <= 0)
        {
            BattleManager.instance.HandlingStates(BattleState.Win);
        }
    }




}
