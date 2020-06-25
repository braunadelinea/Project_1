using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itembutton;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            for (int i = 0; i < this.inventory.slots.Length; i++)
            {
                if (this.inventory.isfull[i] == false)
                {
                    //add item to inventory 
                    this.inventory.isfull[i] = true;
                    Instantiate(itembutton, this.inventory.slots[i].transform, false);
                    Destroy(gameObject); 
                    break;
                }
            }
        }
    }
}
