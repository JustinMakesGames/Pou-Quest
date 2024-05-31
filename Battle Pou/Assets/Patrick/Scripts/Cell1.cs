using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell1 : MonoBehaviour
{
    public bool collapsed;
    public List<Tile> tiles;

    public void CreateCell(bool state, List<Tile> tileOpt)
    {
        collapsed = state;
        tiles = tileOpt;
    }

    public void RecreateCell(List<Tile> tileOpt)
    {
        tiles = tileOpt;
    }
}
