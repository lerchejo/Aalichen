using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Des : MonoBehaviour
{
    public static Des instance;

    public AudioSource eatingFoodSound;
    public AudioSource eatingNazisSound;
    public AudioSource WalkingSound;
    public AudioSource DrinkingBeerSound;
    public AudioSource damageSound;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
