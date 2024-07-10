using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float rotationSpeed = 5f;
    public int ammo = 20;
    public float ShootRange = 20f;
    public bool isPlayerInside = false; // Add this line

    private Transform player;
    private float fireCountdown = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(Shoot());
    }

    void Update()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (fireCountdown <= 0f && ammo > 0 && distanceToPlayer <= ShootRange && !isPlayerInside) // Add !isPlayerInside condition
            {
                GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Bullet bullet = bulletGO.GetComponent<Bullet>();
                if (bullet != null)
                {
                    bullet.Seek(player.position);
                }
                fireCountdown = 1f / fireRate;
                ammo--;
            }

            fireCountdown -= Time.deltaTime;

            yield return null;
        }
    }
}