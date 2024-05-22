using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Inventory MyInventory;

    private GameObject invContents;

    private bool InvActive = false;

    public GameObject ContentPref;
    public GameObject ItemDisplayPref;

    public GameObject TargetCanvasParent;


    private GameObject CreatedContents = null;

    private void Start()
    {
        //YUCK
        invContents = transform.GetChild(0).gameObject;
        invContents.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            if (!InvActive)
            {
                invContents.SetActive(true);
                OpenInv();
                InvActive = true;
            }
            else 
            {
                invContents.SetActive(false);
                CloseInv();
                InvActive=false;
            }
        
        }
    }


    private void OpenInv() 
    {

        CreatedContents = Instantiate(ContentPref, TargetCanvasParent.transform);

        foreach (InventorySlot item in MyInventory.Container)
        {
            GameObject tmp = Instantiate(ItemDisplayPref, CreatedContents.transform);
            tmp.GetComponent<Image>().sprite = item.item.Sprite;
            tmp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.amount.ToString();
        }
    
    }
    private void CloseInv()
    {
       Destroy(CreatedContents);
    }
}
