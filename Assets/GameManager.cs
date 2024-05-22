using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    private int HP = 0;
    private int XP = 0;
    private int coins = 0;

    [SerializeField] private TextMeshProUGUI HPText;
    [SerializeField] private TextMeshProUGUI XPText;
    [SerializeField] private TextMeshProUGUI coinsText;

    public void incrementHP(int value)
    {
        HP += value;
    }

    public void incrementXP(int value)
    {
        XP += value;
    }

    public void incrementCoins(int value)
    {
        coins += value;
    }

    public void decrementHP(int value)
    {
        HP -= value;
    }

    public void decrementCoins(int value)
    {
        coins -= value;
    } 

    private void Update()
    {
        HPText.text = "HP: " + HP;
        XPText.text = "XP: " + XP;
        coinsText.text = "Coins: " + coins;
    }
}