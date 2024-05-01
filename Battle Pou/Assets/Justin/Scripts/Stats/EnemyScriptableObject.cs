using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemy")]
public class EnemyStats : ScriptableObject
{
    public string enemyName;
    public int hp;
    public int maxHp;
    public int attackPower;
}
