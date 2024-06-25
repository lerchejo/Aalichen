using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSound : MonoBehaviour
{
    public AudioSource eatingFoodSound;
    public AudioSource eatingNaziSound;
    
    public void playEatingFoodSound()
    {
       // if (!eatingFoodSound.isPlaying)
       // {
            eatingFoodSound.Play();
            StartCoroutine(playSoundRepetedly());
            eatingFoodSound.Stop();
       // }
    }
    
    IEnumerator playSoundRepetedly()
    {
        yield return new WaitForSeconds(2);
    }
    
    public void playEatingNaziSound()
    {
        if (!eatingNaziSound.isPlaying)
        {
            eatingNaziSound.Play();
        }
    }
}
