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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, 1); 
    }
    // Update is called once per frame
    private void Update() 
    {
        Collider2D other = Physics2D.OverlapCircle(this.transform.position, 1); 
        if (other != null && other.CompareTag("player") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Collision and Key Detected"); 
            for (int i = 0; i < this.inventory.slots.Length; i++)
            {
                if (this.inventory.slots[i].GetComponent<Slot>().occupied == false)
                {
                    this.inventory.slots[i].GetComponent<Slot>().occupied = true;
                    Instantiate(itembutton, this.inventory.slots[i].transform, false);
                    Destroy(gameObject); 
                    break;
                }
            }
        }
    }
}
