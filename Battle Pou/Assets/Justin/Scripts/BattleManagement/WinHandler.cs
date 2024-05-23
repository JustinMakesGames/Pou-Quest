using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class WinHandler : StateHandler
{

    public GameObject winPanel;
    public TMP_Text experiencePoints, enemyExpGainedText;
    private int expGained;

    public GameObject levelUpPanel;
    public TMP_Text maxHpText, maxSpText;
    public TMP_Text plusHpText, plusSpText;
    public override void HandleState()
    {
        HandleWinning();
        RemovingAllProjectiles();
    }

    private void HandleWinning()
    {
        if (playerAttack != null)
        {
            playerAttack.GetComponent<Attacking>().FinishAttack();
        }
        enemyAttack.GetComponent<Attacking>().FinishAttack();
        StartCoroutine(WinScreen(playerHandler.exp, playerHandler.maxExp, enemyHandler.exp));

    }

    private void RemovingAllProjectiles()
    {
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (GameObject projectile in projectiles)
        {
            Destroy(projectile);
        }
    }

    public IEnumerator WinScreen(int playerExp, int playerMaxExp, int enemyExp)
    {
        yield return new WaitForSeconds(3);
        expGained = enemyExp;
        winPanel.SetActive(true);
        experiencePoints.text = playerExp.ToString() + "/" + playerMaxExp.ToString();
        enemyExpGainedText.text = "+" + enemyExp.ToString();
        StartCoroutine(ExperienceChanging(playerExp, playerMaxExp));
    }

    private IEnumerator ExperienceChanging(int playerExp, int playerMaxExp)
    {
        bool hasLevelUp = false;
        PlayerHandler.Instance.exp += expGained;
        playerExp += expGained;
        yield return new WaitForSeconds(2);
        
        if (playerExp >= playerMaxExp)
        {
            hasLevelUp = true;
            playerHandler.exp -= playerMaxExp;
            playerHandler.maxExp += 10;
        }
        experiencePoints.text = playerExp.ToString() + "/" + playerMaxExp.ToString();
        enemyExpGainedText.text = "";
        yield return new WaitForSeconds(2);

        if (hasLevelUp)
        {
            LevelUp();
        }
        else
        {
            BattleTransition.instance.EndingBattle();
        }

    }

    private void LevelUp()
    {
        int hpUp = 2;
        int spUp = 2;

        StartCoroutine(ShowLevelUp(hpUp, spUp));
        
    }

    private IEnumerator ShowLevelUp(int hp, int sp)
    {
        winPanel.SetActive(false);
        levelUpPanel.SetActive(true);

        maxHpText.text = playerHandler.maxHp.ToString();
        maxSpText.text = playerHandler.maxSp.ToString();

        plusHpText.text = "+" + hp.ToString();
        plusSpText.text = "+" + sp.ToString();

        yield return new WaitForSeconds(2);

        ChangeStats(hp, sp);
        maxHpText.text = playerHandler.maxHp.ToString();
        maxSpText.text = playerHandler.maxSp.ToString();

        plusHpText.text = "";
        plusSpText.text = "";

        yield return new WaitForSeconds(2);

        BattleTransition.instance.EndingBattle();


    }

    private void ChangeStats(int hp, int sp)
    {
        playerHandler.maxHp += hp;
        playerHandler.maxSp += sp;
        playerHandler.hp = playerHandler.maxHp;
        playerHandler.sp = playerHandler.maxSp;
    }
}
