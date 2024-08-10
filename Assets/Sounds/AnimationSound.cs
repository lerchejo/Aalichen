using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationSound : MonoBehaviour
{
    
    public static AnimationSound instance;
    private Des SoundRef;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private GameObject audioHolder;

    public AudioSource eatingFoodSound;
    public AudioSource eatingNaziSound;
    public AudioSource runningSound;
    public AudioSource drinkingBeerSound;

    private void Start()
    {
        
        audioHolder = new GameObject("AudioHolder");
        DontDestroyOnLoad(audioHolder);
        
        eatingFoodSound.transform.parent = audioHolder.transform;
        eatingNaziSound.transform.parent = audioHolder.transform;
        runningSound.transform.parent = audioHolder.transform;
        drinkingBeerSound.transform.parent = audioHolder.transform;
        
      //  SoundRef = Des.instance;
      //  eatingFoodSound = SoundRef.eatingFoodSound;
      //  eatingNaziSound = SoundRef.eatingNazisSound;
      //  runningSound = SoundRef.WalkingSound;
      //  drinkingBeerSound = SoundRef.DrinkingBeerSound;
    }

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
