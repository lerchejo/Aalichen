using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum NPCtype 
{
    none,
    shop

}

public class NPC : MonoBehaviour
{

    public NPCtype type;
    public NpcInventory NPCinventory;
    public DialogObject DialogObject;
    public string PersonName;

    private DialogWindow DialogWindow;


    private ShopDisplay Shopdisplay;
    private void Start()
    {
        DialogWindow = DialogWindow.instance;
        Shopdisplay = ShopDisplay.instance;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            if (type != NPCtype.shop)
            {
                DialogWindow.ActivateDialog(DialogObject.EntryText);
            }
            else 
            {
                Shopdisplay.stock = NPCinventory;
                Shopdisplay.ShopName = PersonName;
                DialogWindow.ActivateDialogWithButtons(DialogObject.EntryText, Shopdisplay.GenerateShop , DialogObject.yesText,DialogObject.noText);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
      {
        if (other.CompareTag("Player")) 
        {
            DialogWindow.DeActivateDialog();
            Shopdisplay.DestroyShop();
            
            DialogWindow.ActivateDialog(DialogObject.ExitText);

            StartCoroutine(DialogCloseSlow());

        }
    }
    
    IEnumerator DialogCloseSlow()
    {
        yield return new WaitForSeconds(5f);
        DialogWindow.DeActivateDialog();
    }
}
