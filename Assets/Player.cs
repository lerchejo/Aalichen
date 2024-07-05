using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int HP = 1000;
    public HealthBar healthBar;
    private LevelManager levelManager;

    private void Start()
    {
        healthBar.SetMaxHealth(HP);
        levelManager = FindObjectOfType<LevelManager>();
    }

 
    
    public void decrementHP(int value)
    {
        HP -= value;
        healthBar.SetHealth(HP);
        
        if (HP < 0)
        {
            Time.timeScale = 0f;
            levelManager.enableDeathScreen();
            try
            {
                Destroy(gameObject);
            }catch (Exception e)
            {
                Debug.LogError("Player not found in the scene.");
            }
        }
    }

    
    public void incrementHP(int value)
    {
        HP += value;
        healthBar.SetHealth(HP);
    }
}