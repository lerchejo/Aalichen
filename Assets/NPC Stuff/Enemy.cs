using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{

    [FormerlySerializedAs("Health")] public float health = 50f;
    public float HealthMax = 50f;
    public int damage = 1;
    public GameManager gameManager;
    public GameObject Player;
    public Player PlayerScript;
    public LevelManager levelManager;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
         animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }

    private void Update()
    {
        if(health / HealthMax < 1f && health / HealthMax > 0.66f)
        {
            animator.SetBool("Damage1", true);
            animator.SetBool("Walk Up", false);
            animator.SetBool("Walk Down", false);
            animator.SetBool("Walk Left", false);
            animator.SetBool("Walk Right", false);
        }else if(health / HealthMax > 0.33f && health / HealthMax < 0.66f)
        {
            animator.SetBool("Damage2", true);
            animator.SetBool("Walk Up", false);
            animator.SetBool("Walk Down", false);
            animator.SetBool("Walk Left", false);
            animator.SetBool("Walk Right", false);
        }else if(health / HealthMax < 0.33f)
        {
            animator.SetBool("Damage3", true);
            animator.SetBool("Walk Up", false);
            animator.SetBool("Walk Down", false);
            animator.SetBool("Walk Left", false);
            animator.SetBool("Walk Right", false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerScript.decrementHP(damage);
           
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