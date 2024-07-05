using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBullet : Bullet
{
    public GameObject impactCirclePrefab; // This is your circle prefab
    public GameObject hitEffectPrefab; // This is your hit effect prefab
    private GameObject circleInstance; // This will hold the instance of the circle
    private GameObject effectInstance; // This will hold all instances of the Effect
    public float damageRadius = 5.0f; // The radius of the damage area
    private bool hasReachedTarget = false; // This flag will be set to true when the bullet reaches its target position for the first time
    private AudioSource explosionSound; // The sound that will be played when the bullet hits something
    private AudioSource CannonShotSound; // The sound that will be played when the bullet hits something
    
    private void Start()
    {
        // Instantiate the circle prefab at the target position (where the player was at the time of shooting)
        circleInstance = Instantiate(impactCirclePrefab, targetPosition, Quaternion.identity);
        effectInstance = Instantiate(hitEffectPrefab, targetPosition, Quaternion.identity);
        effectInstance.SetActive(false);
        explosionSound = GameObject.Find("Explosion").GetComponent<AudioSource>(); // Set the volume to the maximum
        CannonShotSound = GameObject.Find("CannonShot").GetComponent<AudioSource>(); // Set the volume to the maximum
        CannonShotSound.Play();



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

        // Check if the bullet has reached its target position
        if (Vector2.Distance(transform.position, targetPosition) <= 0.1f)
        {
            effectInstance.SetActive(true);
            if (!hasReachedTarget)
            {
                Debug.Log("Starting DestroyCircle coroutine"); // Log a message when the coroutine is started
                explosionSound.Play(); // Play the explosion sound at the bullet's position
                GetComponent<Renderer>().enabled = false; // Make the bullet invisible
                StartCoroutine(DestroyCircle());
                hasReachedTarget = true;

                // Call OnTriggerStay2D when the bullet reaches its target position
                OnTriggerStay2D(null);
            }
        }
    }

    IEnumerator DestroyCircle()
    {
        Debug.Log("Destroying gameObject"); // Log a message when the gameObject is destroyed

        yield return new WaitForSeconds(1f);

        Debug.Log("Destroying circleInstance and effectInstance"); // Log a message when the circleInstance and effectInstance are destroyed
        Destroy(circleInstance);
        Destroy(effectInstance);

        Destroy(gameObject); // Destroy the gameObject after a delay
    }

    void OnTriggerStay2D(Collider2D hitInfo)
    {
        // Instantiate the hit effect at the bullet's current position
        Debug.Log("Bullet has hit something"); // Log a message when the bullet hits something
        // Get all colliders within the damage radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(targetPosition, damageRadius);

        // Deal damage to all enemies within the damage radius
        foreach (Collider2D collider in colliders)
        {
            Player player = collider.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("Dealing damage to player"); // Log a message when dealing damage to the player
                player.decrementHP(damage);
            }
        }
    }
}