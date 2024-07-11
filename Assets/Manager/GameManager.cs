using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int XP = 0;
    public int Damage = 10;
    public int coins = 0;
    public int level = 0;
    
    public LevelUpManager LevelUpManager;
    public GameObject PauseScreen;
    public int[] levelThresholds = new int[] {0, 10, 100, 500, 1000};
    
    protected internal List<GameObject> enemies;
    private TextMeshProUGUI EnemyCounter;

    public ExperienceBar experienceBar;

    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] private TextMeshProUGUI coinsText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
    }
    
    private void Start()
    {
        LevelText.SetText("Level: " + level);
        experienceBar.SetXP(1);
        experienceBar.SetMaxXP(levelThresholds[1]);
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        EnemyCounter = UIManager.Instance.EnemyCounter;
    }

    
    
    public void incrementXP(int value)
    {
        XP += value;
        experienceBar.SetXP(XP);
        CheckLevelUp();
        LevelText.SetText("Level: " + level);
    }

    public void incrementCoins(int value)
    {
        coins += value;
    }

    public void decrementCoins(int value)
    {
        coins -= value;
    } 

    private void Update()
    {
        coinsText.text = "Coins: " + coins;
        EnemyCounter.SetText("Enemies: " + enemies.Count);
        
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
        if (level < 5 && XP >= levelThresholds[level + 1])
        {
            Debug.Log("ABCSCSC");
            Time.timeScale = 0f;
            LevelUpManager.LevelUp();
            level++;
            experienceBar.UpdateXP(XP);
        }
    }
}