using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
   //---- MEMBER VARIABLES ----//

    private List<Slot> backpack = new List<Slot>();
    private Slot activeRingSlot;
    private Slot activePotionSlot;
    private Slot selected;

    private GameObject canvas;
    private GameObject backpackPanel;

    //---- METHODS ----//

    void Start()
    {
        backpackPanel = GameObject.Find("Backpack Panel");
        activeRingSlot = GameObject.Find("Active Ring Slot").GetComponent<Slot>();
        activePotionSlot = GameObject.Find("Active Potion Slot").GetComponent<Slot>();
        foreach (Slot slot in backpackPanel.GetComponentsInChildren<Slot>())
        {
            backpack.Add(slot);
        }
        Debug.Log("Backpack size: " + backpack.Count);
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
        Debug.Log("Entered Inventory.PlaceInInventory Script");
        //if item is a ring and active ring slot is not occupied, place in active ring slot
        if(item.GetType() == Item.ItemType.Ring && activeRingSlot.isEmpty())
        {
            Debug.Log("Item is a ring, active ring slot is empty, placing ring in active ring slot");
            SetRingSlot(item);
            item.MoveToInventory();
            return item;
        }
        //if item is a potion and active ring slot is not occupied, place in active potion slot
        else if (item.GetType() == Item.ItemType.Potion && activePotionSlot.isEmpty())
        {
            Debug.Log("Item is a potion, active potion slot is empty, placing potion in active potion slot");
            SetPotionSlot(item);
            item.MoveToInventory();
            return item;
        }
        else if(item.GetType() == Item.ItemType.Ring || item.GetType() == Item.ItemType.Potion)
        {
            Debug.Log("Item is a ring or potion, its active slot is full, attempting to place in backpack");
            foreach (Slot slot in backpack)
            {
                if (slot.isEmpty())
                {
                    Debug.Log("Found empty slot in backpack, placing in backpack");
                    slot.SetItem(item);
                    item.MoveToInventory();
                    return item;
                }
            }
            //backpack is full, do not place the item anywhere
            Debug.Log("Backpack is full, unable to place item anywhere");
            return null;
        }
        Debug.Log("ERROR: A non-ring or potion item has been attempted to be placed in the inventory");
        return null;
        
    }

    //if slot was not already selected, select given slot and return previously selected slot
    //if slot was already selected, unselect given slot and return the given slot
    public void HandleSlotClick(Slot slot)
    {
        if (!backpackPanel.activeSelf)
        {
            return;
        }
        //make sure the passed in slot is not null
        if (slot == null)
        {
            Debug.Log("ERROR: Null slot passed in to HandleSlotClick");
            return;
        }
        if(selected == null)
        {
            Debug.Log("First selection");
            selected = slot;
            selected.gameObject.GetComponent<Image>().color = Color.yellow;
            return;
        }
        //if the previously selected slot is occupied, unselect it and swap items between the slots
        if(selected.GetItem() != null)
        {
            if((slot == activePotionSlot && selected.GetItem().GetType() == Item.ItemType.Ring) || (slot == activeRingSlot && selected.GetItem().GetType() == Item.ItemType.Potion))
            {
                selected.gameObject.GetComponent<Image>().color = Color.white;
                selected = null;
                return;
            }
            SwapItems(selected, slot);
            selected.gameObject.GetComponent<Image>().color = Color.white;
            selected = null;
            return;
        }
        //if the previously selected slot is empty, change selected slot to clicked slot
        else
        {
            selected.gameObject.GetComponent<Image>().color = Color.white;
            //if the same slot was clicked twice, unselect it
            if (selected == slot)
            {
                selected = null;
                return;
            }
            //otherwise select the clicked slot
            selected = slot;
            selected.gameObject.GetComponent<Image>().color = Color.yellow;
            return;
        }
    }

    public void DropSelected() //Unfinished
    {
        if (!backpackPanel.activeSelf)
        {
            return;
        }
        Debug.Log("Entered DropSelected script");
        if (selected == null)
        {
            return;
        }
        if (selected.GetItem() == null)
        {
            selected.gameObject.GetComponent<Image>().color = Color.white;
            selected = null;
            return;
        }
        selected.GetItem().gameObject.SetActive(true);
        Vector2 playerPosition = new Vector2(GameObject.Find("Main Camera").transform.position.x, GameObject.Find("Main Camera").transform.position.y);
        selected.GetItem().gameObject.transform.position = playerPosition;
        selected.GetItem().transform.SetParent(GameObject.Find("Items").transform);
        selected.gameObject.GetComponent<Image>().color = Color.white;
        selected.SetItem(null);
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

    private void SwapItems(Slot slot1, Slot slot2)
    {
        slot1.SetItem(slot2.SetItem(slot1.GetItem()));
    }
}
