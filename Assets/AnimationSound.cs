using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSound : MonoBehaviour
{
    [SerializeField] private AudioSource eatingSound;
    
    public void playEatingSound()
    {
        if (!eatingSound.isPlaying)
        {
            eatingSound.Play();
        }
    }
}
