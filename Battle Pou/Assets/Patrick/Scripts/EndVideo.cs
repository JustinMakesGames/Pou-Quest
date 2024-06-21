using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndVideo : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject blackScreen;
    private void Update()
    {
        if (GetComponent<VideoPlayer>().isPlaying)
        {
            blackScreen.SetActive(false);
        }
        if (GetComponent<VideoPlayer>().frame >= 180)
        {
            mainMenu.SetActive(true);
            mainMenu.GetComponentInParent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}
