using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    public GameObject player; 
    private bool followPlayer = false;

    void Update()
    {
        //don't mess with the camera until the current room has been set
        if (player.GetComponent<PlayerManager>().currentroom == null)
        {
            return;
        }
        Vector3 pos;
        //when the player hits "p", zoom out, speed up player, make camera follow player to maek debugging easier
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!followPlayer)
            {
                followPlayer = true;
                player.GetComponent<PlayerManager>().SetSpeed(0.2f);
                gameObject.GetComponent<Camera>().orthographicSize = 20;
            }
            else
            {
                followPlayer = false;
                player.GetComponent<PlayerManager>().SetSpeed(0.1f);
                gameObject.GetComponent<Camera>().orthographicSize = 5;
            }
        }
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
