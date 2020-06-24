using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnobj : MonoBehaviour
{
    public GameObject[] objects; 
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, objects.Length); //choose a random object in the list 
        GameObject instance = Instantiate(objects[rand], transform.position, Quaternion.identity); //spawn that object 
        instance.transform.parent = transform; //child the newly spawned object to the room it is in. 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
