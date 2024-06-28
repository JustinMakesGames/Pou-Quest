using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;
    public GameObject[] itemsToSpawn;
    public int enemyToSpawn;
    public int itemToSpawn;

    public List<Transform> enemySpawns;
    public List<Transform> itemSpawns;

    public List<GameObject> enemies;
    public List<GameObject> items;
    public GameObject market;
    private void Start()
    {
        /*if (enemySpawns != null && enemySpawns.Count != 0)
        {
            if (Random.Range(0, 20) == 4)
            {
                int i = Random.Range(0, enemySpawns.Count);
                Instantiate(market, enemySpawns[i].position, enemySpawns[i].rotation);
                enemySpawns.RemoveAt(i);
            }
        }*/
        for (int i = 0; i < enemySpawns.Count; i++)
        {
            enemyToSpawn = Random.Range(0, enemiesToSpawn.Length);
            GameObject enemy = Instantiate(enemiesToSpawn[enemyToSpawn], enemySpawns[i].position, Quaternion.identity, transform);
            enemies.Add(enemy);
        }

        for (int i = 0; i < itemSpawns.Count; i++)
        {
           itemToSpawn = Random.Range(0, itemsToSpawn.Length);
           GameObject item = Instantiate(itemsToSpawn[itemToSpawn], itemSpawns[i].position, Quaternion.identity, transform);
           items.Add(item);
        }
    }
}
