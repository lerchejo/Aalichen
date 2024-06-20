using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class ExperienceBar : MonoBehaviour
{
    
    [FormerlySerializedAs("Slider")] public Slider slider;
    public GameManager gameManager;
    // Start is called before the first frame update
    public void UpdateXP(int experience)
    {
        int nextLevel = Math.Min(gameManager.level + 1, gameManager.levelThresholds.Length -1);
        slider.maxValue = gameManager.levelThresholds[nextLevel];
        slider.value = experience;
    }
    
    public void SetXP(int experience)
    {
        slider.value = experience;
    }  public void SetMaxXP(int experience)
    {
        slider.maxValue = experience;
    }
}

