using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell1 : MonoBehaviour
{
    public bool collapsed;
    public List<Tile1> tiles;

    public void CreateCell(bool state, List<Tile1> tileOpt)
    {
        collapsed = state;
        tiles = tileOpt;
    }

    public void RecreateCell(List<Tile1> tileOpt)
    {
        tiles = tileOpt;
    }
}
