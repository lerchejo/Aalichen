using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreCheck : MonoBehaviour
{
    //This is temporary; replace with the right one
    public Inventory playerInventory;

    public TextMeshProUGUI dialogWindow;

    public float timer = 10;
    private float currentTimer = 100;

    private bool LevelCleared = false;

    private bool LevelChangeReady = false;

    public int NeededScore = 1000;

    private void Start()
    { }

    private void Update()
    {
        if (!LevelChangeReady)
        {
            if (LevelCleared)
            {
                currentTimer -= Time.deltaTime;
            }

          //  if (currentTimer <= 0)
          //  {
          //      LevelChangeReady = true;
          //  }
        }
    }

    private void LateUpdate()
    {
        currentTimer = timer;

        if(playerInventory.Coins >= NeededScore && !LevelCleared) 
        {
            //ActivateDialog("The Bus is coming in 10 minutes!\nGo to the bus station!");
            //StartCoroutine(DialogClose());
            currentTimer = timer;
            LevelCleared = true;
        }
    }

    IEnumerator DialogClose(float seconds = 5f)
    {
        yield return new WaitForSeconds(seconds);
        dialogWindow.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            ActivateDialog("The Bus is coming in " + (int)currentTimer + " minutes!");
            StartCoroutine(DialogClose());
            if (LevelCleared && !LevelChangeReady)
            {
                ActivateDialog("The Bus is coming in " + (int)currentTimer + " minutes!");
               
                StartCoroutine(DialogClose());
            }
            else if (LevelChangeReady)
            {
               
            }
            else
            {
                ActivateDialog("The Bus is coming in " + (int)currentTimer + " minutes!");
            }
        }
    }

    private void ActivateDialog(String text)
    {
        dialogWindow.SetText(text);
        dialogWindow.gameObject.SetActive(true);
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (LevelChangeReady && Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //TODO::change later! 
            
            }
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            StartCoroutine(DialogClose());
        }
    }

}
