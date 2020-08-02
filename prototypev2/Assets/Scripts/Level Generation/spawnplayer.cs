using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnplayer : MonoBehaviour
{
    private GameObject boss; 
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
        boss = GameObject.FindGameObjectWithTag("boss");
        player = GameObject.FindGameObjectWithTag("player"); 
    }
    // Update is called once per frame
    private void Update()
    {
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room"); 
        if (GetSpawnedPlayer() == false && rooms.Length == 16) 
        {
            int rand = Random.Range(0, rooms.Length); //pick a room, any room 
            Collider2D[] checkspawn = Physics2D.OverlapCircleAll(rooms[rand].transform.position, 1); //throw a "scout" into where the player would spawn. 
            for (int i = 0; i < checkspawn.Length; i++)
            {
                if (checkspawn[i].gameObject.CompareTag("Tile")) //if no collision is detected
                {
                    Debug.Log("found collision");
                    return; 
                }
            }
            player.transform.position = rooms[rand].transform.position; //create the player 
            SetSpawnedPlayer(true); //no need to spawn the player anymore 
        }
        if (GetSpawnedPlayer() && spawnedboss == false) {
            int rand = Random.Range(0, rooms.Length); // pick a random room 
            if (Vector2.Distance(player.transform.position, rooms[rand].transform.position) > 20) {
                boss.transform.position = rooms[rand].transform.position;
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
