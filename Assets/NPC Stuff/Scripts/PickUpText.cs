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
  
  public GameManager gameManager;

  private bool isEating = false;
  private NPC npc;

  public AnimationSound animationSound;
  

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
      HealthBar.SetMaxHealth(Enemy.Health);
    }
    
    
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
    
    if (gameManager.HP <= 900)
    {
      gameManager.incrementHP(100);
    }
    else if (gameManager.HP > 900 && gameManager.HP % 100 != 0)
    {
      gameManager.incrementHP(100 - gameManager.HP % 100);
    }
    
    //animator.SetBool("isEatingFood", false); // Set isEating to false after eating
    isEating = false;
  }

  private void DealDamage()
  {
    Enemy.Health -= gameManager.Damage;
    HealthBar.SetHealth(Enemy.Health);

    Invoke(nameof(killNazi), 1.5f);
  }

  public void killNazi()
  {
    
    GameObject enemyToBeEaten = Parent.gameObject;
    var random = Random.Range(1, 10);
    
    if (Enemy.Health <= 0f)
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
