using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSound : MonoBehaviour
{
    [SerializeField] private AudioSource eatingSound;
    private bool hasPlayed = false;

    public void startEating()
    {
        hasPlayed = false;
    }
    
    public void playEatingSound()
    {
        if (!hasPlayed)
        {
            eatingSound.Play();
            hasPlayed = true;
        }
    }
}
