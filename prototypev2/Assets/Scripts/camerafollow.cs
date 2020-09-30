using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    public GameObject player; 

    void Update()
    {
        //don't mess with the camera until the current room has been set
        if (player.GetComponent<PlayerManager>().currentroom == null)
        {
            return;
        }
        Vector3 pos;
        if (followPlayer)
        {
            pos = player.transform.position;
        }
        else
        {
            pos = player.GetComponent<PlayerManager>().currentroom.transform.position;
        }
        pos.z -= 10;
        this.transform.position = pos; 
    }

}
