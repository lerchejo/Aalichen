using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public int HP = 1000;
    public int XP = 0;
    public int coins = 0;
    public int level = 0;
    
    public GameObject LevelUpScreen;
    public LevelUpManager LevelUpManager;
    public GameObject PauseScreen;
    // New variable for XP thresholds for each level
    public int[] levelThresholds = new int[] {0, 10, 100, 500, 1000};
    
    public HealthBar healthBar;
    public ExperienceBar experienceBar;

    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] private TextMeshProUGUI coinsText;

    private void Start()
    {
        healthBar.SetMaxHealth(HP);
        experienceBar.SetXP(1);
        experienceBar.SetMaxXP(levelThresholds[1]);
    }

    public void incrementHP(int value)
    {
        HP += value;
        healthBar.SetHealth(HP);
    }

    public void incrementXP(int value)
    {
        XP += value;
        experienceBar.SetXP(XP);
        CheckLevelUp(); // Check if player has leveled up
        LevelText.SetText("Level: " + level);
    }

    public void incrementCoins(int value)
    {
        coins += value;
    }

    public void decrementHP(int value)
    {
        HP -= value;
        healthBar.SetHealth(HP);
    }

    public void decrementCoins(int value)
    {
        coins -= value;
    } 

    private void Update()
    {
        coinsText.text = "Coins: " + coins;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseScreen.activeSelf)
            {
                PauseScreen.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
            }
            else
            {
                PauseScreen.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
            }
        }
    }
    
    private void CheckLevelUp()
    {
        // If player's level is less than 5 and their XP is greater than or equal to the threshold for the next level
        if (level < 5 && XP >= levelThresholds[level + 1])
        {
            Time.timeScale = 0f;
            LevelUpManager.LevelUp();

            level++; // Increase player's level
            experienceBar.UpdateXP(XP);
        }
    }
}
