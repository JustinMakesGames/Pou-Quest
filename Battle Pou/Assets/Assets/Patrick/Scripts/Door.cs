using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Door : MonoBehaviour
{
    public enum Direction { Left, Right, Top, Bottom }

    public Direction dir;
    private Generation generation;
    private void Start()
    {
        generation = FindAnyObjectByType<Generation>();
    }
    public void OnInteract()
    {
        switch (dir)
        {
            case Direction.Left: generation.GenerateDungeon(generation.currentDungeon - 1); break;
            case Direction.Right: generation.GenerateDungeon(generation.currentDungeon + 1); break;
            case Direction.Top: generation.GenerateDungeon(generation.currentDungeon + 30); break;
            case Direction.Bottom: generation.GenerateDungeon(generation.currentDungeon - 30); break;
        }
    }
}
