using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SaveData
{
    [Header("Player")]
    public int[] dungeonType;
    public float[] dungeonX;
    public float[] dungeonZ;
    public int health = 20;
    public int maxHp = 20;
    public int sp = 20;
    public int maxSp = 20;
    public int attackPower = 1;
    public int exp;
    public int maxExp = 100;
    public int level = 1;
    public int coins;
    public List<int> inventoryIds = new();
    public List<int> inventoryCount = new();
    public List<int> attacks = new();
    [Header("Settings")]
    public int fpsLimit = 0;
    public bool fullScreen = true;
    public int resolution = 0;
    public float volume = 1;
    public float textSpeed = 1;
}
