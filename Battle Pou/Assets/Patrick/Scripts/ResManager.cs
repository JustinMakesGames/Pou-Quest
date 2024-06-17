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
    public double currentRefRate;
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
        resolutionList.Clear();
        foreach(var v in resolutionDropdown)
        {
            v.ClearOptions();
        }
        currentRefRate = Screen.currentResolution.refreshRateRatio.value;
        foreach (var res in Screen.resolutions)
        {
            float refRate = Convert.ToSingle(currentRefRate);
            float rate = (float)res.refreshRateRatio.value;
             if (Mathf.Approximately(refRate, rate))
            {
                resolutionList.Add(res);
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