using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class NPC : MonoBehaviour
{

    //public NPCtype type;
    //public NpcInventory NPCinventory;
    //public DialogObject DialogObject;
    public string PersonName;
    private bool shouldFollow = false; 
    private bool inDistance = false;
    public float speed = 5f; // Speed of NPC
    public int amount = 10; // Amount of coins to give
    public GameManager GameManager;

    public GameObject player;
    public int HP = 500; // Health of NPC
    [FormerlySerializedAs("HealthBar")] public HealthBar healthBar; // Experience of NPC
    //public GameObject enemy; // Reference to the enemy
    // public GameObject enemy; // Reference to the enemy
    [SerializeField] private AudioSource[] hireJerkSounds;
    //Last played hireJerkSound, must be initialized to avoid error
    private AudioSource lastHireJerkSound;

    private List<GameObject> enemies; // List of enemies

    private GameObject target; // The enemy that the NPC is currently following
    
    //private ShopDisplay Shopdisplay;
    private void Start()
    {
        //FollowPlayer();
        // DialogWindow = DialogWindow.instance;
        //Shopdisplay = ShopDisplay.instance;
        
        healthBar.SetMaxHealth(HP);
        enemies = GameManager.enemies;

        
        // Initialize the lastHireJerkSound to the first sound in the array to avoid Errors
        lastHireJerkSound = hireJerkSounds[0];
    }
    
    
    private void Update()
    {
//        EnemyCounter.SetText("Enemies: " + enemies.Count);
        // List to store enemies with aggro
        List<GameObject> aggroEnemies = new List<GameObject>();
        
        // List to store enemies to be removed
        List<GameObject> enemiesToRemove = new List<GameObject>();
        
        // Iterate over the list of enemies
        foreach (GameObject enemy in enemies)
        {
            // Check if the enemy has aggro
            if (enemy != null && enemy.GetComponent<AIChase>().deltaDistance) // Assume Enemy is a script attached to the enemy GameObject that has a boolean flag isAggro
            {
                // Add the enemy to the list of enemies with aggro
                aggroEnemies.Add(enemy);
            }
        }
        
        // If there are enemies with aggro
        if (aggroEnemies.Count > 0)
        {
            // Select a random enemy from the list of enemies with aggro
            target = aggroEnemies[Random.Range(0, aggroEnemies.Count)];
        }
        else
        {
            // If there are no enemies with aggro, set the player as the target
            target = player;
        }
        
        
        // If a target has been selected
        if (target != null && target.activeInHierarchy == false)
        {
            // Set the target to null
            target = null;
        }
        
        // Calculate the distance to the enemy
        // Iterate over the list of enemies
        foreach (GameObject enemy in enemies)
        {
            // Calculate the distance to the enemy
            if (enemy == null)
            {
                continue;
            } 
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

            // If the enemy is within range
            if (distanceToEnemy < 2.9f)
            {
                // Decrease the NPC's health
                HP--;
                healthBar.SetHealth(HP);

                // If the NPC's health is less than or equal to 0, destroy the NPC
                if (HP <= 0)
                {
                    Destroy(this.gameObject);
                    break; // Exit the loop as the NPC is destroyed
                }

                // If the enemy is destroyed, add it to the list of enemies to be removed
                if (enemy.activeInHierarchy == false)
                {
                    Debug.Log("Adding enemy to remove");
                    enemiesToRemove.Add(enemy);
                }
            }
        }
        
        // Remove the destroyed enemies from the enemies list
        foreach (GameObject enemy in enemiesToRemove)
        {
            Debug.Log("Removing enemy");
            enemies.Remove(enemy);
        }
        
        if (shouldFollow && target != null)
        {
            FollowPlayer(target);
        }
        
        if (Input.GetKeyDown(KeyCode.E) && inDistance)
        {
            Debug.Log("Hired Jerk!");
            //Play a random hireJerkSound
            if (!lastHireJerkSound.isPlaying)
            {
                lastHireJerkSound = hireJerkSounds[Random.Range(0, hireJerkSounds.Length)];
                lastHireJerkSound.Play();
            }
            
            if (!shouldFollow)
            {
                GameManager.decrementCoins(amount);
                Debug.Log("E pressed");
            }
            shouldFollow = !shouldFollow;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player in distance");
            inDistance = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inDistance = false;
        }
    }
    
    // private void FollowPlayer(GameObject target)
    // {
    //     // Calculate the midpoint between the player and the enemy
    //     Vector2 midpoint = (player.transform.position + target.transform.position) / 2;
    //
    //     // Calculate the direction to the midpoint
    //     Vector2 direction = (midpoint - (Vector2)transform.position).normalized;
    //
    //     // Calculate the target position 2 meters away from the midpoint in the direction of the midpoint
    //     Vector2 targetPosition = midpoint + direction * 2;
    //
    //     // Calculate the distance to the target position
    //     float distanceToTarget = Vector2.Distance(transform.position, targetPosition);
    //
    //     // If the distance is less than a certain threshold, stop moving
    //     if (distanceToTarget < 6f)
    //     {
    //         return;
    //     }
    //
    //     // Move towards the target position
    //     float step = speed * Time.deltaTime;
    //     transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
    // }
    
    private void FollowPlayer(GameObject target)
    {
        // Berechne den Mittelpunkt zwischen Spieler und Ziel
        Vector2 midpoint = (player.transform.position + target.transform.position) / 2;

        // Berechne die Richtung von der aktuellen Position zum Mittelpunkt
        Vector2 directionToMidpoint = (midpoint - (Vector2)transform.position).normalized;

        // Berechne die Zielposition 2 Meter vom Mittelpunkt in der berechneten Richtung
        Vector2 targetPosition = midpoint + directionToMidpoint * 2;

        // Berechne die Distanz zur Zielposition
        float distanceToTarget = Vector2.Distance(transform.position, targetPosition);

        // Wenn die Distanz kleiner als eine bestimmte Schwelle ist, höre auf zu bewegen
        if (distanceToTarget < 6f)
        {
            return;
        }

        // Bewege dich zur Zielposition
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
    }

}
