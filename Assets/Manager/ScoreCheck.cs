using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreCheck : MonoBehaviour
{
    //This is temporary; replace with the right one

    public TextMeshProUGUI dialogWindow;

    public float timer = 10;
    private float currentTimer = 100;

    private bool LevelCleared = false;
    public GameManager gm;

    private bool LevelChangeReady = false;
    
    public AudioSource busSound;
    private Animator animator;

    public int NeededScore = 1000;

    private GameObject player;
    public NPC npc;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

   //private void Update()
   //{
   //    if (!LevelChangeReady)
   //    {
   //        if (LevelCleared)
   //        {
   //            currentTimer -= Time.deltaTime;
   //        }

   //       if (currentTimer <= 0)
   //       {
   //           LevelChangeReady = true;
   //       }
   //    }
   //}

    private void LateUpdate()
    {
        currentTimer = timer;
        
      //  print(npc.enemies.Count + " Enemies left!");
        
        if(npc.enemies.Count == 0 && !LevelCleared) 
        {
            ActivateDialog("The Bus is coming in 10 minutes!\nGo to the bus station!");
            //StartCoroutine(DialogClose());
            //currentTimer = timer;
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
            
           // ActivateDialog("The Bus is coming in " + (int)currentTimer + " minutes!");
            
            if (LevelCleared && !LevelChangeReady)
            {
            //    ActivateDialog("The Bus is coming in " + (int)currentTimer + " minutes!");
               
              //  StartCoroutine(DialogClose());
            }
            else
            {
               // ActivateDialog("The Bus is coming in " + (int)currentTimer + " minutes!");
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
            if (LevelCleared && Input.GetKeyDown(KeyCode.E))
            {
                Destroy(player);
                animator.SetBool("DriveOff", true);
                StartCoroutine(ChangeScene());

            }
        }
       
    }
    
    IEnumerator ChangeScene(float seconds = 2.2f)
    {
        yield return new WaitForSeconds(seconds);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            StartCoroutine(DialogClose());
        }
    }


    public void PlayBusSound()
    {
        busSound.Play();
    }



}
