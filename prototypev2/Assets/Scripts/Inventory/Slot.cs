using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    //---- MEMBER VARIABLES ----//

    private Item item;
    private GameObject slot;

    //---- METHODS ----//

    void Start()
    {
        slot = gameObject;
    }

    public bool isEmpty()
    {
        return item == null;
    }

    public void Click()
    {
        GameObject.FindObjectOfType<Inventory>().HandleSlotClick(this);
    }

    //---- GETTER METHODS ----//

    public Item GetItem()
    {
        return item;
    }

    public Item SetItem(Item newItem)
    {
        Item temp = item;
        item = newItem;
        if(item == null)
        {
            slot.gameObject.GetComponent<Image>().sprite = null;
            return null;
        }
        item.transform.SetParent(slot.gameObject.transform);
        item.transform.localPosition = new Vector3(0,0);
        item.gameObject.SetActive(false);
        slot.gameObject.GetComponent<Image>().sprite = item.GetSprite();
        return temp;
    }

    //---- SETTER METHODS ----//

    public void SetGameObject(GameObject gameObject)
    {
        slot = gameObject;
    }
}
