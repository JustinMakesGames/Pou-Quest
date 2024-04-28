using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fading : MonoBehaviour
{
    public static Fading instance;

    
    
    public Image blackScreen;
    
    public float blackScreenFadeSpeed;

    

    private float fovSpeed = 30f;   
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
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
            Camera.main.fieldOfView -= fovSpeed * Time.deltaTime;
            yield return null;
        }
        Camera.main.fieldOfView = 60;       
    }

    private IEnumerator BlackScreenFading()
    {
        while (blackScreen.color.a < 0.98f)
        {
            blackScreen.color += new Color(0, 0, 0, blackScreenFadeSpeed * Time.deltaTime);
            yield return null;
        }

        CreateBattleArena.instance.SetCameraInPosition();
        while (blackScreen.color.a >= 0)
        {
            blackScreen.color -= new Color(0, 0, 0, blackScreenFadeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    

    

    




}
