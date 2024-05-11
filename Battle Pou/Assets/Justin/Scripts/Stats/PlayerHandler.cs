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
    public List<Transform> attacks = new List<Transform>(3);
    public bool invincible;

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
    }
    public void TakeDamage(int damage)
    {
        if (!invincible)
        {
            print("Took " + damage + " damage");
            hp -= damage;
            invincible = true;
            StartCoroutine(InvincibleFrames());
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
        invincible = false;
    }
}
