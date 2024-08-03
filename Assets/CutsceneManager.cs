using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public List<CutSceneDialogObjectCollection> Dialogs = new();
    public TextMeshProUGUI ContentHolder;
    public RawImage ImageHolder;

    private int currentDialog = 0;
    
    private int currentDialogPage = -1;
    
    public int ChangeSceneIndex = 0;

    private void Start()
    {
        NextDialogPage();
    }

    public void NextDialog()
    {
        currentDialog++;
        currentDialogPage = -1;

        if (Dialogs.Count <= currentDialog)
        {
            ChangeScene();
        }
        else
        {
            NextDialogPage();
        }
       

    }

    public void NextDialogPage()
    {
        currentDialogPage++;
        
        string tmp = Dialogs[currentDialog].GetDialog(currentDialogPage);
        Sprite tmpImage = Dialogs[currentDialog].GetImage(currentDialogPage);
        
        if (tmp != null)
        {
            if (tmpImage != null)
            {
                ImageHolder.texture = tmpImage.texture;
            }
            ContentHolder.text = tmp;
        }
        else
        {
            NextDialog();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextDialogPage();
        }
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeScene();
        }
    
    }
    
    private void ChangeScene()
    {
        SceneManager.LoadScene(ChangeSceneIndex);
    }
}
/*


public TextMeshProUGUI ContentHolder;
    public RawImage ImageHolder;
    public Texture[] images;
    public String[] pages;



    private int currentPage = 0;
    private int currentImage = 0;

    // Start is called before the first frame update
    void Start()
    {
        ImageHolder.texture = images[currentImage];
        ContentHolder.text = pages[currentPage];
        currentPage++;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            skipPage();
        }
    }

    private void skipPage()
    {
            if (currentPage < pages.Length)
            {
                print("Skipping page");
                ContentHolder.text = pages[currentPage];
                currentPage++;

            }
    }
}

*/