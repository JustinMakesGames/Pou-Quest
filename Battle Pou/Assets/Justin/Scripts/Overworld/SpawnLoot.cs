using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnLoot : MonoBehaviour
{
    public GameObject coin;
    public List<GameObject> items;

    public int chanceOfItem;
    public int randomAmountOfCoins;
    public int maxAmountOfCoins;

    public float forwardSpeed;
    public float upSpeed;
    public List<GameObject> lootList;

    public float gravity;

    public GameObject key;
    public void SpawningLoot(Vector3 position, bool hasKey)
    {
        randomAmountOfCoins = Random.Range(0, maxAmountOfCoins);
        
        for (int i = 0; i < randomAmountOfCoins; i++)
        {
            lootList.Add(coin);
        }
        int itemDrop = Random.Range(0, chanceOfItem);

        if (itemDrop == 0)
        {
            int chosenItem = Random.Range(0, items.Count);

            lootList.Add(items[chosenItem]);
        }

        if (hasKey)
        {
            lootList.Add(key);
        }

        for (int i = 0; i < lootList.Count; i++)
        {
            GameObject clone = Instantiate(lootList[i], position, Quaternion.Euler(0, Random.Range(0,360),0));

            if (clone.GetComponent<Rigidbody>() != null)
            {
                Rigidbody rb = clone.GetComponent<Rigidbody>();
                rb.AddForce(clone.transform.forward * forwardSpeed + clone.transform.up * upSpeed, ForceMode.VelocityChange);
                rb.velocity = new Vector3(0, -gravity, 0);
            }
            
        }


        lootList.Clear();
    }
}
