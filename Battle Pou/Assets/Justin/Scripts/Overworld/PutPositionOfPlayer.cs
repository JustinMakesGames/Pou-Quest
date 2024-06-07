using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PutPositionOfPlayer : MonoBehaviour
{
    public string sourceSceneName;
    public string targetSceneName;
    public GameObject objectToMove;
    public Transform newPosition;

    void Awake()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Update the current and previous scene names
        SceneTracker.UpdateCurrentScene(scene.name);
        // Check if the previous scene is the source scene and the current scene is the target scene
        if (SceneTracker.previousSceneName == sourceSceneName && scene.name == targetSceneName)
        {
            // Move the object to the new position
            if (objectToMove != null)
            {
                objectToMove.transform.position = newPosition.position;
            }
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
