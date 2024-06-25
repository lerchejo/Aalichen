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
  public AnimatorController FoodController;
  
  public GameManager gameManager;

  private bool isEating = false;
  private NPC npc;

  

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
      animator.runtimeAnimatorController = FoodController;
      
      
        if (isFood)
        {
          animator.SetBool("isEatingFood", true);
          Invoke(nameof(eatFood), 2f);
        }
        else
        {
          animator.SetBool("isEatingNazi", true);
          Invoke(nameof(eatNazi), 2f);
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
    animator.SetBool("isEatingFood", false); // Set isEating to false after eating
    isEating = false;
  }

  private void DealDamage()
  {
    Enemy.Health -= gameManager.Damage;
    HealthBar.SetHealth(Enemy.Health);

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
    if (npc.enemies == null || Parent == null)
    {
      //Debug.LogError("NPC, NPC enemies list, or Parent is null");
      return;
    }

   
    
    if (this == null)
    {
      Debug.LogError("NPC is null, cannot deal damage");
      return;
    }
    pickUpText.gameObject.SetActive(false);

    DealDamage();
    
    animator.SetBool("isEatingNazi", false); // Set isEating to false after eating
    isEating = false;
  }
  
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
        pickUpText.gameObject.SetActive(true);
        pickUpAllowed = true;
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
