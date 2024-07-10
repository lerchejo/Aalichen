using System.Collections;
using UnityEngine;

public class DestroyTurret : MonoBehaviour
{
    private bool isPlayerInside = false;
    private Coroutine destroyCoroutine;
    public Turret turret;
    public float HP = 30f;
    public float HPMax = 30f;
    public HealthBar healthBar;
    public float cooldownTime = 1f; // Cooldown duration in seconds
    private float nextActionTime = 0f; // When the next action can be performed

    private void Start()
    {
        healthBar.SetMaxHealth(HPMax);
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKey(KeyCode.F) && Time.time >= nextActionTime)
        {
            nextActionTime = Time.time + cooldownTime; // Set when the next action can be performed
            if (destroyCoroutine == null)
            {
                destroyCoroutine = StartCoroutine(DecreaseHealth());
            }
        }
        else
        {
            if (destroyCoroutine != null)
            {
                StopCoroutine(destroyCoroutine);
                destroyCoroutine = null;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            turret.isPlayerInside = true; // Add this line
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            if (destroyCoroutine != null)
            {
                StopCoroutine(destroyCoroutine);
                destroyCoroutine = null;
            }
            turret.isPlayerInside = false; // Add this line
        }
    }

    private IEnumerator DecreaseHealth()
    {
        while (true)
        {
            HP -= 10f;
            healthBar.SetHealth(HP);
            if (HP <= 0)
            {
                Destroy(gameObject);
                yield break;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}