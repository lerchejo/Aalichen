using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaziSounds : MonoBehaviour
{
    [SerializeField] private AudioSource[] naziSounds;
    private AudioSource currentSound;
    //public List<GameObject> enemies; // List of enemies
    private float timePassed = 0.0f;
    public NPC npc;
    
    private void Start()
    {
        currentSound = naziSounds[0];
    }
    
    private void Update()
    {
        foreach (GameObject enemy in npc.enemies)
        {
            // Calculate the distance to the enemy
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

            // If the enemy is within range
            if (distanceToEnemy < 4.0f && timePassed > 3.0f)
            {
                print("in Range to play NaziSound");
                int randomIndex = UnityEngine.Random.Range(0, naziSounds.Length);
                naziSounds[randomIndex].Play();
                currentSound = naziSounds[randomIndex];
                timePassed = 0.0f;
            }
        }
        // increment timePassed
        timePassed += Time.deltaTime;
    }
    
}
