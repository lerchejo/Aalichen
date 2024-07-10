using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject PauseScreen;
    public GameManager gameManager;

   public void PauseGame()
   {
       Time.timeScale = 0f;
   }
   
   public void ResumeGame()
   {
       PauseScreen.SetActive(false);
       Time.timeScale = 1f;
   }
}
