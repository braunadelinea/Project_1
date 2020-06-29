using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnitem : MonoBehaviour
{
    public int type; //this signifies the weapon type, 0 is sword, 1 is range and 2 is ring, 3 is misc 
    public GameObject item;
    private Transform player; 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player").transform; 
    }

    // Update is called once per frame
    public void SpawnDroppedItem() {
        Vector2 playerPos = new Vector2(player.position.x, player.position.y + 2);
        Instantiate(item, playerPos, Quaternion.identity); 
    }
}
