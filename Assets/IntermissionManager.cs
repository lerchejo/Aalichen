using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntermissionManager : MonoBehaviour
{
    #region Instance
    
    public static IntermissionManager instance;

    private void Awake()
    {
            instance = this;
    }
    
    #endregion Instance
    
 
    public TextMeshProUGUI NextLevelText;
    private Image panel;
    
    public float FadeTime = 2f;
    public float TotalWaitTime = 10f;

    private void Start()
    {
        panel = GetComponent<Image>();
    }

    public void IntermissionActivate()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        
        float initialFadeTime = FadeTime;
        while (FadeTime > 0)
        {
            FadeTime -= Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, 1 - (FadeTime / initialFadeTime));
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, alpha);
            NextLevelText.color = new Color(NextLevelText.color.r, NextLevelText.color.g, NextLevelText.color.b, alpha);
            yield return null; 
        }

        yield return new WaitForSeconds(TotalWaitTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}
