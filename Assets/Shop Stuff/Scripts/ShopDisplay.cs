using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopDisplay : MonoBehaviour
{

    #region Instance
    public static ShopDisplay instance;

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

    public NpcInventory stock;
    public GameObject prefab;
    public GameObject ShopHirObj;
    public Inventory playerInventory;

    public string ShopName;
    public GameObject ShopNamePref;
    public void GenerateShop() 
    {
        GameObject emp = Instantiate(ShopNamePref, ShopHirObj.transform);
        emp.GetComponent<TextMeshProUGUI>().text = ShopName + "'s Shop";
        foreach(InventorySlot itemSlot in stock.Container) 
        {
            GameObject go = Instantiate(prefab, ShopHirObj.transform);
            go.GetComponent<Image>().sprite = itemSlot.item.Sprite;
            go.GetComponent<Button>().onClick.AddListener(() => stock.RemoveItem(itemSlot));
            go.GetComponent<Button>().onClick.AddListener(() => playerInventory.AddItem(itemSlot.item,1));
            go.GetComponent<Button>().onClick.AddListener(() => playerInventory.RemoveCoins(itemSlot.item.Price));
            go.GetComponent<Button>().onClick.AddListener(() => Refresh());

            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = itemSlot.item.Price.ToString();
            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemSlot.amount.ToString();
        }
        ShopHirObj.SetActive(true);
    }

    public void DestroyShop() 
    {
        if (ShopHirObj != null)
        {
            ShopHirObj.GetComponent<DestroyChildren>().Execute();
            ShopHirObj.SetActive(false);
        }
    }

    public void Refresh() 
    {
        DestroyShop();
        GenerateShop();
    }
}
