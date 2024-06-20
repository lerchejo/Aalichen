using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
   
    public GameObject deathScreen;
    
    public void ReloadLevel()
    {
        string currentLevelName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentLevelName);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        deathScreen.SetActive(false);
    }
    // Update is called once per frame
    
    public void toMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void enableDeathScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        deathScreen.SetActive(true);
    }
}
