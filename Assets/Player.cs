using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    
    public int HP = 1000;
    private HealthBar healthBar;
    private LevelManager levelManager;

    private void Awake()
    {
    
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {   
        healthBar = UIManager.Instance.HealthBar;
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
                Debug.LogError(e.Message);
            }
        }
    }

    
    public void incrementHP(int value)
    {
        HP += value;
        healthBar.SetHealth(HP);
    }
}