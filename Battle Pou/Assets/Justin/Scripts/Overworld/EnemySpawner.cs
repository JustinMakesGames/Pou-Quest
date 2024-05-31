using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;
    public int enemyToSpawn;

    public List<Transform> enemySpawns;
    private void Start()
    {
        
        for (int i = 0; i < enemySpawns.Count; i++)
        {
            enemyToSpawn = Random.Range(0, enemiesToSpawn.Length);

            Instantiate(enemiesToSpawn[enemyToSpawn], enemySpawns[i].position, Quaternion.identity, transform);


        }
    }
}
