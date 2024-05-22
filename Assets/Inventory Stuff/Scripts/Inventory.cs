using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]

public class Inventory : ScriptableObject
{
    //Creating a list for the Item Objects
    public List<InventorySlot> Container = new();

    public int Coins = 1000;

    //Adding the item to the list
    public bool AddItem(ItemObject _item, int _amount) 
    {

        for(int i = 0; i < Container.Count; i++) 
        { 
            if(Container[i].item == _item) 
            {
                //in this case there is, so add the amount of this Item in that slot; if it more than the amount that is stackable, continue to search
                if (Container[i].amount < Container[i].item.Stack)
                {
                    Container[i].AddAmount(_amount);
                    return true;
                }
                else { continue; }
            }
            
        }

        Container.Add(new InventorySlot(_item, _amount));
        return true;
        
    }

   
    public void RemoveItem(InventorySlot slot)
    {
        // Find the slot in the container
        InventorySlot foundSlot = Container.Find(itemSlot => itemSlot == slot);
        //Debug.Log(foundSlot);
        // Check if the slot is found
        if (foundSlot != null)
        {
           
            // Decrease the stack count or remove the item from the slot
            if (foundSlot.amount > 1)
            {
                // If the stack count is greater than 1, decrease it
                foundSlot.amount--;
            }
            else
            {
                // If the stack count is 1, remove the item from the slot
                Container.Remove(foundSlot);
            }
        }
        else
        {
            // WHERE DID WE GO WRONG
            Debug.LogError("Slot not found in the inventory container.");
        }
    }


    public void AddCoins(int amount) 
    {
        Coins += amount;    
    }
    public void RemoveCoins(int amount) 
    {
        Coins -= amount;    
    }


}



#region InventorySlot

[System.Serializable]
public class InventorySlot 
{
    public ItemObject item;
    public int amount;
    public InventorySlot(ItemObject _item,int _amount) 
    {
        item = _item;
        amount = _amount;
    }

   public void AddAmount(int value) 
    {

        amount += value;
    
    }
}
#endregion InventorySlot