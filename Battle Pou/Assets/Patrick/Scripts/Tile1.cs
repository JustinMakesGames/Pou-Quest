using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile1 : MonoBehaviour
{
    public Tile[] up;
    public Tile[] down;
    public Tile[] left;
    public Tile[] right;
    public int dungeonId;
    public GameObject inDoor;
    public GameObject outDoor;
    public int maxChance;
    private void Start()
    {
        StartCoroutine(GiveEnemyKey());       
    }

    private IEnumerator GiveEnemyKey()
    {
        yield return new WaitForSeconds(0.1f);
        if (GetComponent<EnemySpawner>().enemies.Count > 0)
        {
            //int chance = Random.Range(0,2);
            int chance = 1;

            if (chance == 0)
            {
                print("Nothing happens");
            }
            else
            {
                print("HOLY MOLY IT HAPPENED YESSS");
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
