using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class WinManagement : MonoBehaviour
{
    public static WinManagement instance;
    public GameObject winPanel;
    public TMP_Text experiencePoints, enemyExpGainedText;

    private int expGained;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
        PlayerHandler.Instance.exp += expGained;
        playerExp += expGained;
        yield return new WaitForSeconds(2);
        experiencePoints.text = playerExp.ToString() + "/" + playerMaxExp.ToString();
        enemyExpGainedText.text = "";
        yield return new WaitForSeconds(2);
        
        if (playerExp >= playerMaxExp)
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
        
    }
}
