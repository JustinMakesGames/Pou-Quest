using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
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
    public int currentDungeon;
    public Vector3[] rotations = new Vector3[4];

    public bool DungeonExists(int curDungeon)
    {
        if (gridList[curDungeon].transform.childCount == 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Start()
    {
        GenerateGrid();
    }
    public void GenerateGrid()
    {
        for(int x = 0; x < maxX; x++)
        {
            for(int y = 0; y < maxY; y++)
            {
                Vector3 pos = new Vector3(x * gridOffset, 0, y * gridOffset);
                GameObject block = Instantiate(cube, pos, Quaternion.Euler(rotations[Random.Range(0, 4)]));
                block.transform.SetParent(transform);
                gridList.Add(block);
                int num = gridList.Count - 1;
                block.name = num.ToString();
            }
        }
        GenerateDungeon(0);
    }

    public void GenerateDungeon(int curDungeon)
    {
        bool doesNextDungeonExist = DungeonExists(curDungeon);
        if (!doesNextDungeonExist)
        {
            currentDungeon = curDungeon;
            GameObject newDungeon = Instantiate(dungeon, gridList[curDungeon].transform.position, Quaternion.identity);
            dungeons.Add(newDungeon);
            newDungeon.transform.SetParent(gridList[dungeons.Count - 1].transform);
            newDungeon.name = (dungeons.Count - 1).ToString();
        }
        else
        {
            //TODO: put player inside dungeon instead of making new one
        }
    }
}
