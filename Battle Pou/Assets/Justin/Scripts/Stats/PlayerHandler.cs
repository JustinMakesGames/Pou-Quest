using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
    public int level = 1;
    public int coins;
    //Battle Management
    public Transform battlePlayer;
    public List<Transform> attacks;
    public bool immunityFrames;

    public TMP_Text playerHPText, playerSPText, coinsText;

    
    public Slider playerHPSlider, playerSPSlider;

    public List<int> allAttacks;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
    }

    private void Start()
    {
        //foreach (Transform attack in transform)
        //{
        //    if (attack.GetComponent<Attacking>().isBought)
        //    {
        //        allAttacks.Add(attack.GetComponent<Attacking>().attackStats.id);
        //    }
        //}
    }

    public void BattlePlayerSet(Transform player)
    {
        battlePlayer = player;
    }
    public void TakeDamage(int damage)
    {
        if (!immunityFrames)
        {
            print("Took " + damage + " damage");
            hp -= damage;

            BattleUI.instance.StatsChange();
            if (hp <= 0)
            {
                hp = 0;
                BattleManager.instance.HandlingStates(BattleState.Lose);
            }
            else
            {
                immunityFrames = true;

                if (BattleManager.instance.playerAttack != null)
                {
                    StartCoroutine(InvincibleFrames(BattleManager.instance.playerAttack));
                }
                else
                {
                    StartCoroutine(InvincibleFrames(battlePlayer));
                }
            }
            
        }
        
        
    }

    private IEnumerator InvincibleFrames(Transform player)
    {
        int invincibleFrameCount = 5;

        for (int i = 0; i < invincibleFrameCount; i++)
        {
            player.GetComponentInChildren<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            player.GetComponentInChildren<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        immunityFrames = false;
    }

    public void StopImmunityFrames()
    {
        StopAllCoroutines();
        immunityFrames = false;
    }

    public void StatsOverworldChange()
    {
        playerHPText.text = hp.ToString() + "/" + maxHp.ToString();
        playerSPText.text = sp.ToString() + "/" + maxSp.ToString();

        playerHPSlider.maxValue = maxHp;
        playerHPSlider.value = hp;
        playerSPSlider.maxValue = maxSp;
        playerSPSlider.value = sp;
    }
}
