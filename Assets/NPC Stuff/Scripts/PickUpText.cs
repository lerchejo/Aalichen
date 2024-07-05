using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Animations;
using Random = UnityEngine.Random;

public class PickUpText : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI pickUpText;
  //[SerializeField] private float proximityThreshold = 2.0f;
  [SerializeField] private bool isFood = true;
  
  public Enemy Enemy = null;
  public HealthBar HealthBar = null;
  public GameObject Parent;
  public Animator animator;
  public Player player;
  
  public GameManager gameManager;

  private bool isEating = false;
  private NPC npc;

  public AnimationSound animationSound;
  [SerializeField] private AudioSource[] naziSounds;
  private AudioSource currentSound;
  
  private bool pickUpAllowed;

  private void Start()
  {
    npc = FindObjectOfType<NPC>();
    pickUpText.gameObject.SetActive(false);
    
    if (isFood)
    {
      Enemy = null;
      HealthBar = null;
    }
    else
    {
      HealthBar.SetMaxHealth(Enemy.health);
    }
    
    // Initialize currentSound to be able to check if it is playing
    currentSound = naziSounds[0];
    
    
  }
  private void Update()
  {
    if (pickUpAllowed && Input.GetKeyDown(KeyCode.F) && isEating == false)
    {
      isEating = true;
        if (isFood)
        {
          //animator.SetBool("isEatingFood", true);
          animationSound.eatingFoodSound.Play();
          eatFood();
          
        }
        else
        {
          animationSound.eatingNaziSound.Play();
          
          eatNazi();
        }
      
    }
  }

  private void eatFood()
  {
    pickUpText.gameObject.SetActive(false);
    Destroy(Parent.gameObject);
    
    if (player.HP <= 900)
    {
      player.incrementHP(100);
    }
    else if (player.HP > 900 && player.HP % 100 != 0)
    {
      player.incrementHP(100 - player.HP % 100);
    }
    
    //animator.SetBool("isEatingFood", false); // Set isEating to false after eating
    isEating = false;
  }

  private void DealDamage()
  {
    // Play random Nazi Sound if it is the first time the enemy is hit
    if (Enemy.health >= 50)
    {
      int randomIndex = UnityEngine.Random.Range(0, naziSounds.Length);
      naziSounds[randomIndex].Play();
      currentSound = naziSounds[randomIndex];
    }
    
    Enemy.health -= gameManager.Damage;
    HealthBar.SetHealth(Enemy.health);

    Invoke(nameof(killNazi), 1.5f);
  }

  public void killNazi()
  {
    
    GameObject enemyToBeEaten = Parent.gameObject;
    var random = Random.Range(1, 10);
    
    if (Enemy.health <= 0f)
    {
      gameManager.incrementXP(Random.Range(5, 20));
      gameManager.incrementCoins(random);
      npc.enemies.Remove(enemyToBeEaten);
      Destroy(enemyToBeEaten);
    }
  }

  private void eatNazi()
  {

          
    //animator.SetBool("isEatingNazi", true);
    

    pickUpText.gameObject.SetActive(false);
    isEating = false;
    DealDamage();
    //animator.SetBool("isEatingNazi", false); // Set isEating to false after eating
    
  }
  
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
        pickUpText.gameObject.SetActive(true);
        pickUpAllowed = true;
        print("Player entered");;
    }
  }
  
  private void OnTriggerExit2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      pickUpText.gameObject.SetActive(false);
      pickUpAllowed = false;
    }
  }
}
