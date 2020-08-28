using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    //---- MEMBER VARIABLES ----//

    private Item item;
    private GameObject slot;
    private Sprite sprite;

    //---- METHODS ----//

    void Start()
    {
        slot = gameObject;
        sprite = gameObject.GetComponent<Image>().sprite;
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
        if (newItem == null)
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = null;
            gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
            return null;
        }
        item.transform.SetParent(slot.gameObject.transform);
        item.transform.localPosition = new Vector3(0,0);
        gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item.GetSprite();
        gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
        return temp;
    }

    //---- SETTER METHODS ----//

    public void SetGameObject(GameObject gameObject)
    {
        slot = gameObject;
    }
}
