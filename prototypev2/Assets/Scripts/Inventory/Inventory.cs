using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
   //---- MEMBER VARIABLES ----//

    private Slot[] backpack;
    private Slot activeRingSlot;
    private Slot activePotionSlot;
    private Slot selected; //NEEDS VISUAL

    private GameObject canvas;
    private GameObject backpackPanel;

    //---- METHODS ----//

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (backpackPanel.activeSelf)
            {
                backpackPanel.SetActive(false);
            }
            else
            {
                backpackPanel.SetActive(true);
            }
        }
    }

    //---- PUBLIC METHODS ----//

    //places an item in it's active slot if possible, otherwise places in next available inventory slot, otherwise returns null and does not place anywhere
    public Item PlaceInInventory(Item item)
    {
        //if item is a ring and active ring slot is not occupied, place in active ring slot
        if(item.GetType() == Item.ItemType.Ring && activeRingSlot.GetItem() == null)
        {
            SetRingSlot(item);
            return item;
        }
        //if item is a potion and active ring slot is not occupied, place in active potion slot
        else if (item.GetType() == Item.ItemType.Potion && activePotionSlot.GetItem() == null)
        {
            SetPotionSlot(item);
            return item;
        }
        else if(item.GetType() == Item.ItemType.Ring || item.GetType() == Item.ItemType.Ring)
        {
            for (int i = 0; i < backpack.Length; i++)
            {
                if (backpack[i].GetItem() == null)
                {
                    backpack[i].SetItem(item);
                    return item;
                }
            }
            //backpack is full, do not place the item anywhere
            return null;
        }
        Debug.Log("ERROR: A non-ring or potion item has been attempted to be placed in the inventory");
        return null;
        
    }

    public Slot dropSelected()
    {
        if(selected.GetItem() == null)
        {
            return null;
        }
        Slot temp = selected;
        //TODO: Place item back into world
        toggleSelect(selected);
        return temp;
    }

    //---- GETTER METHODS ----//

    public Slot GetSelected()
    {
        return selected;
    }

    public Item GetActiveRing()
    {
        return activeRingSlot.GetItem();
    }

    public Item GetActivePotion()
    {
        return activePotionSlot.GetItem();
    }

    //---- SETTER METHODS ----//

    //sets the item passed in to the current active ring, returns what was previously in the ring slot.
    public Item SetRingSlot(Item newRing) 
    {
        if (newRing.GetType() != Item.ItemType.Ring)
        {
            Debug.Log("ERROR: Non-ring object attempted to be placed in ring slot");
            return null;
        }
        Item temp = activeRingSlot.GetItem();
        activeRingSlot.SetItem(newRing);
        return temp;
    }

    //sets the item passed in to the current active potion, returns what was previously in the potion slot.
    public Item SetPotionSlot(Item newPotion) 
    {
        if (newPotion.GetType() != Item.ItemType.Potion)
        {
            Debug.Log("ERROR: Non-potion object attempted to be placed in potion slot");
            return null;
        }
        Item temp = activePotionSlot.GetItem();
        activePotionSlot.SetItem(newPotion);
        return temp;
    }

    //---- HELPER METHODS ----//

    //if slot was not already selected, select given slot and return previously selected slot
    //if slot was already selected, unselect given slot and return the given slot
    private Slot toggleSelect(Slot slot)
    {
        Slot temp = selected;
        if (slot != selected)
        {
            selected = slot;
            return temp;
        }
        selected.SetItem(null);
        return selected;
    }
}
