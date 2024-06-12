using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSound : MonoBehaviour
{
    [SerializeField] private AudioSource damageSound;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !damageSound.isPlaying)
        {
            damageSound.Play();
        }
    }
}
