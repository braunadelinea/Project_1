using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelgen : MonoBehaviour
{
    /* Possible Improvements 
       Player currently spawns in random room, make the player spawn at the start of the generated "path" (there will always be a walkable path from start to end point) by ensuring the room at the start of the generated path does not have any tiles in the center. A possible way this could be achieved is by adding all rooms to an array as they are created, putting the rooms that make up the path in order, then swap the first room in that array with an empty LRTB (all four doors open) room and place the player inside of it.   
    */


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

    public int downcounter = 0;  //how many times a room has moved down, used to help fix a nasty bug where path could be blocked if algorithim decided to go down two times in a row. 

    public LayerMask room; //LayerMask is just fancy talk for Layer, we use this to create an overlap circle, more on that later. 

    // Start is called before the first frame update
    void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length); //there are 4 starting positions for the "main" path, the top four Poses.
        transform.position = startingPositions[randStartingPos].position; //The position of the level generation object is set to the position of the random starting position we chose.
        Instantiate(rooms[0], transform.position, Quaternion.identity); //spawns a left right room prefab at the starting position so the generator can begin to branch, this sometimes is deleted and overwritten to meet other cases. 

        direction = Random.Range(1, 6); //what direction the generator will move 
    }

    public void Move()
    {
        if (direction == 1 || direction == 2) //if direction is one or two, then we will be moving right 
        {
            if (transform.position.x < maxX) //make sure we are not passing our maxX value, helping us stay inside the box 
            {
                downcounter = 0; //set downcounter to zero, since we are not moving downward. (this helps with a bug) 

                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y); //create position of where we will move the level generator: current position incremented right by moveamount, which is 10, all rooms are equally spaced apart by 10 units. 

                transform.position = newPos; //actually move the level generator to the previously defined position. 

                int rand = Random.Range(0, rooms.Length); //choose a random room type either LR, LRB, LRT, or LRTB
                Instantiate(rooms[rand], transform.position, Quaternion.identity); //create that room at our new position. 

                direction = Random.Range(1, 6); //choose the next direction to move 

                if (direction == 3) //if it says to move left, we just came from the left, so we move right again instead. 
                {
                    direction = 2; //move right again 
                }
                else if (direction == 4) { //if we are moving left, we just came from the left, but we also just told it to move right again if direction was 3 (left), so let's give it a 50/50 chance to either go down or right if we just came from left and are told to move left. 
                    direction = 5; //move down 
                }
            }
            else {
                direction = 5; //if we cannot grow the path further to the right, and just came from the left, we are forced to move down. 
            }
        }
        else if (direction == 3 || direction == 4) //if direction is three or four, then we will be moving left. 
        {
            if (transform.position.x > minX) //make sure we are not passing our minX value, helping us stay inside the box. 
            {
                downcounter = 0; //we are not moving down, so set downcounter to zero (fights the path blocking move down by 2 bug) 

                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y); //create a new position that we will move the levelgen object to, 10 units to the left of our current position, as all rooms are equally spaced by 10 units. 

                transform.position = newPos; //actually move the level generator 

                int rand = Random.Range(0, rooms.Length); //get a random room prefab
                Instantiate(rooms[rand], transform.position, Quaternion.identity); //create whichever prefab we chose

                direction = Random.Range(3, 6); //get a new direction, but exclude 1 and 2, because we just came from the right 
            }
            else {
                direction = 5; //if we have hit the left wall of the box, our only option is to move down. 
            }
        }
        else if (direction == 5) { //move down, you'll notice there is a higher chance of moving left or right, this should make the path longer, which is better. 

            downcounter++; //increment down counter because we are moving down, helps fix the block move down by 2 bug. 

            if (transform.position.y > minY) //helps stay inside the box, makes sure we do not pass below our minY value 
            {
                //lines 97-114 handle the bug that will block the main path if the generator moves down twice, I've done my best to explain it, but if you need more help, contact me (Toby) 

                Collider2D roomdetection = Physics2D.OverlapCircle(transform.position, 1, room); //creates a new circle in the current position of the level generator which a radius one, on the layer room (which has all the rooms) 
                if (roomdetection.GetComponent<scoutroom>().type != 1 && roomdetection.GetComponent<scoutroom>().type != 3) { //if the room does not have a bottom opening 
                    if (downcounter >= 2) //if we have moved down twice (the dreaded bug) 
                    {
                        roomdetection.GetComponent<scoutroom>().RoomDestruction(); //destroy the current room 
                        Instantiate(rooms[3], transform.position, Quaternion.identity); //create a room with all four doors open, ensuring the bug blocking the path will not occur 
                    }
                    else //if we have not moved down 2 times
                    {
                        roomdetection.GetComponent<scoutroom>().RoomDestruction(); //destroy the current room anyways 

                        int randbottomroom = Random.Range(1, 4); //get random room type 

                        if (randbottomroom == 2) //if the random room type is LRT, switch it to a LRB
                        {
                            randbottomroom = 1;
                        }
                        Instantiate(rooms[randbottomroom], transform.position, Quaternion.identity); //create the room 
                    }
                }


                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount); //create new position we will move level generator too, incremented by 10 units as all rooms are equally sized at 10 units. 
                transform.position = newPos; //actually move the level generator 

                int rand = Random.Range(2, 4); //get a random room typed 2 or 3 (rooms with top openings since we just moved down) 
                Instantiate(rooms[rand], transform.position, Quaternion.identity); //create that room 

                direction = Random.Range(1, 6); //figure out where to move next, we cannot move up, so anything is game 
            }
            else {
                stopgen = true; //reached bottom, spawn exit
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (timebtwroom <= 0 && stopgen == false) //if there are rooms to generate and the time between each room is up, let's move, create a room and restart the timer. 
        {
            Move();
            timebtwroom = starttimebtwroom;
        }
        else {
            timebtwroom -= Time.deltaTime; //otherwise, count the timer down 
        }
    }
}