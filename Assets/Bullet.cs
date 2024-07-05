using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    protected Vector2 targetPosition;
    protected Vector2 direction;

    private AudioSource bulletSound;
    private AudioSource ImpactSound;
    private AudioSource bulletMiss;
    private AudioSource bulletNearMiss; // AudioSource for the near miss sound

    private Transform player; // Transform of the player
    public float nearMissDistance = 1.0f; // Distance threshold for a near miss

    private void Start()
    {
        ImpactSound = GameObject.Find("ImpactSound").GetComponent<AudioSource>();
        bulletSound = GameObject.Find("GunshotSound").GetComponent<AudioSource>();
        bulletMiss = GameObject.Find("BulletMissSound").GetComponent<AudioSource>();
        bulletNearMiss = GameObject.Find("BulletNearMissSound").GetComponent<AudioSource>(); // Get the AudioSource for the near miss sound
        bulletSound.Play();

        player = GameObject.FindGameObjectWithTag("Player").transform; // Get the Transform of the player
        StartCoroutine(CheckForNearMiss());

        
    }

    
    IEnumerator CheckForNearMiss()
    {
        while (true)
        {
            // Calculate the distance to the player
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // If the distance to the player is less than the near miss distance, play the near miss sound
            if (distanceToPlayer < nearMissDistance && !bulletNearMiss.isPlaying    )
            {
                bulletNearMiss.Play();
            }

            // Wait for 0.1 seconds before the next check
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void Seek(Vector2 _targetPosition)
    {
        // Store the target's position
        targetPosition = _targetPosition;

        // Calculate and store the direction to the target's position
        direction = (targetPosition - (Vector2)transform.position).normalized;
    }

    void Update()
    {
        if (direction == Vector2.zero)
        {
            Destroy(gameObject);
            return;
        }

        // Move the bullet in the stored direction
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Calculate the distance to the player
     //  float distanceToPlayer = Vector2.Distance(transform.position, player.position);

     //  // If the distance to the player is less than the near miss distance, play the near miss sound
     //  if (distanceToPlayer < nearMissDistance)
     //  {
     //      bulletNearMiss.Play();
     //  }
    }

    protected void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.CompareTag("Player"))
        {
            Debug.Log(hitInfo.name);
            Player gameManager = hitInfo.GetComponent<Player>();
            if (gameManager != null)
            {
                ImpactSound.Play();
                gameManager.decrementHP(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            bulletMiss.Play();
            Destroy(gameObject);
        }
    }
}