using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOnMainManue : MonoBehaviour
{
    // The index of the scene where the GameObject should be destroyed
    public int sceneIndex = 0;

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene's build index matches the specified scene index
        if (scene.buildIndex == sceneIndex)
        {
            // Destroy the GameObject this script is attached to
            Destroy(gameObject);
        }
    }
}
