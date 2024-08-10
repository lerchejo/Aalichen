using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSound : MonoBehaviour
{
    [SerializeField] private AudioSource damageSound;
    
    private Des SoundRef;
    private GameObject audioHolder;

    private void Start()
    {
        audioHolder = new GameObject("AudioHolder");
        DontDestroyOnLoad(audioHolder);
        
        damageSound.transform.parent = audioHolder.transform;
        //SoundRef = Des.instance;
        //damageSound = SoundRef.damageSound;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !damageSound.isPlaying)
        {
            damageSound.Play();
        }
    }
}
