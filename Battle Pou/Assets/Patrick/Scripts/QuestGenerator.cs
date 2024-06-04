using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGenerator : MonoBehaviour
{
    public string quest;
    public string[] questOptions;
    public int maxCoins;
    public int minCoins;
    public int maxGoal;
    public int minGoal;
    public string[] enemy;
    public int enemyID;
    public int goal;
    public int coins;

    public static QuestGenerator instance;
    public void Awake()
    {
        instance = this;
    }

    public string GenerateQuest()
    {
        if (QuestManager.instance.currentQuest != null)
        {
            quest = questOptions[Random.Range(0, questOptions.Length)];
            int coins = Random.Range(minCoins, maxCoins);
            goal = Random.Range(minGoal, maxGoal);
            enemyID = QuestManager.instance.enemies[Random.Range(0, QuestManager.instance.enemies.Length)];
            quest = quest.Replace("[]", goal.ToString());
            quest = quest.Replace("()", enemy[enemyID]);
            quest = quest.Replace("{}", coins.ToString());
            return quest;
        }
        else return QuestManager.instance.currentQuest;
    }
}
