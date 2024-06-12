using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class PickUpText : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI pickUpText;
  [SerializeField] private float proximityThreshold = 2.0f;
  [SerializeField] private bool isFood = true;
  [SerializeField] private AudioSource[] eatingNaziSounds;
  [SerializeField] private AudioSource burpSound;

  
  
  public GameObject Parent;
  public Animator animator;
  public GameManager gameManager;
  public AnimationSound animationSound;
  private bool isEating = false;

  

  private bool pickUpAllowed;

  private void Start()
  {
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
    Destroy(Parent.gameObject);
    pickUpText.gameObject.SetActive(false);
    var random = Random.Range(1, 10);
    gameManager.incrementXP(100);
    gameManager.incrementCoins(30);
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
