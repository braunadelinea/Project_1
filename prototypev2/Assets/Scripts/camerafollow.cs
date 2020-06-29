using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    public GameObject player; 

    void Update()
    {
        Vector3 pos = player.GetComponent<playercontroller>().currentroom.transform.position;
        pos.z -= 10;
        this.transform.position = pos; 
    }

}
