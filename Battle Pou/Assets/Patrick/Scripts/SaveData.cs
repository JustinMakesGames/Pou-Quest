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
    public int sp;
    public int attackPower;
    public int exp;
    public int level;
    public List<int> inventoryIds = new List<int>();
}
