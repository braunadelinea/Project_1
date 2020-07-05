using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //---- GETTER METHODS ----//

    public Item GetItem()
    {
        return item;
    }

    public void SetItem(Item newItem)
    {
        if (item != null)
        {
            Debug.Log("ERROR: Item attempted to be placed in occupied slot");
        }
    }
}
