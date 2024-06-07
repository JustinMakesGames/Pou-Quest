using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGenerator : MonoBehaviour
{
    public Quest quest;
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

    public Quest GenerateQuest()
    {
        if (QuestManager.instance.currentQuest == null)
        {
            quest.coins = Random.Range(minCoins, maxCoins);
            quest.goal = Random.Range(maxCoins, maxGoal);
            quest.enemyId = QuestManager.instance.enemies[Random.Range(0, enemy.Length)];
            quest.enemyName = enemy[quest.enemyId];
            enemyID = quest.enemyId;
            return quest;
        }
        else return QuestManager.instance.currentQuest;
    }
}
