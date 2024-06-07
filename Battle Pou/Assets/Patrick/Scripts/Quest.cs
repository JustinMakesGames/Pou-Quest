using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Quests")]
public class Quest : ScriptableObject
{
    public int enemyId;
    public int coins;
    public int goal;
    public string enemyName;
    public string quest;
}
