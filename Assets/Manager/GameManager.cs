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
    
    public HealthBar healthBar;
    public ExperienceBar experienceBar;

    //[SerializeField] private TextMeshProUGUI HPText;
    [SerializeField] private TextMeshProUGUI XPText;
    [SerializeField] private TextMeshProUGUI coinsText;

    private void Start()
    {
        healthBar.SetMaxHealth(HP);
        experienceBar.SetXP(0);
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
        //HPText.text = "HP: " + HP;
        //XPText.text = "XP: " + XP;
        coinsText.text = "Coins: " + coins;
    }
}
