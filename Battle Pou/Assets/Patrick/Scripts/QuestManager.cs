using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public Quest currentQuest;
    public string quest;
    public TMP_Text questText;
    public int needed;
    public int newNeeded;
    public int coins;
    public int enemyID;
    public int[] enemies;
    private void Start()
    {
        instance = this;
    }
    public void UpdateQuest(int id)
    {
        if (id == enemyID)
        {
            newNeeded = needed - 1;
            quest = quest.Replace(needed.ToString(), newNeeded.ToString());
            questText.text = quest;
            if (needed <= 0)
            {
                Debug.Log("completed quest");
                PlayerHandler.Instance.coins += coins;
                PlayerHandler.Instance.exp += (10 * PlayerHandler.Instance.level) / 2;
                quest = null;
                currentQuest = null;
                questText.text = null;
            }
        }
    }

    public void AddQuest()
    {
        if (currentQuest == null)
        {
            currentQuest = QuestGenerator.instance.GenerateQuest();
            coins = currentQuest.coins;
            needed = currentQuest.goal;
            quest = currentQuest.quest;
            questText.text = quest;
        }
        else
        {
            Debug.Log("You already have a quest");
        }
    }
}
