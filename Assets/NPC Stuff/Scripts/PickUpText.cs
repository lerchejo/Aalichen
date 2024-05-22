using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class PickUpText : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI pickUpText;
  [SerializeField] private float proximityThreshold = 2.0f;
  [SerializeField] private bool isFood = true;

  public Animator animator;

  public GameManager gameManager;

  private bool isEating = false;

  

  private bool pickUpAllowed;

  private void start()
  {
    pickUpText.gameObject.SetActive(false);
  }

  private void Update()
  {

 

    if (pickUpAllowed && Input.GetKeyDown(KeyCode.F))
    {
        animator.SetBool("isEating", true);
        print(isEating);
        if (isFood)
        {
          Invoke("eatFood", 2f);
        }
        else
        {
           Invoke("eatNazi", 2f);
        }
        isEating = false;
    }

    float distanceToPlayer = Vector2.Distance(transform.position, GameObject.Find("Player").transform.position);
    print(distanceToPlayer);
    if (distanceToPlayer < proximityThreshold)
    {
        pickUpText.gameObject.SetActive(true);
        print(pickUpText.gameObject.activeSelf);
        pickUpAllowed = true;

    }
    else 
    {
        pickUpText.gameObject.SetActive(false);
        pickUpAllowed = false;
    }
    
  }


  private void OnTriggerExit2D(Collider2D collision)
  {
    if (collision.gameObject.name.Equals("Player"))
    {
      pickUpText.gameObject.SetActive(false);
      pickUpAllowed = false;
    }
  }

  private void eatFood()
  {
    Destroy(pickUpText.gameObject);
    Destroy(gameObject);
    gameManager.incrementHP(10);
    animator.SetBool("isEating", isEating);
  }

  private void eatNazi()
  {
    Destroy(pickUpText.gameObject);
    Destroy(gameObject);
    gameManager.incrementXP(10);
    animator.SetBool("isEating", isEating);
  }
}
