using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnplayer : MonoBehaviour
{
    public levelgen levelgen;
    public GameObject player;
    public LayerMask tile;
    public static bool spawnedplayer;
    
    //NOTE: if changes propsed in levelgen.cs are made, this script will become obselete. 

    // Update is called once per frame
    void Update()
    {
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room"); //get all rooms currently in the scene 
        if (spawnedplayer == false && rooms.Length == 16) //once all rooms are generated, if we have not spawned the player, let's spawn the player 
        {
            int rand = Random.Range(0, rooms.Length); //pick a room, any room 
            Collider2D checkspawn = Physics2D.OverlapCircle(rooms[rand].transform.position, 1, tile); //throw a "scout" into where the player would spawn. 
            if (checkspawn == null) //if no collision is detected
            {
                player.transform.position = rooms[rand].transform.position; //create the player 
                spawnedplayer = true; //no need to spawn the player anymore 
            }
        }
        
    }
}
