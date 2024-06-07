using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneTracker
{
    public static string previousSceneName = "";
    public static string currentSceneName = "";

    public static void UpdateCurrentScene(string sceneName)
    {
        previousSceneName = currentSceneName;
        currentSceneName = sceneName;
    }
}
