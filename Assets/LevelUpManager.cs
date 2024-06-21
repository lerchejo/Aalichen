using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelUpManager : MonoBehaviour
{
    // Start is called before the first frame update
        public GameObject levelUpScreen;
        public GameManager GameManager; // Reference to the GameManager
        public Movement Movement; // Reference to the MovementScript

        public Button Option1Button;
        public Button Option2Button;
        public Button Option3Button;
        
        public TextMeshProUGUI Option1Text;
        public TextMeshProUGUI Option2Text;
        public TextMeshProUGUI Option3Text;

        
        public class Upgrade
        {
            public string Name { get; set; }
            public Action<GameManager, Movement> Apply { get; set; }
        }
    
    

    private Upgrade[] upgrades = new Upgrade[]
    {
        new Upgrade { Name = "Increase Health", Apply = (gm, mv) => gm.HP +=  Random.Range(25, 75) },
        new Upgrade { Name = "Gain Coins", Apply = (gm, mv) => gm.coins += Random.Range(10, 50)},
        new Upgrade { Name = "Increase Speed", Apply = (gm, mv) => mv.runSpeed += Random.Range(1, 2) },
        new Upgrade { Name = "Dash Duration", Apply = (gm, mv) => mv.dashDuration += Random.Range(0.2f, 0.4f) },
        new Upgrade { Name = "Increase Dash Speed", Apply = (gm, mv) => mv.runSpeed += Random.Range(1, 2) },
        new Upgrade { Name = "Increase Player Strength", Apply = (gm, mv) => gm.Damage += Random.Range(5,10) }

    };
    
    private void DisplayUpgrades(Upgrade[] upgrades)
    {
        // Display the upgrades to the user
        // This will depend on your UI implementation
        // For example, you might have a method like this:
        Option1Text.SetText("Option 1: " + upgrades[0].Name);
        Option2Text.SetText("Option 2: " + upgrades[1].Name);
        Option3Text.SetText("Option 3: " + upgrades[2].Name);
    }

    
    public void LevelUp()
    {
        
        levelUpScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Select three random upgrades
        Upgrade[] selectedUpgrades = new Upgrade[3];
        List<int> selectedIndices = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            int index;
            do
            {
                index = Random.Range(0, upgrades.Length);
            } while (selectedIndices.Contains(index));
            
            selectedIndices.Add(index);
            selectedUpgrades[i] = upgrades[index];
        }

        // Assign each button to apply a different upgrade
        Option1Button.onClick.AddListener(() =>
        {
            selectedUpgrades[0].Apply(GameManager, Movement);
            levelUpScreen.SetActive(false);
            Time.timeScale = 1f;

        });
        Option2Button.onClick.AddListener(() =>
        {
            selectedUpgrades[1].Apply(GameManager, Movement);
            levelUpScreen.SetActive(false);
            Time.timeScale = 1f;

        });
        Option3Button.onClick.AddListener(() =>
        {
            selectedUpgrades[2].Apply(GameManager, Movement);
            levelUpScreen.SetActive(false);
            Time.timeScale = 1f;

        });

        // Display the upgrades to the user
        // This will depend on your UI implementation
        // For example, you might have a method like this:
        DisplayUpgrades(selectedUpgrades);
    }
}

