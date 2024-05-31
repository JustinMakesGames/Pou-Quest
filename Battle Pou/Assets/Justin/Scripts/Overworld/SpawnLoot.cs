using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        StartCoroutine(SpawnEveryTime());
    }

    private IEnumerator SpawnEveryTime()
    {
        SpawningLoot(new Vector3(0.4f, -1.3f, -5.02f));
        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnEveryTime());
    }
    public void SpawningLoot(Vector3 position)
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

        for (int i = 0; i < lootList.Count; i++)
        {
            GameObject clone = Instantiate(lootList[i], position, Random.rotationUniform);
            clone.transform.rotation = Quaternion.Euler(0, clone.transform.rotation.y, 0);
            Rigidbody rb = clone.GetComponent<Rigidbody>();
            rb.AddForce(clone.transform.forward * forwardSpeed + clone.transform.up * upSpeed, ForceMode.Impulse);
        }
        lootList.Clear();
    }
}
