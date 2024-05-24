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
    public string enemy;

    public static QuestGenerator Instance;
    public void Awake()
    {
        Instance = this;
    }

    public string GenerateQuest()
    {
        quest = questOptions[Random.Range(0, questOptions.Length)];
        int coins = Random.Range(minCoins, maxCoins);
        int goal = Random.Range(minGoal, maxGoal);
        quest = quest.Replace("[]", goal.ToString());
        quest = quest.Replace("()", enemy);
        quest = quest.Replace("{}", coins.ToString());
        return quest;
    }
}
