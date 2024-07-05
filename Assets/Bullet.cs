using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    protected Vector2 targetPosition;
    protected Vector2 direction;
    
    private AudioSource bulletSound;
    private AudioSource ImpactSound;

    private void Start()
    {
        ImpactSound = GameObject.Find("ImpactSound").GetComponent<AudioSource>();
        bulletSound = GameObject.Find("GunshotSound").GetComponent<AudioSource>();
        
        bulletSound.Play();
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
            Destroy(gameObject);
        }
    }
}