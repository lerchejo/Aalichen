using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] internal TextMeshProUGUI dialogWindow;
    [SerializeField] internal TextMeshProUGUI pressE;
    [SerializeField] internal HealthBar HealthBar;
    [SerializeField] internal ExperienceBar ExperienceBar;
    [SerializeField] internal Slider DashCooldown;
    
    
    [SerializeField] internal GameObject DeathScreen;
    [SerializeField] internal GameObject LevelUpScreen;
    [SerializeField] internal GameObject PauseMenu;
    [SerializeField] internal TextMeshProUGUI EnemyCounter;

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
        }
    }

    // Other methods for updating UI...
}
