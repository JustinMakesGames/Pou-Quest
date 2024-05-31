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
    private void Start()
    {
        
        for (int i = 0; i < enemySpawns.Count; i++)
        {
            enemyToSpawn = Random.Range(0, enemiesToSpawn.Length);
            Instantiate(enemiesToSpawn[enemyToSpawn], enemySpawns[i].position, Quaternion.identity, transform);
        }

        for (int i = 0; i < itemSpawns.Count; i++)
        {
            itemToSpawn = Random.Range(0, itemsToSpawn.Length);
            Instantiate(itemsToSpawn[itemToSpawn], itemSpawns[i].position, Quaternion.identity, transform);
        }
    }
}
