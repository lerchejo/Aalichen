using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExperienceBar : MonoBehaviour
{

    public Slider Slider;
    // Start is called before the first frame update
    public void SetMaxXP(int experience)
    {
        Slider.maxValue = experience;
        Slider.value = experience;
    }
    
    public void SetXP(int experience)
    {
        Slider.value = experience;
    }
}

