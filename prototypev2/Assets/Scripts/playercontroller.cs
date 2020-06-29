using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    public float moveIncrement;
    public GameObject currentroom; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawnplayer.spawnedplayer == true)
        {
            if (Input.GetKey(KeyCode.A))
            {
                Vector3 pos = this.transform.position;
                pos.x -= moveIncrement;
                this.transform.position = pos;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Vector3 pos = this.transform.position;
                pos.x += moveIncrement;
                this.transform.position = pos;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Vector3 pos = this.transform.position;
                pos.y -= moveIncrement;
                this.transform.position = pos;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                Vector3 pos = this.transform.position;
                pos.y += moveIncrement;
                this.transform.position = pos;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Room")) {
            Debug.Log("Successfully Detected Room Collision");
            currentroom = collision.gameObject; 
        }
    }


}
