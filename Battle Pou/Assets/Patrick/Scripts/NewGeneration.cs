using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewGeneration : MonoBehaviour
{
    public enum Direction { Left, Right, Up, Down }
    public int maxX;
    public int maxY;
    public int gridOffset;
    public Transform grid;
    public GameObject dungeon;
    public int maxDungeons;
    public List<Transform> gridList = new List<Transform>();
    public List<GameObject> dungeonList = new List<GameObject>();
    private void Start()
    {
        GenerateGrid();
    }
    void GenerateGrid()
    {
        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++) 
            {
                Vector3 pos = new Vector3(x * gridOffset, 0, y * gridOffset);
                Transform gridObj = Instantiate(grid, pos, Quaternion.identity);
                gridObj.SetParent(transform);
                gridList.Add(gridObj);
            }
        }
        GenerateDungeons();
    }

    void GenerateDungeons()
    {
        for (int i = 0; i < maxDungeons; i++)
        {
            if (i == 0)
            {
                GameObject newDun = Instantiate(dungeon, gridList[0].transform);
                dungeonList.Add(newDun);
            }
            else
            {
                for (int d = 0; d < dungeonList[i].GetComponent<PossibleDungeon>().doors.Length; d++)
                {
                    GameObject[] dungeons = dungeonList[i].GetComponent<PossibleDungeon>().possibleDungeons;
                    GameObject dungeonToGenerate = dungeons[Random.Range(0, dungeons.Length)];
                    if (dungeonToGenerate != null)
                    {
                        GameObject newDun = Instantiate(dungeonToGenerate, dungeonList[dungeonList.Count].transform.GetChild
                            (dungeonList[dungeonList.Count].transform.childCount).GetChild(0).position, Quaternion.identity);
                        dungeonList.Add(newDun);
                    }
                }
                //foreach (GameObject d in dungeonList[dungeonList.Count].GetComponent<PossibleDungeon>().doors)
                //{
                //    GameObject dungeonToGenerate = dungeonList[dungeonList.Count].GetComponent<PossibleDungeon>().possibleDungeons[
                //        Random.Range(0, dungeonList[dungeonList.Count].GetComponent<PossibleDungeon>().possibleDungeons.Length)];
                //    if (dungeonToGenerate != null)
                //    {
                //        GameObject newDun = Instantiate(dungeonToGenerate, dungeonList[dungeonList.Count].transform.GetChild(dungeonList[dungeonList.Count].transform.childCount - 1).GetChild(0).position, Quaternion.identity);
                //        dungeonList.Add(newDun);
                //    }
                //}
            }
        }
    }
    private void OnDrawGizmos()
    {
        for(int i = 0; i < gridList.Count; i++)
        {
            Gizmos.DrawCube(gridList[i].transform.position, new Vector3(10, 10, 10));
        }
    }
}
