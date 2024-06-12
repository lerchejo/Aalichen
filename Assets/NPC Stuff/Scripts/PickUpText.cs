using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class PickUpText : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI pickUpText;
  //[SerializeField] private float proximityThreshold = 2.0f;
  [SerializeField] private bool isFood = true;
  
  
  public GameObject Parent;
  public Animator animator;
  
  
  public GameManager gameManager;

  private bool isEating = false;
  private NPC npc;

  

  private bool pickUpAllowed;

  private void Start()
  {
    npc = FindObjectOfType<NPC>();
    pickUpText.gameObject.SetActive(false);
  }
  private void Update()
  {
    if (pickUpAllowed && Input.GetKeyDown(KeyCode.F))
    {
      animator.SetBool("isEating", true);
      
      
        if (isFood)
        {
          Invoke(nameof(eatFood), 2f);
        }
        else
        {
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
    animator.SetBool("isEating", false); // Set isEating to false after eating
  }

  private void eatNazi()
  {
    if (npc.enemies == null || Parent == null)
    {
      //Debug.LogError("NPC, NPC enemies list, or Parent is null");
      return;
    }

    GameObject enemyToBeEaten = Parent.gameObject;
    if (enemyToBeEaten == null)
    {
      //Debug.LogError("Enemy to be eaten is null");
      return;
    }
    
    if (this == null)
    {
      Debug.LogError("NPC is null, cannot deal damage");
      return;
    }
    npc.enemies.Remove(enemyToBeEaten);
    Destroy(enemyToBeEaten);
    pickUpText.gameObject.SetActive(false);
    var random = Random.Range(1, 10);
    gameManager.incrementXP(100);
    gameManager.incrementCoins(random);
    animator.SetBool("isEating", false); // Set isEating to false after eating
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
