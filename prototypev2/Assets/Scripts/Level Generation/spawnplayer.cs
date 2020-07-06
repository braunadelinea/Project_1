using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnplayer : MonoBehaviour
{
    private GameObject bossroom; 
    private GameObject player;
    private bool spawnedplayer;
    private bool spawnedboss;
    /* spawnplayer.cs 
     * Written by: Toby Klauder 
     * Last Edited 7/5/2020
     * Spawns the player after the rooms have all spawned
     * Change-Log: 
     * Added Getters and Setters, overall simplifications
     */

    //PRIVATE METHODS
    private void Start()
    {
        bossroom = GameObject.FindGameObjectWithTag("boss");
        player = GameObject.FindGameObjectWithTag("player"); 
    }
    // Update is called once per frame
    private void Update()
    {
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room"); 
        if (GetSpawnedPlayer() == false && rooms.Length == 16) 
        {
            int rand = Random.Range(0, rooms.Length); //pick a room, any room 
            Collider2D checkspawn = Physics2D.OverlapCircle(rooms[rand].transform.position, 1, 9); //throw a "scout" into where the player would spawn. 
            if (checkspawn == null) //if no collision is detected
            {
                player.transform.position = rooms[rand].transform.position; //create the player 
                SetSpawnedPlayer(true); //no need to spawn the player anymore 
            }
        }
        if (GetSpawnedPlayer() && spawnedboss == false) {
            int rand = Random.Range(0, rooms.Length); // pick a random room 
            if (Vector2.Distance(player.transform.position, rooms[rand].transform.position) > 20) {
                Destroy(rooms[rand]);
                bossroom.transform.position = rooms[rand].transform.position;
                spawnedboss = true;
            }
       
        }
        
    }

    //GETTER METHODS

    public bool GetSpawnedPlayer() {
        return spawnedplayer; 
    }

    //SETTER METHODS

    public void SetSpawnedPlayer(bool newspawnedplayer) {
        spawnedplayer = newspawnedplayer; 
    }

}
