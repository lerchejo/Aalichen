using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playTrapSound : MonoBehaviour
{
    public AudioSource trapSound;
    
    private void playSound()
    {
        trapSound.Play();
    }
}
