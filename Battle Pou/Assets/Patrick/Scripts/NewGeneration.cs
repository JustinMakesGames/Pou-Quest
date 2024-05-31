using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;


public class NewGeneration : MonoBehaviour
{
    public int maxXZ;
    public float offsetX;
    public float offsetZ;
    public Tile[] tileObjects;
    public List<Cell> gridList = new();
    public Cell cell;
    public Tile blankTile;
    private int iterations = 0;
    public static NewGeneration instance;
    public List<GameObject> dungeons = new();
    public List<Transform> dungeonPositions = new();

    private void Start()
    {
        instance = this;
        InitializeGrid();
    }
    void InitializeGrid()
    {
        for (int x = 0; x < maxXZ; x++)
        {
            for (int z = 0; z < maxXZ; z++)
            {
                Cell newCell = Instantiate(cell, new Vector3(x * offsetX, 0, z * offsetZ), Quaternion.identity);
                newCell.CreateCell(false, tileObjects);
                gridList.Add(newCell);
            }
        }
        StartCoroutine(CheckEntropy());
    }
    IEnumerator CheckEntropy()
    {
        List<Cell> tempGrid = new(gridList);
        tempGrid.RemoveAll(c => c.collapsed);
        tempGrid.Sort((a, b) => { return a.tiles.Length - b.tiles.Length; });
        int arrLength = tempGrid[0].tiles.Length;
        int stopIndex = default;
        for (int i = 1; i < tempGrid.Count; i++)
        {
            stopIndex = i;
            break;
        }
        if (stopIndex > 0)
        {
            tempGrid.RemoveRange(stopIndex, tempGrid.Count - stopIndex);
        }
        yield return new WaitForSeconds(0.01f);
        CollapseCell(tempGrid);
    }
    void CollapseCell(List<Cell> tempGrid)
    {
        int randIndex = UnityEngine.Random.Range(0, tempGrid.Count);
        Cell colCell = tempGrid[randIndex];
        colCell.collapsed = true;
        Tile sTile = blankTile;
        try
        {
            sTile = colCell.tiles[UnityEngine.Random.Range(0, colCell.tiles.Length)];
        }
        catch
        {
            Debug.Log("array error");
        }
        colCell.tiles = new Tile[] { sTile };
        Tile fTile = colCell.tiles[0];
        Tile newTile = Instantiate(fTile, colCell.transform.position, Quaternion.Euler(Vector3.zero));
        dungeons.Add(newTile.gameObject);
        dungeonPositions.Add(newTile.transform);
        newTile.transform.parent = transform;
        newTile.gameObject.isStatic = true;
        newTile.dungeonId = fTile.dungeonId;
        UpdateGeneration();
    }
    void UpdateGeneration()
    {
        List<Cell> newCells = new(gridList);
        for (int x = 0; x < maxXZ; x++)
        {
            for (int y = 0; y < maxXZ; y++)
            {
                var index = x + y * maxXZ;
                if (gridList[index].collapsed)
                {
                    newCells[index] = gridList[index];
                }
                else
                {
                    List<Tile> options = new();
                    foreach (var v in tileObjects) 
                    {
                        options.Add(v);
                    }
                    if (y > 0)
                    {
                        Cell up = gridList[x + (y - 1) * maxXZ];
                        List<Tile> validOptions = new();
                        foreach (var possible in up.tiles)
                        {
                            var valOp = Array.FindIndex(tileObjects, obj => obj == possible);
                            var valid = tileObjects[valOp].up;
                            validOptions = validOptions.Concat(valid).ToList();
                        }
                        CheckValidity(options, validOptions);
                    }
                    if (x < maxXZ - 1)
                    {
                        Cell right = gridList[x + 1 + y * maxXZ];
                        List<Tile> validOptions = new();
                        foreach (Tile possible in right.tiles)
                        {
                            var valOp = Array.FindIndex(tileObjects, obj => obj == possible);
                            var valid = tileObjects[valOp].right;
                            validOptions = validOptions.Concat(valid).ToList();
                        }
                        CheckValidity(options, validOptions);
                    }
                    if (y < maxXZ - 1)
                    {
                        Cell down = gridList[x + (y + 1) * maxXZ];
                        List<Tile> validOptions = new();
                        foreach (Tile possible in down.tiles)
                        {
                            var valOp = Array.FindIndex(tileObjects, obj => obj == possible);
                            var valid = tileObjects[valOp].down;
                            validOptions = validOptions.Concat(valid).ToList();
                        }
                        CheckValidity(options, validOptions);
                    }
                    if (x > 0)
                    {
                        Cell left = gridList[x - 1 + y * maxXZ];
                        List<Tile> validOptions = new();
                        foreach (Tile possible in left.tiles)
                        {
                            var valOp = Array.FindIndex(tileObjects, obj => obj == possible);
                            var valid = tileObjects[valOp].left;
                            validOptions = validOptions.Concat(valid).ToList();
                        }
                        CheckValidity(options, validOptions);
                    }
                    Tile[] newTiles = new Tile[options.Count];
                    for (int i = 0; i < options.Count; i++)
                    {
                        newTiles[i] = options[i];
                    }

                    newCells[index].RecreateCell(newTiles);
                }
            }
        }
        gridList = newCells;
        iterations++;
        if (iterations < maxXZ * maxXZ)
        {
            StartCoroutine(CheckEntropy());
        }
    }
    void CheckValidity(List<Tile> optionList, List<Tile> validOption)
    {
        for (int x = optionList.Count - 1; x >= 0; x--)
        {
            var element = optionList[x];
            if (!validOption.Contains(element))
            {
                optionList.RemoveAt(x);
            }
        }
    }
}
