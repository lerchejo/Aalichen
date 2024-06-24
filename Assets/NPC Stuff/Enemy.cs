using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float Health = 50f;
    public int damage = 1;
    public GameManager gameManager;
    public GameObject Player;
    public LevelManager levelManager;
    public AudioSource damageSound;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!damageSound.isPlaying)
            {
                damageSound.Play();
            }
            
            gameManager.decrementHP(damage);
            if (gameManager.HP < 0)
            {
                Time.timeScale = 0f;
                levelManager.enableDeathScreen();
                try
                {
                    Destroy(Player);
                }catch (Exception e)
                {
                    Debug.LogError("Player not found in the scene.");
                }
                
                
            }
        }
      // else if (other.CompareTag("NPC"))
      // {
      //     // Check if the collided object is the damage collider
      //    
      //         var npc = other.GetComponentInParent<NPC>(); // Get the NPC component from the parent of the collided object
      //         if (npc == null) return;
      //         
      //         npc.HP -= damage;
      //         npc.healthBar.SetHealth(npc.HP); // Update the health bar
      //         
      //         if (npc.HP < 0)
      //         {
      //             Destroy(npc.gameObject);
      //         }
      // }
    }
}