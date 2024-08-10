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
        
            instance = this;
       
    }
    
    #endregion Instance


    
    
    public GameObject deathScreen;
    public AudioSource deathSound;
    
    public IntermissionManager intermissionManager;

    UIManager uiManager;

    public Pause PauseManager;
    private void Start()
    {
        intermissionManager = IntermissionManager.instance;
        uiManager = UIManager.Instance;
        deathScreen = uiManager.gameObject.GetComponent<FindDeathScreen>().DeathScreen;
    }

    public void ReloadLevel()
    {
        Time.timeScale = 1f; 
        deathScreen.SetActive(false);
        string currentLevelName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentLevelName);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
       
    }
    // Update is called once per frame
    
    public void toMainMenu()
    {
        PauseManager.ResumeGame();
        SceneManager.LoadScene("MainMenu");
    }
    
    public void toManual()
    {
        SceneManager.LoadScene("Manual");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void LoadNextLevel()
    {
        intermissionManager = IntermissionManager.instance;
        intermissionManager.IntermissionActivate();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void startGame()
    {
        SceneManager.LoadScene("CutScene");
    }
    
    public void enableDeathScreen()
    {
        deathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        deathSound.Play();
    }
}
