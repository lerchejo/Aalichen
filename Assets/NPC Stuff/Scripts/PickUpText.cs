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
  //[SerializeField] private TextMeshProUGUI pickUpText;
  //[SerializeField] private float proximityThreshold = 2.0f;
  [SerializeField] private bool isFood = true;
  private bool isBeer;
  
  public Enemy Enemy = null;
  private HealthBar HealthBar = null;
  public GameObject Parent;
  public Animator animator;
  public Player player;
  
  private GameManager gameManager;

  private bool isEating = false;
  private NPC npc;

  
  public AnimationSound animationSound;
  [SerializeField] private AudioSource[] naziSounds;
  private AudioSource currentSound;
  
  private bool pickUpAllowed;

  private void Start()
  {
    UIManager.Instance.pressE.gameObject.SetActive(false);
    gameManager = GameManager.Instance;
    if (isFood)
    {
      Enemy = null;
      HealthBar = null;

      // Determine if the object is beer
      if (gameObject.name.Contains("Bier"))
      {
        isBeer = true;
      }
    }
    else
    {
      HealthBar = Parent.GetComponentInChildren<HealthBar>();
      HealthBar.SetMaxHealth(Enemy.health);
    }
    
    // Initialize currentSound to be able to check if it is playing
    //currentSound = naziSounds[0];
  }
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.F) && isEating == false)
    {
      animator.SetBool("Attack", true);
      
      isEating = true;
        if (isFood && pickUpAllowed)
        {
          //animator.SetBool("isEatingFood", true);
          //Play the right sound
          if (isBeer)
          {
            animationSound.drinkingBeerSound.Play();
          }
          else
          {
            animationSound.eatingFoodSound.Play();
          }
          
          eatFood();
          
        }
        else if (!isFood && pickUpAllowed)
        {
          eatNazi();
        }

        StartCoroutine(StopAttack());
      
    }
  }
  
  IEnumerator StopAttack()
  {
    yield return new WaitForSeconds(0.5f);      
    isEating = false;
    animator.SetBool("Attack", false);
  }

  private void eatFood()
  {
    //print(HealthBar);
    animationSound.eatingFoodSound.Play();
    UIManager.Instance.pressE.gameObject.SetActive(false);
    Destroy(Parent.gameObject);
    
    if (player.HP <= 850)
    {
      player.incrementHP(150);
    }
    else if (player.HP > 900 && player.HP % 150 != 0)
    {
      player.incrementHP(150 - player.HP % 150);
    }
    
    //animator.SetBool("isEatingFood", false); // Set isEating to false after eating
    isEating = false;
  }

  private void DealDamage()
  {
    // Play random Nazi Sound if it is the first time the enemy is hit
    if (Enemy.health >= 50)
    {
      int randomIndex = Random.Range(0, naziSounds.Length);
      naziSounds[randomIndex].Play();
      currentSound = naziSounds[randomIndex];
    }
    
    Enemy.health -= gameManager.Damage;
    HealthBar.SetHealth(Enemy.health);
    animator.SetBool("Attack", false);
    
    
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
      gameManager.enemies.Remove(enemyToBeEaten);
      //npc.enemies.Remove(enemyToBeEaten);
      
      Destroy(enemyToBeEaten);
    }
  }

  private void eatNazi()
  {

    animationSound.eatingNaziSound.Play();
    UIManager.Instance.pressE.gameObject.SetActive(false);
    isEating = false;
    DealDamage();
    
  }
  
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      UIManager.Instance.pressE.gameObject.SetActive(true);
        pickUpAllowed = true;
        print("Player entered");
    }
  }
  
  private void OnTriggerExit2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      UIManager.Instance.pressE.gameObject.SetActive(false);
      pickUpAllowed = false;
    }
  }
}
