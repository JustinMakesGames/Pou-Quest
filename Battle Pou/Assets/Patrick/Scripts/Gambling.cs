using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gambling : MonoBehaviour
{
    private bool isGambling;
    public Sprite[] sprites;
    public List<int> ints = new List<int>();
    public GameObject[] gamblingPanels;
    public void StartGambling()
    {
        if (!isGambling)
        {
            StartCoroutine(Gamble());
        }

    }


    IEnumerator Gamble()
    {
        isGambling = true;
        foreach (GameObject go in gamblingPanels)
        {
            go.GetComponent<Image>().sprite = null;
        }
        for (int i = 0; i < gamblingPanels.Length; i++)
        {
            int p = 0;
            for (int g = 0; g < 15; g++)
            {
                int r = Random.Range(0, sprites.Length);
                gamblingPanels[i].GetComponent<Image>().sprite = sprites[r];
                p = r;
                yield return new WaitForSeconds(0.2f);
            }
            ints.Add(p);
        }
        if (ints[0] == ints[0 + 1] && ints[0] == ints[0 + 2])
        {
            print("you won");
        }
        else
        {
            Debug.Log("you lost");
        }
        ints.Clear();
        isGambling = false;
    }
}
