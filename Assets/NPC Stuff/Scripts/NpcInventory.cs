using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC Inventory", menuName = "ShopSystem/NPC Inventory")]

public class NpcInventory : Inventory
{

    public void RemoveItemByItemObject(Item item)
    {
        // Find the slot in the container
        InventorySlot foundSlot = Container.Find(itemSlot => itemSlot.item == item);
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

}
