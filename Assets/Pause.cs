using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public GameObject PauseScreen;
    public GameManager gameManager;
    public LevelManager LevelManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        LevelManager = LevelManager.instance;
        LevelManager.PauseManager = this;
    }

    public void PauseGame()
   {
       Time.timeScale = 0.001f;
   }
   
   public void ResumeGame()
   {
       PauseScreen.SetActive(false);
       Time.timeScale = 1f;
   }
}
