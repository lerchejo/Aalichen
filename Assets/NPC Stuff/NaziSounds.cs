using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaziSounds : MonoBehaviour
{
    [SerializeField] private AudioSource[] naziSounds;
    private AudioSource currentSound;

    private void Start()
    {
        currentSound = naziSounds[0];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !currentSound.isPlaying)
        {
            print("in Range to play NaziSound");
            int randomIndex = UnityEngine.Random.Range(0, naziSounds.Length);
            naziSounds[randomIndex].Play();
            currentSound = naziSounds[randomIndex];
        }
    }
}
