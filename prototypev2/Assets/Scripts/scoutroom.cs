using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoutroom : MonoBehaviour
{
    //attached to the room prefabs, helps determine what type of room they are, which is used to fix block move down by 2 bug, and will be helpful in the future. 
    public int type;

    public void RoomDestruction() { //a function that destroys the room
        Destroy(gameObject); 
    }
}
