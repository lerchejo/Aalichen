using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float rotationSpeed = 5f;
    public int ammo = 20; // Add this line
    public float ShootRange = 20f;
    
    private Transform player;
    private float fireCountdown = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(Shoot());
    

    }

    void Update()
    {
        // Calculate direction to the player
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Rotate turret to face the player
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            // Calculate the distance to the player
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Only shoot if the player is within 20 units of the turret
            if (fireCountdown <= 0f && ammo > 0 && distanceToPlayer <= ShootRange)
            { 
                // Instantiate bullet and set its direction
                // Instantiate bullet and set its direction
                GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Bullet bullet = bulletGO.GetComponent<Bullet>();
                if (bullet != null)
                {
                   
                    bullet.Seek(player.position);
                }
                fireCountdown = 1f / fireRate;
                ammo--; // Decrement ammo by 1
            }

            fireCountdown -= Time.deltaTime;

            yield return null;
        }
    }
}