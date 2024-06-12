using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogWindow : MonoBehaviour
{
    #region Instance
    public static DialogWindow instance;

    void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }
    #endregion Instance

    private GameObject D_WindowParent;
    private TextMeshProUGUI text;

    public Button yesButton;
    public Button noButton;

    private void Start()
    {
        D_WindowParent = transform.GetChild(0).gameObject;
        text = D_WindowParent.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }


    public void ActivateDialog(string target) 
    {
        D_WindowParent.SetActive(true);
        text.text = target;
    }


    public void ActivateDialogWithButtons(string targetYes,UnityAction yes , string yesText = null, string noText = null)
    {
        D_WindowParent.SetActive(true);
        text.text = targetYes;

        if(yesText != null && yesText != "")
        {
            yesButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = yesText;
        }
        if(noText != null && yesText != "")
        {
            noButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = noText;
        }


        


        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);

        yesButton.onClick.AddListener(()=> yes());

    }

    public void DeActivateDialog() 
    {
        if(D_WindowParent != null)
        {
            D_WindowParent.SetActive(false);
            text.text = "";
        }
        
        if(yesButton != null && noButton != null) 
        {
            yesButton.gameObject.SetActive(false);
            yesButton.onClick.RemoveAllListeners();
            noButton.gameObject.SetActive(false);
        }
        
    }

}
