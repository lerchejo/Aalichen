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

   // private TextMeshProUGUI dialogWindow;
   // private TextMeshProUGUI pressE;

    public float timer = 10;
    private float currentTimer = 100;

    private bool LevelCleared = false;
    private GameManager gm;

    private bool LevelChangeReady = false;
    
    public AudioSource DepartSound;
    public AudioSource ArriveSound;
    public AudioSource IdleSound;
    private Animator animator;

    public int NeededScore = 1000;

    private GameObject player;
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        gm = GameManager.Instance;
        
        StartCoroutine(StepOutOfBus());
    }
    IEnumerator StepOutOfBus(float seconds = 1.5f)
    {
        var Movement = player.GetComponent<NewMovement>();
        if(Movement != null)
        {
            Movement.enabled = false;
        }
        yield return new WaitForSeconds(seconds);
        player.GetComponent<SpriteRenderer>().enabled = true;
        if (Movement)
        {
            Movement.enabled = true;
        }
    }
    private void LateUpdate()
    {
        currentTimer = timer;
        
      //  print(npc.enemies.Count + " Enemies left!");
        
        if(gm.enemies.Count == 0 && !LevelCleared) 
        {
            Debug.Log(animator.name);
            ActivateDialog("The Bus is coming in 10 minutes!\nGo to the bus station!");
            //StartCoroutine(DialogClose());
            //currentTimer = timer;
            
           Debug.Log("Level Cleared");
           animator.SetTrigger("LevelCleared");
           animator.SetBool("WaitAtStop", true);
                
            
            
            LevelCleared = true;
        }
    }

    IEnumerator DialogClose(float seconds = 5f)
    {
        yield return new WaitForSeconds(seconds);
        UIManager.Instance.dialogWindow.gameObject.SetActive(false);
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
        UIManager.Instance.dialogWindow.SetText("The Bus is coming in 10 Minutes!\nGo to the bus station!");
        UIManager.Instance.dialogWindow.gameObject.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.pressE.SetText("Joo, drück mal E!");
            UIManager.Instance.pressE.gameObject.SetActive(true);
            if (LevelCleared && Input.GetKeyDown(KeyCode.E))
            {
                Destroy(player);
                LevelManager.instance.LoadNextLevel();
               //if (SceneManager.GetActiveScene().buildIndex == 1)
               //{
               //    animator.SetBool("DriveOff", true);
               //    StartCoroutine(ChangeScene());

               //}
              //  else if (SceneManager.GetActiveScene().buildIndex == 2)
              //  {
                    animator.SetBool("WaitAtStop", false);
                    //StartCoroutine(ChangeScene(3f));

                //}
            }
            UIManager.Instance.pressE.gameObject.SetActive(false);
        }
       
    }
    
    IEnumerator ChangeScene(float seconds = 2.2f)
    {
        yield return new WaitForSeconds(seconds);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        DepartSound.Play();
    }
    
    public void PlayBusArriveSound()
    {
        ArriveSound.Play();
    }
    
    public void PlayBusIdleSound()
    {
        IdleSound.Play();
    }



}
