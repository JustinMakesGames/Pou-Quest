using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndVideo : MonoBehaviour
{
    public GameObject mainMenu;
    private void Update()
    {
        if (GetComponent<VideoPlayer>().frame >= 180)
        {
            mainMenu.SetActive(true);
            Destroy(gameObject);
        }
    }
}
