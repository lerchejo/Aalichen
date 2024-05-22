using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreCheck : MonoBehaviour
{
    //This is temporary; replace with the right one
    public Inventory playerInventory;

    private DialogWindow dialogWindow;

    public float timer = 10;
    private float currentTimer = 100;

    private bool LevelCleared = false;

    private bool LevelChangeReady = false;

    public int NeededScore = 1000;

    private void Start()
    {
        dialogWindow = DialogWindow.instance;
    }

    private void Update()
    {
        if (!LevelChangeReady)
        {
            if (LevelCleared)
            {
                currentTimer -= Time.deltaTime;
            }

            if (currentTimer <= 0)
            {
                LevelChangeReady = true;
            }
        }
    }

    private void LateUpdate()
    {
        if(playerInventory.Coins >= NeededScore && !LevelCleared) 
        {
            dialogWindow.ActivateDialog("The Bus is coming in 10 minutes!\nGo to the bus station!");
            StartCoroutine(DialogClose());
            currentTimer = timer;
            LevelCleared = true;
        }
    }

    IEnumerator DialogClose()
    {
        yield return new WaitForSeconds(1.5f);
        dialogWindow.DeActivateDialog();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (LevelCleared && !LevelChangeReady)
            {
                dialogWindow.ActivateDialog("The Bus is coming in " + (int)currentTimer + " minutes!");
                StartCoroutine(DialogClose());
            }
            else if (LevelChangeReady)
            {
               
            }
            else
            {
                dialogWindow.ActivateDialog("You still need " + (NeededScore - playerInventory.Coins) + " Coins!");
            }
        }
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
