using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    public int i;
    public bool occupied; 

    private void Start() {
        inventory = GameObject.FindGameObjectWithTag("player").GetComponent<Inventory>(); 
    }
    private void Update()
    {
        if (transform.childCount <= 0) {
            occupied = false; 
        }
    }
    public void DropItem() {
        foreach (Transform child in transform) {
            child.GetComponent<spawnitem>().SpawnDroppedItem();
            GameObject.Destroy(child.gameObject); 
        }
    }
}
