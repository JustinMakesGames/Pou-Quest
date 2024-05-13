using System;
using System.Collections;
using System.Collections.Generic;
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
    private void Start()
    {
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
        }
        else
        {
            currentDungeon--;
            currentDungeonPos = pos;
        }
    }
}
