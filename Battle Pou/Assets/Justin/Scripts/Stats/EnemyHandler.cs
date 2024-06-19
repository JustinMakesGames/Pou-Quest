using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Material redMaterial;
    private Material originalMaterial;
    private void Awake()
    {
        damageAudios = FindAnyObjectByType<AudioRef>().damageAudios.ToList();
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
        originalMaterial = GetComponentInChildren<Renderer>().material;
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
        if (enemyName == "Plaque Doctor")
        {
            GetComponentInChildren<Renderer>().material = redMaterial;
            yield return new WaitForSeconds(0.5f);
            GetComponentInChildren<Renderer>().material = originalMaterial;
        }
        else
        {
            GetComponentInChildren<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            GetComponentInChildren<Renderer>().material.color = originalMaterial.color;
        }
        
    }




}
