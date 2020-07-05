using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //---- MEMBER VARIABLES ----//

    public enum ItemType { Potion, Ring, Heart, Coin };
    private ItemType type = ItemType.Potion;
    private GameObject item;

    //---- METHODS ----//

    void Start()
    {
        item = gameObject;
    }

    //---- GETTER METHODS ----//

    new public ItemType GetType()
    {
        return type;
    }
}
