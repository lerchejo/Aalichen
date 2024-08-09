using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
   
    #region Instance
    
    public static LevelManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }
    
    #endregion Instance


    
    
    public GameObject deathScreen;
    public AudioSource deathSound;
    
    public IntermissionManager intermissionManager;

    private void Start()
    {
        intermissionManager = IntermissionManager.instance;
    }

    public void ReloadLevel()
    {
        Time.timeScale = 1f;
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
        intermissionManager.IntermissionActivate();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void startGame()
    {
        SceneManager.LoadScene("CutScene");
    }
    
    public void enableDeathScreen()
    {
        deathSound.Play();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        deathScreen.SetActive(true);
    }
}
