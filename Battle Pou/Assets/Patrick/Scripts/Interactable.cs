using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    private bool isGambling;
    public Sprite[] sprites;
    public List<int> ints = new List<int>();
    public GameObject[] gamblingPanels;
    public GameObject player;
    float t = 0;
    float egn = 0.1f;
    public bool rigged;
    public GameObject coin;
    public int coinsToWin;
    private void Start()
    {
        player = FindAnyObjectByType<PlayerOverworld>().gameObject;
    }

    public void StartGambling()
    {
        if (!isGambling)
        {
            transform.GetChild(5).GetComponent<Animator>().Play("GamblingMachine", -1, 0);
            StartCoroutine(Gamble());
        }

    }
    IEnumerator Gamble()
    {

        StartCoroutine(Lerp());
        Camera.main.GetComponent<CamMovement>().enabled = false;
        player.GetComponent<MeshRenderer>().enabled = false;
        Debug.Log("started gambling");
        isGambling = true;
        foreach (GameObject go in gamblingPanels)
        {
            go.GetComponent<Image>().sprite = null;
        }
        if (!rigged)
        {
            for (int i = 0; i < gamblingPanels.Length; i++)
            {
                int p = 0;
                for (int g = 0; g < 240; g++)
                {
                    int r = Random.Range(0, sprites.Length);
                    gamblingPanels[i].GetComponent<Image>().sprite = sprites[r];
                    p = r;
                    yield return new WaitForSeconds(0.01f);
                }
                ints.Add(p);
            }
        }
        else
        {
            int final = Random.Range(0, sprites.Length);
            for (int i = 0; i < gamblingPanels.Length; i++)
            {
                int p = 0;
                for (int g = 0; g < 240; g++)
                {

                    int r = Random.Range(0, sprites.Length);
                    if (g != 239)
                    {
                        gamblingPanels[i].GetComponent<Image>().sprite = sprites[r];
                    }
                    else
                    {
                        gamblingPanels[i].GetComponent<Image>().sprite = sprites[final];
                    }
                    p = final;
                    yield return new WaitForSeconds(0.01f);
                }
                ints.Add(p);
            }
        }
        if (ints[0] == ints[1] && ints[0] == ints[2])
        {
            StartCoroutine(WinningAnimation());
            yield return new WaitUntil(() => transform.GetChild(6).position == Camera.main.transform.position);
            Vector3 pos = transform.GetChild(1).position;
            pos.y += 0.02f;
            transform.GetChild(3).GetComponent<Animator>().Play("GamblingDoor", -1, 0);
            for (int i = 0; i < 30; i++)
            {
                Instantiate(coin, pos, Random.rotation);
                for (int c = 0; c < 3; c++)
                {
                    Instantiate(coin, new Vector3(pos.x += Random.Range(-0.2f, 0.2f), pos.y, pos.z), Random.rotation);
                    pos = transform.GetChild(1).position;
                    yield return null;
                }
                GetComponentInChildren<TMP_Text>().enabled = true;
                PlayerHandler.Instance.coins += coinsToWin;
                Debug.Log("you won");
            }
        }
        else
        {
            //things that happen when you lose
            Debug.Log("you lost");
        }
            ints.Clear();
            yield return new WaitForSeconds(2);
            player.GetComponent<PlayerOverworld>().enabled = true;
            Camera.main.GetComponent<CamMovement>().enabled = true;
            Camera.main.transform.rotation = Quaternion.Euler(65, 0, 0);
            GetComponentInChildren<TMP_Text>().enabled = false;
            player.GetComponent<MeshRenderer>().enabled = true;
            isGambling = false;
        }


        IEnumerator Lerp()
        {
            if (transform.GetChild(0).position != Camera.main.transform.position)
            {
                t += Time.deltaTime * egn;
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, transform.GetChild(0).position, t);
                Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, transform.GetChild(0).rotation, t);
                yield return null;
                StartCoroutine(Lerp());
            }
            else
            {
                yield return null;
                t = 0;
            }
        }
        IEnumerator WinningAnimation()
        {
            if (transform.GetChild(6).position != Camera.main.transform.position)
            {
                t += Time.deltaTime * egn;
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, transform.GetChild(6).position, t);
                yield return null;
                StartCoroutine(WinningAnimation());
            }
            else
            {
                yield return null;
                t = 0;
            }
        }
        IEnumerator CoinAnimation()
        {
            if (transform.GetChild(7).position != Camera.main.transform.position)
            {
                GameObject newCoin = Instantiate(coin, transform.GetChild(7).GetChild(0).position, Quaternion.identity);
                t += Time.deltaTime * egn;
                Camera.main.transform.position = Vector3.Lerp(newCoin.transform.position, transform.GetChild(7).position, t);
                yield return null;
                StartCoroutine(CoinAnimation());
            }
            else
            {
                t = 0;
                yield return null;
            }
        }
    }