using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using System;

public class ResManager : MonoBehaviour
{
    public TMP_Dropdown[] resolutionDropdown;
    private Resolution[] resolutions;
    private List<Resolution> resolutionList;
    private float currentRefRate;
    public int currentResolutionIndex = 0;
    public string currentRes;
    public bool fullscreen;
    public static ResManager instance;
    public int fpsLimit;
    public GameObject obj;
    void Start()
    {
        instance = this;
        foreach(var v in resolutionDropdown)
        {
            v.ClearOptions();
        }
        resolutions = Screen.resolutions;
        resolutionList = new List<Resolution>();
        currentRefRate = Mathf.Round((float)Screen.currentResolution.refreshRateRatio.value);

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (Mathf.Round((float)resolutions[i].refreshRateRatio.value) == currentRefRate)
            {
                resolutionList.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < resolutionList.Count; i++)
        {
            string resolutionOption = resolutionList[i].width + "x" + resolutionList[i].height + " " + Mathf.Round((float)resolutionList[i].refreshRateRatio.value) + " Hz";
            options.Add(resolutionOption);
            if (resolutionList[i].width == Screen.width && resolutionList[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
            foreach (var v in resolutionDropdown)
            {
                v.RefreshShownValue();
            }
        }
        foreach (var v in resolutionDropdown)
        {
            v.AddOptions(options);
            v.value = currentResolutionIndex;
            v.RefreshShownValue();
        }

    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutionList[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, fullscreen);
    }

    public void GetGameObject(GameObject calledObject)
    {
        obj = calledObject;
    }
    public void FPSLimit(string target)
    {
        bool validInput = true;
        foreach (char c in target.ToCharArray())
        {
            if (!char.IsNumber(c))
            {
                validInput = false;
                break;
            }
        }
        if (validInput)
        {
            fpsLimit = Convert.ToInt32(target);
            if (fpsLimit == 0)
            {
                Application.targetFrameRate = -1;
            }
            else
            {
                Application.targetFrameRate = fpsLimit;
            }
        }
        else
        {
            obj.GetComponent<TMP_Text>().text = "Invalid input.";
        }
    }

}