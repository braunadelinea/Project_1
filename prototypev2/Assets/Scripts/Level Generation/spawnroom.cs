using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnroom : MonoBehaviour
{
    public LayerMask whatIsRoom;
    public levelgen levelgen; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D roomdetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom); //checks to see if there is already a room in a given place 
        if (roomdetection == null && levelgen.stopgen == true) { //if roomdetection did not find a room in a given spot, and the main path is done generating 
            int rand = Random.Range(0, levelgen.rooms.Length); //get a random room type 
            Instantiate(levelgen.rooms[rand], transform.position, Quaternion.identity); //spawn a random room type
            Destroy(gameObject); //destroy the pose that was there beforehand 
        }
    }
}
