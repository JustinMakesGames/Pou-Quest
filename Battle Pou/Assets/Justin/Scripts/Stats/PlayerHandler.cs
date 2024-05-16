using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    //Stats
    public static PlayerHandler Instance;
    public int hp;
    public int maxHp;
    public int sp;
    public int maxSp;
    public int attackPower;
    public int exp;
    public int maxExp;

    //Battle Management
    public Transform battlePlayer;
    public List<Transform> attacks;
    public bool immunityFrames;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
    }

    public void BattlePlayerSet(Transform player)
    {
        battlePlayer = player;

        for (int i = 0; i < battlePlayer.childCount; i++)
        {
            attacks.Add(battlePlayer.GetChild(i));
        }
    }
    public void TakeDamage(int damage)
    {
        if (!immunityFrames)
        {
            print("Took " + damage + " damage");
            hp -= damage;

            if (hp <= 0)
            {
                BattleManager.instance.HandlingStates(BattleState.Lose);
            }
            else
            {
                immunityFrames = true;
                StartCoroutine(InvincibleFrames());
            }
            
        }
        
        
    }

    private IEnumerator InvincibleFrames()
    {
        int invincibleFrameCount = 10;

        for (int i = 0; i < invincibleFrameCount; i++)
        {
            battlePlayer.GetComponent<MeshRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            battlePlayer.GetComponent <MeshRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        immunityFrames = false;
    }
}
