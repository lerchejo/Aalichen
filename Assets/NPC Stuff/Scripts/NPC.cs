using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


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
    // Flag to determine if NPC should follow player
    public GameObject player;
    public int HP = 500; // Health of NPC
    [FormerlySerializedAs("HealthBar")] public HealthBar healthBar; // Experience of NPC
    public GameObject enemy; // Reference to the enemy



    //private ShopDisplay Shopdisplay;
    private void Start()
    {
        //FollowPlayer();
        // DialogWindow = DialogWindow.instance;
        //Shopdisplay = ShopDisplay.instance;
        
        healthBar.SetMaxHealth(HP);
    }
    
    
    private void Update()
    {
        // Calculate the distance to the enemy
        float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

        if (distanceToEnemy < 2.9)
        {
            HP--; // Assume damage is a predefined variable
            healthBar.SetHealth(HP);
            if(HP < 0) Destroy(this.gameObject);
            
        }
        
        if (shouldFollow)
        {
            FollowPlayer();
        }
        
        if (Input.GetKeyDown(KeyCode.E) && inDistance)
        {
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
    private void FollowPlayer()
    {
        // Calculate the midpoint between the player and the enemy
        Vector2 midpoint = (player.transform.position + enemy.transform.position) / 2;

        // Calculate the distance to the midpoint
        float distanceToMidpoint = Vector2.Distance(transform.position, midpoint);

        // If the distance is less than a certain threshold, stop moving
        if (distanceToMidpoint < 0.5f)
        {
            return;
        }

        // Move towards the midpoint
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, midpoint, step);
    }
    
    
    
    
    //IEnumerator DialogCloseSlow()
    //{
    //    yield return new WaitForSeconds(5f);
    //    DialogWindow.DeActivateDialog();
    //}
}
