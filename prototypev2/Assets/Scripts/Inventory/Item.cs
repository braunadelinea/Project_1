using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //---- MEMBER VARIABLES ----//

    public enum ItemType { Potion, Ring, Heart, Coin };
    private ItemType type;
    public enum WorldPosition { World, Inventory }
    private WorldPosition worldPosition;
    private GameObject item;
    private Sprite sprite;

    //---- METHODS ----//

    void Start()
    {
        type = ItemType.Potion;
        item = gameObject;
        sprite = item.GetComponent<SpriteRenderer>().sprite;
        worldPosition = WorldPosition.World;
    }

    //---- PUBLIC METHODS ----//

    public void MoveToInventory()
    {
        worldPosition = WorldPosition.Inventory;
    }

    //---- GETTER METHODS ----//

    new public ItemType GetType()
    {
        return type;
    }

    public GameObject GetGameObject()
    {
        return item;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    //---- SETTER METHODS ----//

    public ItemType SetType(ItemType newType)
    {
        ItemType oldType = type;
        type = newType;
        return oldType;
    }
}
