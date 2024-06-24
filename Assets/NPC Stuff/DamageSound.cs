using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSound : MonoBehaviour
{
    [SerializeField] private AudioSource damageSound;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !damageSound.isPlaying)
        {
            damageSound.Play();
        }
    }
}
