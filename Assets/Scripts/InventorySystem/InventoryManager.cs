using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum EquipedItem
{
    first,
    second,
    third,
    fourth,
    fifth,
    none,
}

public class InventoryManager : MonoBehaviour 
{
    private Item[] inventory = new Item[5];
    private EquipedItem equipedItemNumber = EquipedItem.none;
    private Item equipedItem
    {
        get
        {
            return inventory[ (int) equipedItemNumber ];
        }
    }

    private void TryToSwitchSlot()
    {
        if ( Input.GetKeyDown(KeyCode.Alpha1) )
        {
            UI_Inventory.instance.defineSlot(0);
            equipedItemNumber = EquipedItem.first;
        }
        else if ( Input.GetKeyDown(KeyCode.Alpha2) )
        {
            UI_Inventory.instance.defineSlot(1);
            equipedItemNumber = EquipedItem.second;
        }
        else if ( Input.GetKeyDown(KeyCode.Alpha3) )
        {
            UI_Inventory.instance.defineSlot(2);
            equipedItemNumber = EquipedItem.third;
        }
        else if ( Input.GetKeyDown(KeyCode.Alpha4) )
        {
            UI_Inventory.instance.defineSlot(3);
            equipedItemNumber = EquipedItem.fourth;
        }
        else if ( Input.GetKeyDown(KeyCode.Alpha5) )
        {
            UI_Inventory.instance.defineSlot(4);
            equipedItemNumber = EquipedItem.fifth;
        }
    }


    public void TryToAdd(Item item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                Debug.Log("TryToAdd: success");
                AddToInventory(item, i);
                return;
            }
        } 
    }

    public void AddToInventory(Item itemToAdd, int slotIndex)
    {
        // inventory[slotIndex] = item;
        inventory.SetValue(itemToAdd, slotIndex);
        UI_Inventory.instance.ChangeSlotSprite(itemToAdd.sprite, slotIndex);
    }

    public void TryToRemove(Item equipedItem)
    {
        if (equipedItem == null || equipedItemNumber == EquipedItem.none)
        {
            Debug.Log("Can't delete item, because item doesn't exist!");
            return;
        }
        else
        {
            Remove();
        }
    }

    public void Remove()
    {
        Sprite defaultItemSprite = UI_Inventory.instance.defaultItemSlotSprite;
        inventory[ (int) equipedItemNumber ] = null;
        UI_Inventory.instance.ChangeSlotSprite(defaultItemSprite, (int) equipedItemNumber);
    }

    void Update()
    {
        // listen item pressed (in future and raycast and it is)
        if (Input.GetKeyDown(KeyCode.I))
        {
            TryToAdd(Database.instance.ItemDataBase[0]);
        }
        
        // listen alpha key code pressed
        TryToSwitchSlot();

        // listen r key pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            try
            {
                TryToRemove(equipedItem);
            }
            catch (System.Exception)
            {
                Debug.Log("Index of removed item out of range. Maybe your array has no elemets");
                // throw;
            }
        }
    }
}