using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Equip : MonoBehaviour
{
    private Inventory inventory;
    private Hotbar hotbar;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("player").GetComponent<Inventory>();
        hotbar = GameObject.FindGameObjectWithTag("player").GetComponent<Hotbar>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TransferToHotbar() {
        int type = this.GetComponent<spawnitem>().type;
        if (type == 0 && hotbar.hotslots[0].GetComponent<Slot>().occupied == false) {
            Instantiate(this, hotbar.hotslots[0].transform, false);
            transform.parent.GetComponent<Slot>().occupied = false; 
            Destroy(this.gameObject);
            hotbar.hotslots[0].GetComponent<Slot>().occupied = true; 
        }
        if (type == 1 && hotbar.hotslots[1].GetComponent<Slot>().occupied == false) {
            Instantiate(this, hotbar.hotslots[1].transform, false);
            transform.parent.GetComponent<Slot>().occupied = false; 
            Destroy(this.gameObject);
            hotbar.hotslots[1].GetComponent<Slot>().occupied = true; 
        }
        if (type == 2 && hotbar.hotslots[2].GetComponent<Slot>().occupied == false) {
            Instantiate(this, hotbar.hotslots[2].transform, false);
            transform.parent.GetComponent<Slot>().occupied = false; 
            Destroy(this.gameObject);
            hotbar.hotslots[2].GetComponent<Slot>().occupied = true;
        } 
        if (type == 3 && hotbar.hotslots[3].GetComponent<Slot>().occupied == false) {
            Instantiate(this, hotbar.hotslots[3].transform, false);
            transform.parent.GetComponent<Slot>().occupied = false; 
            Destroy(this.gameObject);
            hotbar.hotslots[3].GetComponent<Slot>().occupied = true; 
        }
    }
}
