using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SaveData
{
    public int[] dungeonType;
    public float[] dungeonX;
    public float[] dungeonZ;
    public int health;
    public int maxHp;
    public int sp;
    public int maxSp;
    public int attackPower;
    public int exp;
    public int maxExp;
    public int level;
    public int coins;
    public List<int> inventoryIds = new List<int>();
}
