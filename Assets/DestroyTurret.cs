using System.Collections;
using UnityEngine;

public class DestroyTurret : MonoBehaviour
{
    private bool isPlayerInside = false;
    private Coroutine destroyCoroutine;

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            destroyCoroutine = StartCoroutine(DestroyAfterDelay());
        }
        if (Input.GetKeyUp(KeyCode.E))
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
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}