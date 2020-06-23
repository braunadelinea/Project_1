﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnplayer : MonoBehaviour
{
    public levelgen levelgen;
    public GameObject player;
    public LayerMask tile;
    public bool spawnedplayer; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
                Instantiate(player, rooms[rand].transform.position, Quaternion.identity); //create the player 
                spawnedplayer = true; //no need to spawn the player anymore 
            }
        }
        
    }
}