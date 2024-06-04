using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile1 : MonoBehaviour
{
    public Tile[] up;
    public Tile[] down;
    public Tile[] left;
    public Tile[] right;

    public GameObject inDoor;
    public GameObject outDoor;
    public int maxChance;
    private void Start()
    {
        StartCoroutine(GiveEnemyKey());       
    }

    private IEnumerator GiveEnemyKey()
    {
        while (inDoor == null)
        {
            yield return null;
        }
        if (GetComponent<EnemySpawner>().enemies.Count > 0)
        {
            int chance = 1;

            if (chance == 0)
            {
                print("Nothing happens");
            }
            else
            {
                EnemySpawner enemySpawner = GetComponent<EnemySpawner>();

                int randomEnemy = Random.Range(0, enemySpawner.enemies.Count);

                enemySpawner.enemies[randomEnemy].GetComponent<EnemyOverworld>().hasKey = true;

                LockDoor();              
            }
        }
    }

    private void LockDoor()
    {
        inDoor.GetComponent<Collider>().isTrigger = false;
        inDoor.transform.GetChild(2).GetComponent<Renderer>().enabled = true;
    }
}