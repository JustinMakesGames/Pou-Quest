using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public string currentQuest;
    public TMP_Text questText;
    public int needed;
    public int coins;
    public int enemyID;
    public int[] enemies;
    private void Start()
    {
        instance = this;
    }
    public void UpdateQuest(int id)
    {
        currentQuest = currentQuest.Replace(needed.ToString(), needed--.ToString());
        questText.text = currentQuest;
        if (needed <= 0)
        {
            Debug.Log("completed quest");
            PlayerHandler.Instance.coins += coins;
            currentQuest = null;
        }
        //if (id == enemyID)
        //{

        //}
    }

    public void AddQuest()
    {
        if (currentQuest != null)
        {
            currentQuest = QuestGenerator.instance.GenerateQuest();
            needed = QuestGenerator.instance.goal;
            coins = QuestGenerator.instance.coins;
            enemyID = QuestGenerator.instance.enemyID;
            questText.text = currentQuest;
        }
        else
        {
            Debug.Log("You already have a quest");
        }
    }
}
