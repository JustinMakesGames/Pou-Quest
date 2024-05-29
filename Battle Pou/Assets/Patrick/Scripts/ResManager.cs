using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class ResManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private List<Resolution> resolutionList;
    private float currentRefRate;
    private int currentResolutionIndex = 0;
    public string currentRes;
    public bool fullscreen;

    void Start()
    {
        resolutionDropdown.ClearOptions();
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
            resolutionDropdown.RefreshShownValue();
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutionList[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, fullscreen);
    }


}