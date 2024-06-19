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
    private List<Resolution> resolutionList = new();
    private double currentRefRate;
    public int currentResolutionIndex = 0;
    public string currentRes;
    public bool fullscreen;
    public static ResManager instance;
    public int fpsLimit;
    public GameObject obj;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        fullscreen = Save.instance.saveData.fullScreen;
        Screen.fullScreen = fullscreen;
        foreach(var v in resolutionDropdown)
        {
            v.ClearOptions();
        }
        resolutions = Screen.resolutions;
        resolutionList = new List<Resolution>();
        currentRefRate = Screen.currentResolution.refreshRateRatio.value;
        Debug.LogWarning(resolutions.Length);
        for (int i = 0; i < resolutions.Length; i++)
        {
            float refRate = (float)currentRefRate;
            float rate = (float)resolutions[i].refreshRateRatio.value;
            if (Mathf.Approximately(refRate, rate))
            {
                resolutionList.Add(resolutions[i]);
            }
            else
            {
                Debug.LogWarning(refRate.ToString() + rate.ToString());
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
            StartCoroutine(Delay());
        }
        Debug.Log(resolutionList.Count.ToString());
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutionList[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, fullscreen);
        foreach(var v in resolutionDropdown)
        {
            v.value = resolutionIndex;
            v.RefreshShownValue();
        }
    }
    public void FPSLimit(string target)
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

    IEnumerator Delay()
    {
        yield return null;
        SetResolution(Save.instance.saveData.resolution);
    }

}