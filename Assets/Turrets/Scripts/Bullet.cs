using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    protected Vector2 targetPosition;
    protected Vector2 Direction;
    private bool isDestroyed = false; // Add this line

    private AudioSource bulletSound;
    private AudioSource ImpactSound;
    private AudioSource bulletMiss;
    private AudioSource bulletNearMiss; // AudioSource for the near miss sound

    private Transform player; // Transform of the player
    public float nearMissDistance = 1.0f; // Distance threshold for a near miss

    public GameObject WOOSH;
    
    private void Start()
    {
        GameObject impactSoundObject = GameObject.Find("ImpactSound");
        if (impactSoundObject != null)
        {
            ImpactSound = impactSoundObject.GetComponent<AudioSource>();
        }

        GameObject gunshotSoundObject = GameObject.Find("GunshotSound");
        if (gunshotSoundObject != null)
        {
            bulletSound = gunshotSoundObject.GetComponent<AudioSource>();
        }

        GameObject bulletMissSoundObject = GameObject.Find("BulletMissSound");
        if (bulletMissSoundObject != null)
        {
            bulletMiss = bulletMissSoundObject.GetComponent<AudioSource>();
        }

        GameObject bulletNearMissSoundObject = GameObject.Find("BulletNearMissSound");
        if (bulletNearMissSoundObject != null)
        {
            bulletNearMiss = bulletNearMissSoundObject.GetComponent<AudioSource>();
        }

        if (bulletSound != null)
        {
            bulletSound.Play();
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player != null)
        {
            StartCoroutine(CheckForNearMiss());
        }

        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(10f);
        if (!isDestroyed)
        {
            Debug.Log("Destroying bullet");
            Destroy(gameObject);
        }
    }
    
    private GameObject NearmissSoundObject = null;
    IEnumerator CheckForNearMiss()
    {
        while (true)
        {
            // Calculate the distance to the player
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // If the distance to the player is less than the near miss distance, play the near miss sound
            if (distanceToPlayer < nearMissDistance && NearmissSoundObject == null)
            {
                NearmissSoundObject = Instantiate(WOOSH, transform.position, Quaternion.identity);
                    //bulletNearMiss.Play();
                
            }
            
            yield return null;
        }
    }
    public void Seek(Vector2 _targetPosition)
    {
        // Store the target's position
        targetPosition = _targetPosition;

        // Calculate and store the direction to the target's position
        Direction = (targetPosition - (Vector2)transform.position).normalized;

        // Calculate the angle to the target
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

        // Set the bullet's rotation to match the direction to the target
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
    
    void Update()
    {
        if (Direction == Vector2.zero)
        {
            if(NearmissSoundObject != null)
            {
                Destroy(NearmissSoundObject);
            }

            isDestroyed = true; // Add this line
            Destroy(gameObject);
            return;
        }

        // Move the bullet in the stored direction
        transform.Translate(Direction * speed * Time.deltaTime, Space.World);
        
    }

    protected void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.CompareTag("Player"))
        {
            Debug.Log(hitInfo.name);
            isDestroyed = true; // Add this line

            Player gameManager = hitInfo.GetComponent<Player>();
            if (gameManager != null)
            {
                ImpactSound.Play();
                gameManager.decrementHP(damage);
                if(NearmissSoundObject != null)
                {
                    isDestroyed = true; // Add this line

                    Destroy(NearmissSoundObject);
                }
                Destroy(gameObject);
            }
        }
        else
        {
            bulletMiss.Play();
            if(NearmissSoundObject != null)
            {
                Destroy(NearmissSoundObject);
            }
            
            isDestroyed = true; // Add this line
            Destroy(gameObject);
        }
    }
}