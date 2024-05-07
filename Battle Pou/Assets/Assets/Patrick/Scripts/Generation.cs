using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class Generation : MonoBehaviour
{
    public int maxX;
    public int maxY;
    public int gridOffset = 10;
    public GameObject cube, door, player;
    public List<GameObject> gridList = new List<GameObject>();
    public List<GameObject> children = new List<GameObject>();
    public List<GameObject> dungeons = new List<GameObject>();
    public GameObject dungeon;
    public int currentDungeon = 450;
    public Vector3[] rotations = new Vector3[4];
    public int currentGridX;
    public int currentGridY;
    public Vector3 currentDungeonPos;
    public GameObject checkForDungeon;
    public bool hasWaited;


    private void Start()
    {
        GenerateDungeon(Door.Direction.None);
        //GenerateGrid();
    }
    //public void GenerateGrid()
    //{
    //    for(int x = currentGridX; x < maxX; x++)
    //    {
    //        for(int y = currentGridY; y < maxY; y++)
    //        {
    //            Vector3 pos = new Vector3(x * gridOffset, 0, y * gridOffset);
    //            GameObject block = Instantiate(cube, pos, Quaternion.Euler(rotations[Random.Range(0, 4)]));
    //            block.transform.SetParent(transform);
    //            gridList.Add(block);
    //            int num = gridList.Count - 1;
    //            block.name = num.ToString();
    //        }
    //    }
    //    GenerateDungeon(0, Door.Direction.Left);
    //}
    bool DoesDungeonExist(Vector3 pos)
    {
        GameObject go = Instantiate(checkForDungeon, pos, Quaternion.identity);
        return go.GetComponent<CheckForDungeon>().doesDungeonExist;
    }
    Vector3 Pos(Door.Direction direction)
    {
        switch (direction)
        {
            case Door.Direction.Left: return new Vector3(currentDungeonPos.x, 0, currentDungeonPos.z + 30);
            case Door.Direction.Right: return new Vector3(currentDungeonPos.x, 0, currentDungeonPos.z - 30);
            case Door.Direction.Top: return new Vector3(currentDungeonPos.x + 30, 0, currentDungeonPos.z);
            case Door.Direction.Bottom: return new Vector3(currentDungeonPos.x - 30, 0, currentDungeonPos.z);
            case Door.Direction.None: return currentDungeonPos;
            default: throw new ArgumentOutOfRangeException();
        }
    }

    public void GenerateDungeon(Door.Direction direction)
    {
        Vector3 pos = Pos(direction);
        bool doesDungeonExist = DoesDungeonExist(pos);
        if (!doesDungeonExist)
        {
            currentDungeon++;
            GameObject newDungeon = Instantiate(dungeon, pos, Quaternion.identity);
            dungeons.Add(newDungeon);
            newDungeon.name = (dungeons.Count - 1).ToString();
            currentDungeonPos = newDungeon.transform.position;
        }
        else
        {
            //put player inside dungeon
        }
    }


}
