using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoorEntrances : MonoBehaviour
{
    public GameObject[] possibleDungeons;
    public enum Direction { Left, Right, Top, Bottom, None }
    public Direction dir;

    //public Direction dir;
    //private Generation generation;
    //private void Start()
    //{
    //    generation = FindAnyObjectByType<Generation>();
    //}
    //public void OnInteract()
    //{
    //    switch (dir)
    //    {
    //        case Direction.Left: generation.GenerateDungeon(Direction.Left); break;
    //        case Direction.Right: generation.GenerateDungeon(Direction.Right); break;
    //        case Direction.Top: generation.GenerateDungeon(Direction.Top); break;
    //        case Direction.Bottom: generation.GenerateDungeon(Direction.Bottom); break;
    //    }
    //}
}
