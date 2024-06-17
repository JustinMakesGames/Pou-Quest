using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHandler : MonoBehaviour
{
    public int id;
    public string enemyName;
    public int hp;
    public int maxHp;
    public int attackPower;
    public int exp;
    public EnemyStats stats;
    public List<Transform> enemyAttacks;
    public NavMeshAgent agent;
    public List<AudioSource> damageAudios = new();
    public Animator animator;

    private Color color;
    private void Awake()
    {
        for (int i = 1; i < 4; i++)
        {
            damageAudios.Add(GameObject.FindGameObjectWithTag("Audio").GetComponentAtIndex<AudioSource>(i));
        }
        enemyName = stats.enemyName;
        hp = stats.hp;
        maxHp = stats.maxHp;
        attackPower = stats.attackPower;
        exp = stats.exp;

        Attacking[] attacks = transform.GetComponentsInChildren<Attacking>();
        for (int i = 0; i < attacks.Length; i++)
        {
            enemyAttacks.Add(attacks[i].transform);
        }
        color = GetComponentInChildren<Renderer>().material.color;
    }

    private void Update()
    {
        if (animator != null)
        {
            animator.SetFloat("Walking", agent.velocity.magnitude);
        }
        
    }
    public void ChoosingAttack()
    {
        int randomAttack = Random.Range(0, enemyAttacks.Count);
        Transform attack = enemyAttacks[randomAttack];
        BattleManager.instance.enemyAttack = attack;
        BattleManager.instance.InitializeHandlers();
    }

    public void TakeDamage(int damage)
    {
        print("YOUCH");
        hp -= damage;
        damageAudios[Random.Range(0, damageAudios.Count)].Play();
        BattleUI.instance.StatsChange();
        StartCoroutine(ShowDamage());
       
        if (hp <= 0)
        {
            hp = 0;
            if (QuestManager.instance != null)
            {
                QuestManager.instance.UpdateQuest(id);
            }

            BattleManager.instance.HandlingStates(BattleState.Win);
            StartCoroutine(EnemyDeathAnimation());
        }
    }

    private IEnumerator EnemyDeathAnimation()
    {
        if (animator != null)
        {
            yield return new WaitForSeconds(1);
            animator.SetTrigger("Death");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        }      
        Destroy(gameObject);
        
    }

    private IEnumerator ShowDamage()
    {
        GetComponentInChildren<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        GetComponentInChildren<Renderer>().material.color = color;
    }




}
