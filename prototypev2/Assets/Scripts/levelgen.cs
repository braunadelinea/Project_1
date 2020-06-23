using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelgen : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms; // index 0-left and right 1--left right and bottom 2--left right top and 3 all four 

    private int direction; // direction in which the next room will spawn 
    public float moveAmount = 10; //each room is equally spaced apart 
    private float timebtwroom; //used to check room spawn time 
    public float starttimebtwroom = 0.25f; //0.25 seconds between each room spawn 

    public float minX; //Pose in the top left of the box, prevents rooms from breaking the left barrier of the box - set in the editor
    public float maxX; //Pose in the top right of the box, prevents rooms from breaking the right barrier of the box - set in the editor 
    public float minY; //Pose in the bottom right of the box, prevents rooms from breaking the bottom barrier of the box - set in the editor 
    //NOTE: rooms never generate/spawn upward, so no need to restrict there. 

    public bool stopgen; //Stop Generation - once we have generated the walkable path, we set this to true to allow the other rooms to spawn, and then to stop the gen process entirely 

    public int downcounter = 0; 

    public LayerMask room; 
    // Start is called before the first frame update
    void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        direction = Random.Range(1, 6); 
    }

    public void Move()
    {
        if (direction == 1 || direction == 2)
        {
            if (transform.position.x < maxX)
            {
                downcounter = 0; 
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity); 

                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 2;
                }
                else if (direction == 4) {
                    direction = 5; 
                }
            }
            else {
                direction = 5; 
            }
        }
        else if (direction == 3 || direction == 4)
        {
            if (transform.position.x > minX)
            {
                downcounter = 0; 
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity); 

                direction = Random.Range(3, 6); 
            }
            else {
                direction = 5; 
            }
        }
        else if (direction == 5) {

            downcounter++; 

            if (transform.position.y > minY)
            {
                Collider2D roomdetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if (roomdetection.GetComponent<scoutroom>().type != 1 && roomdetection.GetComponent<scoutroom>().type != 3) {
                    if (downcounter >= 2)
                    {
                        roomdetection.GetComponent<scoutroom>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {


                        roomdetection.GetComponent<scoutroom>().RoomDestruction();

                        int randbottomroom = Random.Range(1, 4);
                        if (randbottomroom == 2)
                        {
                            randbottomroom = 1;
                        }
                        Instantiate(rooms[randbottomroom], transform.position, Quaternion.identity);
                    }
                }


                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6); 
            }
            else {
                stopgen = true; 
                //reached bottom, spawn exit
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (timebtwroom <= 0 && stopgen == false)
        {
            Move();
            timebtwroom = starttimebtwroom;
        }
        else {
            timebtwroom -= Time.deltaTime; 
        }
    }
}
