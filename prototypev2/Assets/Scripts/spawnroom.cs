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
        Collider2D roomdetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
        if (roomdetection == null && levelgen.stopgen == true) {
            int rand = Random.Range(0, levelgen.rooms.Length);
            Instantiate(levelgen.rooms[rand], transform.position, Quaternion.identity);
            Destroy(gameObject); 
        }
    }
}
