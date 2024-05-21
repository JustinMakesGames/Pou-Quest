using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Generation : MonoBehaviour
{
    public int offset;
    public GameObject player;
    public List<GameObject> dungeons = new List<GameObject>();
    public GameObject dungeon;
    public int currentDungeon = 0;
    public Vector3 currentDungeonPos;
    public List<Vector3> dungeonPositions = new List<Vector3>();
    public int currentChance = 400;
    public int random;
    public GameObject pou;
    public static Generation instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        random = UnityEngine.Random.Range(0, currentChance);
        GenerateDungeon(Door.Direction.None);
    }
    bool DoesDungeonExist(Vector3 pos)
    {
        return dungeonPositions.Contains(pos);
    }
    Vector3 Pos(Door.Direction direction)
    {
        switch (direction)
        {
            case Door.Direction.Left: return new Vector3(currentDungeonPos.x, 0, currentDungeonPos.z + offset);
            case Door.Direction.Right: return new Vector3(currentDungeonPos.x, 0, currentDungeonPos.z - offset);
            case Door.Direction.Top: return new Vector3(currentDungeonPos.x + offset, 0, currentDungeonPos.z);
            case Door.Direction.Bottom: return new Vector3(currentDungeonPos.x - offset, 0, currentDungeonPos.z);
            case Door.Direction.None: return currentDungeonPos;
            default: throw new ArgumentOutOfRangeException();
        }
    }
    public void GenerateDungeon(Door.Direction direction)
    {
        Vector3 pos = Pos(direction);
        bool doesDungeonExist = DoesDungeonExist(pos);
        if(!doesDungeonExist)
        {
            currentDungeon++;
            GameObject newDungeon = Instantiate(dungeon, pos, Quaternion.identity);
            dungeons.Add(newDungeon);
            newDungeon.name = (dungeons.Count - 1).ToString();
            currentDungeonPos = newDungeon.transform.position;
            dungeonPositions.Add(currentDungeonPos);
            int randomNum = UnityEngine.Random.Range(0, currentChance);
            if (randomNum == random)
            {
                Instantiate(pou, new Vector3(currentDungeonPos.x + 13, currentDungeonPos.y + 1, currentDungeonPos.z), Quaternion.Euler(0, -90, 0));
            }
        }
        else
        {
            currentDungeon--;
            currentDungeonPos = pos;
        }
    }

    public void GenerateDungeonAtPos(Vector3 pos)
    {
        currentDungeon++;
        GameObject newDungeon = Instantiate(dungeon, pos, Quaternion.identity);
        dungeons.Add(newDungeon);
        newDungeon.name = (dungeons.Count - 1).ToString();
        currentDungeonPos = newDungeon.transform.position;
        dungeonPositions.Add(currentDungeonPos);
    }
}
