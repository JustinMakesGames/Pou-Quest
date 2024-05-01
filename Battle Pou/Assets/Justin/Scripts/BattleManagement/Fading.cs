using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fading : MonoBehaviour
{
    public static Fading instance;


    public Transform player;
    public Image blackScreen;
    private Transform cam;
    public float blackScreenFadeSpeed;


    public float rotationSpeed;
    private float fovSpeed = 30f;   
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        cam = Camera.main.transform;
    }
    

    

    public void ActivateIEnumerator()
    {
        StartCoroutine(FovChange());
        StartCoroutine(BlackScreenFading());
    }

    private IEnumerator FovChange()
    {
        
        while (Camera.main.fieldOfView >= 30)
        {
            Quaternion lookRotation = Quaternion.LookRotation(player.position - cam.position);
            cam.rotation = Quaternion.Lerp(cam.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            Camera.main.fieldOfView -= fovSpeed * Time.deltaTime;
            yield return null;
        }
        CreateBattleArena.instance.SetCameraInPosition();
        Camera.main.fieldOfView = 60;       
    }

    private IEnumerator BlackScreenFading()
    {
        while (blackScreen.color.a < 0.98f)
        {
            blackScreen.color += new Color(0, 0, 0, blackScreenFadeSpeed * Time.deltaTime);
            yield return null;
        }

        
        while (blackScreen.color.a >= 0)
        {
            blackScreen.color -= new Color(0, 0, 0, blackScreenFadeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    

    

    




}
