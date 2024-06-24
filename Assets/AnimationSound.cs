using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSound : MonoBehaviour
{
    [SerializeField] private AudioSource eatingFoodSound;
    [SerializeField] private AudioSource eatingNaziSound;
    
    public void playEatingFoodSound()
    {
        if (!eatingFoodSound.isPlaying)
        {
            eatingFoodSound.Play();
        }
    }
    
    public void playEatingNaziSound()
    {
        if (!eatingNaziSound.isPlaying)
        {
            eatingNaziSound.Play();
        }
    }
}
