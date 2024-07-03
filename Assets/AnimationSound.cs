using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSound : MonoBehaviour
{
    public AudioSource eatingFoodSound;
    public AudioSource eatingNaziSound;
    public AudioSource runningSound;
    public AudioSource drinkingBeerSound;
    
    public void playEatingFoodSound()
    {
        if (!eatingFoodSound.isPlaying)
        {
            eatingFoodSound.Play();
        }
    }
    
    public void playDrinkingBeerSound()
    {
        if (!drinkingBeerSound.isPlaying)
        {
            drinkingBeerSound.Play();
        }
    }
    
    
    public void playEatingNaziSound()
    {
        if (!eatingNaziSound.isPlaying)
        {
            eatingNaziSound.Play();
        }
    }
    
    public void playRunningSound()
    {
        if (!runningSound.isPlaying)
        {
            runningSound.Play();
        }
    }
}
