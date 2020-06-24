using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    public float moveIncrement; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 pos = this.transform.position;
            pos.x -= moveIncrement;
            this.transform.position = pos;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Vector3 pos = this.transform.position;
            pos.x += moveIncrement;
            this.transform.position = pos;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Vector3 pos = this.transform.position;
            pos.y -= moveIncrement;
            this.transform.position = pos;
        }
        else if (Input.GetKey(KeyCode.W)) {
            Vector3 pos = this.transform.position;
            pos.y += moveIncrement;
            this.transform.position = pos;
        }
    }


}
